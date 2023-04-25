using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class userDel : MonoBehaviour
{
    string url = "http://localhost/HrApp_v1/Assets/Scripts/userDelete.php";
    /* public InputField userNameInput;
    public InputField accountRegisterInput;
    public InputField pwdRegisterInput; */
    /* public string userNameInput;
    public string accountRegisterInput;
    public string pwdRegisterInput; */
    public string WhereField;
    public string WhereCondition;

    private void Awake() //繼承
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {

    }

    void Update()
    {
        DelUser(WhereField, WhereCondition);
    }

    public void DelUser (string wF, string wC)
    {
        /* string name = userNameInput.text;
        string account = accountRegisterInput.text;
        string pwd = pwdRegisterInput.text; */

        WWWForm form = new WWWForm();
        form.AddField ("whereField", wF);
        form.AddField ("whereCondition", wC);

        //WWW www = new WWW (URL, form);
        StartCoroutine(SendRequest(form));
    }

    IEnumerator SendRequest(WWWForm form)
    {
        // 實作送出請求的方法
        yield return null;
    }
}
