using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class userInsert : MonoBehaviour
{   
    string url = "http://localhost/HrApp_v1/Assets/Scripts/userInsert.php";
    public InputField userNameInput;
    public InputField accountRegisterInput;
    public InputField pwdRegisterInput;
    public Dropdown identifyDropdown; //identifyDropdown
    public Button sendButton;
    //public InputField ondateInput; //到職日
    //public InputField salaryTypeInput; //salarycategory
    //public InputField timeWageInput; //時薪
    //public InputField dayWageInput; //日薪
    //public Button savButton;
    
    private void Awake() //繼承
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        // check Button
        /* if (sendButton != null){
            sendButton.onClick.AddListener(send);
        }
        else{
            Debug.LogError("SendButton is not assigned in the inspector!");
        } */

        sendButton.onClick.AddListener(send);
        //savButton.onClick.AddListener(SaveStaff);
    }

    //--- 辨認身份別 ---
    public string identify = "";
    public void ID ()
    {
        if (identifyDropdown != null)
        {
            identifyDropdown.onValueChanged.AddListener(delegate { Identify(identifyDropdown.value); });
        }
        else
        {
            Debug.LogError("IdentifyDropdown is not assigned in the inspector!");
        }
    }

    public void Identify (int index)
    {
        identify = "";

        switch (index)
        {
            case 0:
                identify = "使用者身份";
                break;
            case 1:
                identify = "管理者身份";
                break;
            default:
                break;
        }

        Debug.Log("選擇的身份為：" + identify);
    }

    //--- 註冊頁送出命令 ---
    public void send()
    {
        string name = userNameInput.text;
        string account = accountRegisterInput.text;
        string pwd = pwdRegisterInput.text;
        //identify = identifyDropdown.options[identifyDropdown.value].text;
        //identify = xxxx.options[xxxx.value].text;
        
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(account) || string.IsNullOrEmpty(pwd) || string.IsNullOrEmpty(identify))
        {
            Debug.Log("尚未完成資料填寫");
            return;
        }
        Debug.Log("身份別為:"+identify);
        
        //create
        WWWForm formRegister = new WWWForm();
        formRegister.AddField("addName", name);
        formRegister.AddField("addAccount", account);
        formRegister.AddField("addpwd", pwd);
        formRegister.AddField("addIdentify", identify);

        //POST
        StartCoroutine(SendRequest(formRegister));
    }

    IEnumerator SendRequest(WWWForm formRegister)
    {
        string url = "http://localhost/HrApp_v1/Assets/Scripts/userInsert.php";
        
        //发送POST请求并等待响应
        using (UnityWebRequest www = UnityWebRequest.Post(url, formRegister))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("HTTP 錯誤：" + www.error);
            }
            else
            {
                Debug.Log("成功加入數據庫");
            }
        }
    }

}
