using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LeaveData : MonoBehaviour
{
    //private string phpUrl = "http://localhost/HrApp_v1/Assets/Scripts/leaveData.php";
    public Text offdayText;
    IEnumerator Start()
    {
        // Set the URL of the PHP script
        string url = "http://localhost/HrApp_v1/Assets/Scripts/leaveData.php";

        // Send a GET request to the PHP script
        UnityWebRequest www = UnityWebRequest.Get(url);

        // Wait for the response
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            // Display an error message if there was an error with the request
            Debug.LogError(www.error);
            offdayText.text = "Error retrieving leave days.";
        }
        else
        {
            // Parse the response and display the leave days
            string response = www.downloadHandler.text;
            Debug.Log(response);
            float leaveDays = float.Parse(response);
            offdayText.text = leaveDays.ToString();
        }
    }
}
