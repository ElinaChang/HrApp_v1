using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StaffChange : MonoBehaviour
{
    public Button DelButton;
    public Button SavButton;

    void Start()
    {
        DelButton.onClick.AddListener(Del);
        SavButton.onClick.AddListener(Save);
    }

    public void Del()
    {
        SceneManager.LoadScene("manage");
    }

    public void Save()
    {
        SceneManager.LoadScene("manage");
    }
}
