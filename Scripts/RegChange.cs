using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegChange : MonoBehaviour
{
    public Button backloginButton;

    void Start()
    {
        backloginButton.onClick.AddListener(BackLogin);
    }

    public void BackLogin()
    {
        SceneManager.LoadScene("Menu");
    }
}