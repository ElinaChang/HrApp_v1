//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class registerUI : MonoBehaviour
{
    public InputField userNameInput;
    public InputField accountRegisterInput;
    public InputField pwdRegisterInput;
    //public InputField pwdConfirmInput;
    public Dropdown identifyDropdown;
    public List<string> listIdentify  = new List<string>();
    public Button sendButton;
    public Button backloginButton;

    public void Start()
    {
        sendButton.onClick.AddListener(send);
        //backloginButton.onClick.AddListener(back);
        //identifyDropdown.onValueChanged.AddListener(Identify);
    }

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

    public void send()
    {   
        string name = userNameInput.text;
        string account = accountRegisterInput.text;
        string pwd = pwdRegisterInput.text;
        //string pwdConfirm = pwdConfirmInput.text;

        if (!string.IsNullOrEmpty(name))
        {
            Debug.Log("UserName為" + name);
        }
        else
        {
            Debug.Log("請輸入UserName");
        }

        if (!string.IsNullOrEmpty(account))
        {
            Debug.Log("帳號為" + account);
        }
        else
        {
            Debug.Log("請輸入帳號");
        }

        if (!string.IsNullOrEmpty(pwd))
        {
            Debug.Log("密碼為: " + pwd);
        }
        else
        {
            Debug.Log("請設定帳號");
        }
        

        /* if (!string.IsNullOrEmpty(pwd) && !string.IsNullOrEmpty(pwdConfirm))
        {
            if (pwd == pwdConfirm)
            {
                Debug.Log("密碼一致，密碼為: " + pwdConfirm);
            }
            else
            {
                Debug.Log("密碼不一致，請再次確認密碼");
                Debug.Log(pwd);
                Debug.Log(pwdConfirm);
            }
        }
        else if (string.IsNullOrEmpty(pwd) || string.IsNullOrEmpty(pwdConfirm))
        {
            if (string.IsNullOrEmpty(pwd))
            {
                Debug.Log("設定密碼欄未輸入");
            }
            else if (string.IsNullOrEmpty(pwdConfirm))
            {
                Debug.Log("請輸入再次確認密碼欄");
            }
        } */
        
        //call function identify
        Identify(0); 
    }

}