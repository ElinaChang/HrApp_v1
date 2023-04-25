using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


[System.Serializable]
public class StaffData 
{
    public string staff_id;
    public string name;
}

public class StaffManage : MonoBehaviour 
{
    public GameObject staffTogglePrefab;
    public Transform staffToggles;
    public Button deleteButton;
    public Button selectAllButton;
    public Button editButton;

    private List<StaffData> staffDataList = new List<StaffData>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start() 
    {
        StartCoroutine(LoadStaffDataCoroutine());
        deleteButton.onClick.AddListener(DeleteSelectedStaff);
        selectAllButton.onClick.AddListener(SelectAllStaff);
        editButton.onClick.AddListener(EditStaffData);
    }

    IEnumerator LoadStaffDataCoroutine() 
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/HrApp_v1/Assets/Scripts/staffData.php");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) 
        {
            Debug.Log(www.error);
        } 
        else 
        {
            string[] lines = www.downloadHandler.text.Split('\n');
            foreach (string line in lines) 
            {
                if (!string.IsNullOrEmpty(line)) 
                {
                    string[] values = line.Split(',');
                    string staff_id = values[0];
                    string staff_name = values[1];

                    StaffData staffData = new StaffData();
                    staffData.staff_id = staff_id;
                    staffData.name = staff_name;
                    staffDataList.Add(staffData);

                    GameObject toggleObj = Instantiate(staffTogglePrefab, staffToggles);
                    Toggle toggle = toggleObj.GetComponent<Toggle>();
                    toggle.name = staff_id;
                    toggle.onValueChanged.AddListener((value) => 
                    {
                        if (value) 
                        {
                            Debug.Log("Staff ID: " + staff_id + ", Name: " + staff_name + " is selected.");
                        }
                    });
                    Text nameText = toggleObj.transform.GetChild(1).GetComponent<Text>();
                    nameText.text = "職編："+staff_id + "   |   姓名：" + staff_name;
                }
            }
        }
    }

    public void DeleteSelectedStaff() 
    {
        List<StaffData> staffDataToDelete = new List<StaffData>();

        foreach (Transform toggle in staffToggles.transform) 
        {
            if (toggle.GetComponent<Toggle>().isOn) 
            {
                string staff_id = toggle.name;

                StaffData staffData = staffDataList.Find(x => x.staff_id == staff_id);
                if (staffData != null) 
                {
                    staffDataToDelete.Add(staffData);
                }

                StartCoroutine(DeleteStaffCoroutine(staff_id));
                Destroy(toggle.gameObject);
            }
        }

        foreach (StaffData staffData in staffDataToDelete) 
        {
            staffDataList.Remove(staffData);
        }
    }

    IEnumerator DeleteStaffCoroutine(string staff_id) 
    {
        WWWForm form = new WWWForm();
        form.AddField("staff_id", staff_id);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/HrApp_v1/Assets/Scripts/delete_staff.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) 
            {
                Debug.Log(www.error);
            } 
            else 
            {
                Debug.Log("ID:" + staff_id + " has been deleted.");
            }
        }
    }

    public void SelectAllStaff() 
    {
        bool allSelected = true;

        foreach (Transform toggle in staffToggles.transform) 
        {
            Toggle toggleComponent = toggle.GetComponent<Toggle>();

            // toggle沒有被選取就將allSelected設為false
            if (!toggleComponent.isOn) 
            {
                allSelected = false;
                toggleComponent.isOn = true; // 設定toggle為選取狀態
            }
        }

        // 如果所有toggle都已經被選取就取消所有toggle選取狀態
        if (allSelected) 
        {
            foreach (Transform toggle in staffToggles.transform) 
            {
                toggle.GetComponent<Toggle>().isOn = false;
            }
        }
    }

    public void EditStaffData()
    {
        string staffIdToEdit = null;

        // 取得被選取的Toggle
        foreach (Transform toggle in staffToggles.transform) 
        {
            if (toggle.GetComponent<Toggle>().isOn) 
            {
                staffIdToEdit = toggle.name;
                break;
            }
        }

        // 如果沒有被選取任何Toggle，則顯示錯誤訊息
        if (staffIdToEdit == null) 
        {
            Debug.Log("Please select a staff to edit.");
            return;
        }

        // 從資料庫取得要編輯的人員資料
        StartCoroutine(LoadStaffDataByIdCoroutine(staffIdToEdit));
    }

    IEnumerator LoadStaffDataByIdCoroutine(string staffId)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/HrApp_v1/Assets/Scripts/staffData.php?staff_id=" + staffId);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) 
        {
            Debug.Log(www.error);
        } 
        else 
        {
            string[] values = www.downloadHandler.text.Split(',');
            //string staff_id = values[0];
            string staff_name = values[1];
            string staff_ondate = values[5];
            string staff_salaryType = values[6];
            string staff_dailyWage = values[7];
            string staff_timeWage = values[8];

            // 將取得的人員資料顯示在編輯人員場景的輸入欄位中
            GameObject editScene = GameObject.Find("StaffSave");
            InputField nameInputField = editScene.transform.Find("userNameInput").GetComponent<InputField>();
            InputField ondateInputField = editScene.transform.Find("ondateInput").GetComponent<InputField>();
            InputField salaryTypeInputField = editScene.transform.Find("salaryTypeInput").GetComponent<InputField>();
            InputField dailyWageInputField = editScene.transform.Find("dayWageInput").GetComponent<InputField>();
            InputField timeWageInputField = editScene.transform.Find("timeWageInput").GetComponent<InputField>();

            nameInputField.text = staff_name;
            ondateInputField.text = staff_ondate;
            salaryTypeInputField.text = staff_salaryType;
            dailyWageInputField.text = staff_dailyWage;
            timeWageInputField.text = staff_timeWage;


            // 切換到編輯人員場景
            UnityEngine.SceneManagement.SceneManager.LoadScene("staffEdit");
        }
    }
}



