using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class userSelect : MonoBehaviour
{
    string URL = "http://localhost/HrApp_v1/Assets/Scripts/userSelect.php";
    public string [] usersData;

    private void Awake() //繼承
    {
        DontDestroyOnLoad(this.gameObject);
    }


    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string usersDataString = www.downloadHandler.text;
            usersData = usersDataString.Split(';');

            print(GetValueData(usersData[0], "username:"));
        }
    }

    string GetValueData(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }

}
