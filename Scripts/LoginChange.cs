using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginChange : MonoBehaviour
{
    public Button forgetPwdButton;
    public Button RegisterButton;
    //public Button backloginButton;
    
    public void Start()
    {
        forgetPwdButton.onClick.AddListener(ForgetPwd);
        RegisterButton.onClick.AddListener(RegNew);
    }

    /* private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);

        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Register")
        {
            backloginButton = GameObject.Find("registerUI/backloginButton").GetComponent<Button>();   
        }
        backloginButton.onClick.AddListener(BackLogin);
    }
 */

    public void ForgetPwd()
    {
        SceneManager.LoadScene("Register");
    }

    public void RegNew()
    {
        SceneManager.LoadScene("Register");
    }
    
    /* public void backlogObject()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Register")
        {
            Button backloginButtonComponent = GameObject.Find("backloginButton").GetComponent<Button>();
            backloginButtonComponent.onClick.AddListener(BackLogin);
        }
    } */

    /* public void BackLogin()
    {
        SceneManager.LoadScene("Menu");
    } */

}
