/* using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetPwd : MonoBehaviour
{
    public InputField nameInput;
    public InputField accountInput;
    public Dropdown identifyDropdown;
    public InputField newPasswordInput;
    public Button resetButton;

    public void Start()
    {
        resetButton.onClick.AddListener(ResetPassword);
    }

    public void ResetPassword()
    {
        string name = nameInput.text;
        string account = accountInput.text;
        string newPassword = newPasswordInput.text;

        // 辨認身份別
        string identify = "";

        void Identify(int index)
        {
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

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(account) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(identify))
        {
            Debug.Log("請完成填寫");
            return;
        }
        Debug.Log("身份別為:" + identify);

        StartCoroutine(UpdatePasswordCoroutine(name, account, identify, newPassword));
    }

    IEnumerator UpdatePasswordCoroutine(string name, string account, string identify, string newPassword)
    {
        string url = "http://localhost/HrApp_v1/Assets/Scripts/resetPwd.php";
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("account", account);
        form.AddField("identify", identify);
        form.AddField("pwd", newPassword);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("HTTP 錯誤：" + www.error);
            }
            else
            {
                Debug.Log("伺服器回應：" + www.downloadHandler.text);

                if (www.downloadHandler.text.Contains("密碼更新成功"))
                {
                    Debug.Log("密碼更新成功,請重新登入");
                    SceneManager.LoadScene("Menu");
                }
                else
                {
                    Debug.Log("密碼更新失敗");
                }
            }
        }
    }
} */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetPwd : MonoBehaviour
{
    public InputField nameInput;
    public InputField accountInput;
    public Dropdown identifyDropdown;
    public InputField newPasswordInput;
    public Button resetButton;

    private bool isIdentifySelected = false;

    public void Start()
    {
        resetButton.onClick.AddListener(ResetPassword);
    }

    public void ResetPassword()
    {
        string name = nameInput.text;
        string account = accountInput.text;
        string newPassword = newPasswordInput.text;

        // 辨認身份別
        string identify = "";

        if (identifyDropdown != null)
        {
            switch (identifyDropdown.value)
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

            isIdentifySelected = true;
        }
        else
        {
            Debug.LogError("IdentifyDropdown is not assigned in the inspector!");
        }

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(account) || string.IsNullOrEmpty(newPassword) || !isIdentifySelected)
        {
            Debug.Log("請完成填寫");
            return;
        }

        Debug.Log("身份別為:" + identify);

        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("account", account);
        form.AddField("identify", identify);
        form.AddField("pwd", newPassword);

        StartCoroutine(UpdatePasswordCoroutine(form));
    }

    IEnumerator UpdatePasswordCoroutine(WWWForm form)
    {
        string url = "http://localhost/HrApp_v1/Assets/Scripts/resetPwd.php";

        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("HTTP 錯誤：" + www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log("Response: " + www.downloadHandler.text);
            Debug.Log("密碼更新成功,請重新登入");
            SceneManager.LoadScene("Menu");
        }
    }
}


/* 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetPwd : MonoBehaviour
{
    public InputField nameInput;
    public InputField accountInput;
    public Dropdown identifyDropdown;
    public InputField newPasswordInput;
    public Button resetButton;

    public void Start()
    {
        resetButton.onClick.AddListener(ResetPassword);
    }
    
    public void ResetPassword()
    {
        string name = nameInput.text;
        string account = accountInput.text;
        string newPassword = newPasswordInput.text;
        
        //--- 辨認身份別 ---
        string identify = "";
        
        void ID ()
        {
            if (identifyDropdown != null)
            {
                identifyDropdown.onValueChanged.AddListener(delegate { Identify(identifyDropdown.value);});
            }
            else
            {
                Debug.LogError("IdentifyDropdown is not assigned in the inspector!");
            }
        }

        void Identify (int index)
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
               
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(account) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(identify))
        {
            Debug.Log("請完成填寫");
            return;
        }
        Debug.Log("身份別為:"+identify);
        
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("account", account);
        form.AddField("identify", identify);
        form.AddField("pwd", newPassword);

        StartCoroutine(UpdatePasswordCoroutine(form));
    }

    IEnumerator UpdatePasswordCoroutine(WWWForm form)
    {
        string Url = "http://localhost/HrApp_v1/Assets/Scripts/resetPwd.php";

        // 新建UnityWebRequest對象
        UnityWebRequest www = UnityWebRequest.Post(Url, form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("HTTP 錯誤：" + www.error);
        }
        else
        {
            Debug.Log("密碼更新成功,請重新登入");
            SceneManager.LoadScene("Menu");
        }
    }
} 
*/