

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class userUpdate : MonoBehaviour
{
    string url = "http://localhost/HrApp_v1/Assets/Scripts/userUpdate.php";
    public InputField userNameInput;
    //public InputField accountRegisterInput;
    //public InputField pwdRegisterInput;
    public InputField ondateInput; //到職日
    public InputField salaryTypeInput; //salarycategory
    public InputField timeWageInput; //時薪
    public InputField dayWageInput; //日薪
    public Button saveButton;
    public Button backManageButton;
    

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        //resetButton.onClick.AddListener(Reset);
        saveButton.onClick.AddListener(Save);
        backManageButton.onClick.AddListener(BackManage);
    }

    public void Save()
    {
        string name = userNameInput.text;
        string onDate = ondateInput.text;
        string salaryTp = salaryTypeInput.text;
        string hourWage = timeWageInput.text;
        string dayWage = dayWageInput.text;


        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(onDate) || string.IsNullOrEmpty(salaryTp) || (string.IsNullOrEmpty(hourWage) && string.IsNullOrEmpty(dayWage)))
        {
            Debug.Log("請完成填寫後儲存送出");
            return;
        }

        WWWForm StaffForm = new WWWForm();
        StaffForm.AddField("editName", name);
        StaffForm.AddField("addOndate", onDate);
        StaffForm.AddField("addSalaryType", salaryTp);
        StaffForm.AddField("addDayWage", dayWage);
        StaffForm.AddField("addTimeWage", hourWage);

        StartCoroutine(SaveStaffRequest(StaffForm));
    }

    IEnumerator SaveStaffRequest(WWWForm StaffForm)
    {
        //string url = "http://localhost/HrApp_v1/Assets/Scripts/userUpdate.php";
        
        using (UnityWebRequest www = UnityWebRequest.Post(url, StaffForm))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("HTTP 錯誤：" + www.error);
            }
            else
            {
                Debug.Log("成功加入數據庫");
                Debug.Log(www.downloadHandler.text);
                BackManage();
            }
        }
    } 

    public void BackManage()
    {
        SceneManager.LoadScene("manage");
    } 
}
