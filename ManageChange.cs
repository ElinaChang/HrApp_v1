using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageChange : MonoBehaviour
{
    public Button LogoutButton;
    public Button EditButton;

    void Start()
    {
        LogoutButton.onClick.AddListener(Logout);
        EditButton.onClick.AddListener(Edit);
    }

    public void Logout()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Edit()
    {
        SceneManager.LoadScene("StaffEdit");
    }
}
