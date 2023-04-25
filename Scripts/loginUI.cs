using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class loginUI : MonoBehaviour
{
    public InputField accountInput;
    public InputField passwordInput;
    public Toggle userToggle;
    public Toggle managerToggle;
    public Text messageText;
    public string staffId;

    string identify;

    public void LoginClick()
    {
        string account = accountInput.text;
        string password = passwordInput.text;
        bool isUser = userToggle.isOn;
        bool isManager = managerToggle.isOn;

        if (!isUser && !isManager)
        {
            messageText.text = "請選擇登入身份";
            return;
        }

        identify = isUser ? "使用者身份" : "管理者身份";

        Debug.Log("登入的帳號: " + account);
        //Debug.Log("登入的密碼: " + password);
        Debug.Log("是否選取user身份: " + isUser);
        Debug.Log("是否選取Manager身份: " + isManager);
        Debug.Log("登入身份為: " + identify);

        StartCoroutine(LoginCoroutine(account, password, identify));
    }

    IEnumerator LoginCoroutine(string account, string password, string identify)
    {
        string URL = "http://localhost/HrApp_v1/Assets/Scripts/login.php";
        
        WWWForm loginForm = new WWWForm();
        loginForm.AddField("account", account);
        loginForm.AddField("pwd", password);
        loginForm.AddField("identify", identify);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, loginForm))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Error: {www.error}");
                messageText.text = "Connection Error!";
                yield break;
            }

            string result = www.downloadHandler.text;
            Debug.Log("result: " + result);

            if (result == "logInformation inserted")
            {
                messageText.text = "登入成功!";
                Debug.Log("Login success!");
                
                //according to user's account to get staffid
                /* staffId = GetstaffId(account);
                Debug.Log("員工代號: " + staffId); */

                if (identify == "使用者身份")
                {
                    SceneManager.LoadScene("DakaS");
                }
                else if (identify == "管理者身份")
                {
                    SceneManager.LoadScene("manage");
                }
            }
            else if (result == "logInformation failed")
            {
                messageText.text = "請確認帳號、密碼、登入身份無誤";
                Debug.Log("Account or password incorrect!");
            }
            else
            {
                messageText.text = "Unknown Error!";
                Debug.Log("Unknown Error!");
            }
        }
    }

    public void LogoutClick()
    {
        StartCoroutine(LogoutCoroutine());
        //SceneManager.LoadScene("Menu");
    }

    IEnumerator LogoutCoroutine()
    {
        string URL = "http://localhost/HrApp_v1/Assets/Scripts/logout.php";
        
        UnityWebRequest logRequest = UnityWebRequest.Get(URL);
        yield return logRequest.SendWebRequest();

        if (logRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(logRequest.error);
            yield break;
        }

        string logResult = logRequest.downloadHandler.text;

        if (logResult == "logout time inserted")
        {
            Debug.Log("Logout time recorded.");
        }
        else
        {
            Debug.Log("Logout time not inserted.");
        }
        
        SceneManager.LoadScene("Menu");
    }

}
