using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.Net.Sockets;
using UnityEngine.SceneManagement;

public class CheckInOut : MonoBehaviour
{
    public Text checkinTimeText;
    public Text checkinIPText;
    public Text checkoutTimeText;
    public Text checkoutIPText;
    public Button CheckinButton;
    public Button CheckoutButton;
    public Button leaveButton;
    public Button logoutButton;

    private void OnEnable()
    {
        StartCoroutine(GetCurrentCheckInfo());
    }

    IEnumerator GetCurrentCheckInfo()
    {
        string checkInfoURL = "http://localhost/HrApp_v1/Assets/Scripts/checkInfo.php";
        UnityWebRequest www = UnityWebRequest.Get(checkInfoURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            string[] checkInfoArray = www.downloadHandler.text.Split(',');
            
            Debug.Log("checkInfoArray length: " + checkInfoArray.Length);
            Debug.Log("checkinTimeText: " + checkInfoArray[0]);
            Debug.Log("checkinIPText: " + checkInfoArray[1]);
            Debug.Log("checkoutTimeText: " + checkInfoArray[2]);
            Debug.Log("checkoutIPText: " + checkInfoArray[3]);

            checkinTimeText.text = checkInfoArray[0];
            checkinIPText.text = checkInfoArray[1];
            checkoutTimeText.text = checkInfoArray[2];
            checkoutIPText.text = checkInfoArray[3];
            

        }
    } 

    //Check In
    string checkin_IP = "";
    string checkin_Time = "";
    public void CheckInButton()
    {        
        IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

        foreach (IPAddress addr in localIPs)
        {
            if (addr.AddressFamily == AddressFamily.InterNetwork)
            {
                checkin_IP = addr.ToString();
                break;
            }
        }

        StartCoroutine(CheckInRequest(checkin_IP));
    }

    IEnumerator CheckInRequest(string checkin_IP)
    {
        string InTimeURL = "http://localhost/HrApp_v1/Assets/Scripts/checkIn.php";
        string parameters = "?checkin_time=" + checkin_Time+"&checkin_IP=" + checkin_IP;
        UnityWebRequest InTimeRequest = UnityWebRequest.Get(InTimeURL + parameters);
        yield return InTimeRequest.SendWebRequest();

        if (InTimeRequest.result == UnityWebRequest.Result.ConnectionError || InTimeRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(InTimeRequest.error);
        }
        else
        {
            string checkinTime = InTimeRequest.downloadHandler.text;
            checkinTimeText.text = checkinTime;
            checkinIPText.text = checkin_IP;
        }
    }

    //Check Out
    string checkout_IP = "";
    string checkout_Time = "";
    public void CheckOutButton()
    {        
        IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

        foreach (IPAddress addr in localIPs)
        {
            if (addr.AddressFamily == AddressFamily.InterNetwork)
            {
                checkout_IP = addr.ToString();
                break;
            }
        }

        StartCoroutine(CheckOutRequest(checkout_IP));
    }

    IEnumerator CheckOutRequest(string checkout_IP)
    {
        string OutTimeURL = "http://localhost/HrApp_v1/Assets/Scripts/checkOut.php";
        string parameters = "?checkout_time=" + checkout_Time+"&checkout_IP=" + checkout_IP;
        UnityWebRequest OutTimeRequest = UnityWebRequest.Get(OutTimeURL + parameters);
        yield return OutTimeRequest.SendWebRequest();

        if (OutTimeRequest.result == UnityWebRequest.Result.ConnectionError || OutTimeRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(OutTimeRequest.error);
        }
        else
        {
            string checkoutTime = OutTimeRequest.downloadHandler.text;
            checkoutTimeText.text = checkoutTime;
            checkoutIPText.text = checkout_IP;
        }
    }

    public void LvButton()
    {
        SceneManager.LoadScene("LeaveS");
    }

    public void LogoutButton()
    {
        SceneManager.LoadScene("Menu");
    }
}


