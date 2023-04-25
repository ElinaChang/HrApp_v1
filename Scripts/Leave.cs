using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class Leave : MonoBehaviour
{
    //public Text DateStartText;
    //public Text DateEndText;
    public InputField DateStartInput;
    public InputField DateEndInput;
    public Dropdown lvTypeDropdown;
    public InputField ReasonInput;
    public Button lv_sendButton;

    void Start()
    {
        Type();
        lv_sendButton.onClick.AddListener(LvSend);
    }

    // 請假類別
    public string Lv_Type = "";
    public void Type ()
    {
        if (lvTypeDropdown != null)
        {
            lvTypeDropdown.onValueChanged.AddListener(delegate { LvType(lvTypeDropdown.value); });
        }
        else
        {
            Debug.LogError("lvTypeDropdown is not assigned in the inspector!");
        }
    }

    public void LvType (int index)
    {
        Lv_Type = "";

        switch (index)
        {
            case 0:
                Lv_Type = "病假";
                break;
            case 1:
                Lv_Type = "事假";
                break;
            case 2:
                Lv_Type = "喪假";
                break;
            case 3:
                Lv_Type = "生理假";
                break;
            default:
                break;
        }

        Debug.Log("請假類別為：" + Lv_Type);
    }

    //--- 送出假單 ---
    public void LvSend()
    {
        string DateStart = DateStartInput.text;
        string DateEnd = DateEndInput.text;
        string Reason = ReasonInput.text;
        
        if (!string.IsNullOrEmpty(DateStart))
        {
            Debug.Log(DateStart);
        }
        if (!string.IsNullOrEmpty(DateEnd))
        {
            Debug.Log(DateEnd);
        }
        if (!string.IsNullOrEmpty(Reason))
        {
            Debug.Log(Reason);
        }
        Debug.Log("請假類別為：" + Lv_Type);

        if (string.IsNullOrEmpty(DateStart) || string.IsNullOrEmpty(DateEnd) || string.IsNullOrEmpty(Reason) || string.IsNullOrEmpty(Lv_Type))
        {
            Debug.Log("未完成填寫");
            return;
        }
        
        
        WWWForm formLeave = new WWWForm();
        formLeave.AddField("lv_start", DateStart);
        formLeave.AddField("lv_end", DateEnd);
        formLeave.AddField("lv_reason", Reason);
        formLeave.AddField("lv_type", Lv_Type);

        StartCoroutine(SendRequest(formLeave));
    }

    IEnumerator SendRequest(WWWForm formLeave)
    {
        string url = "http://localhost/HrApp_v1/Assets/Scripts/leave.php";
        
        using (UnityWebRequest www = UnityWebRequest.Post(url, formLeave))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("HTTP 錯誤：" + www.error);
            }
            else if (www.responseCode == 200)
            {
                Debug.Log("成功加入假單");
            }
            else
            {
                Debug.Log("伺服器錯誤：" + www.responseCode);
            }
        }
    }

}
