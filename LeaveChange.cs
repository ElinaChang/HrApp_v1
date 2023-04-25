using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaveChange : MonoBehaviour
{
    //public Button sendButton;
    public Button returnButton;

    void Start()
    {
        //sendButton.onClick.AddListener(Send);
        returnButton.onClick.AddListener(Return);
    }

    /* public void Send()
    {
        SceneManager.LoadScene("DakaS");
    } */

    public void Return()
    {
        SceneManager.LoadScene("DakaS");
    }
}
