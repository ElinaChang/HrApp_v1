using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DakaChange : MonoBehaviour
{
    public Button leaveButton;
    public Button logoutButton;

    void Start()
    {
        leaveButton.onClick.AddListener(Leave);
        logoutButton.onClick.AddListener(Logout);
    }

    public void Leave()
    {
        SceneManager.LoadScene("LeaveS");
    }

    public void Logout()
    {
        SceneManager.LoadScene("Menu");
    }
}
