using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class WebTest : MonoBehaviour {

    [Header("Website address")]
    public string URL = "http://localhost/getdata2.php";

    [Header("Interface")]
    public Button SendButton;
    public InputField Username;
    public InputField Password;

    private void Start()
    {
        SendButton.onClick.AddListener(Send);
    }

    public void Send()
    {
        StartCoroutine(SendDataToPHP(Username.text, Password.text));
    }

    public class User
    {
        public int ID;
        public string UserName;
        public string RealName;
        public User(int t_ID, string t_UserName, string t_RealName)
        {
            ID = t_ID;
            UserName = t_UserName;
            RealName = t_RealName;
        }
    }

    IEnumerator SendDataToPHP(string Username, string Password)
    {
        WWWForm form = new WWWForm();
        form.AddField("getUserByUsername", Username);
        form.AddField("Password", Password);
        //form.AddField("Password", Password);
        WWW www = new WWW(URL, form);

        if (www.error != null)
        {
            Debug.Log("Ikke sendt!");
        }
        else
        {
            var N = JSON.Parse(www.text);
            User U = new User(N["UserID"].AsInt, N["UserName"].Value, N["RealName"].Value);

            Debug.Log(www.text);
            Debug.Log("Sendt!");
        }
        yield return www;
    }
}
