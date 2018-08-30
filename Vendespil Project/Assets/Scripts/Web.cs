using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class Web : MonoBehaviour
{
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

    IEnumerator SendDataToPHP(string Username, string Password)
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", Username);
        form.AddField("Password", Password);
        WWW www = new WWW(URL, form);

        yield return www;
        if (www.error != null)
        {
            Debug.Log("Ikke sendt!");
        }
        else
        {
            Debug.Log(www.text);
            Debug.Log("Sendt!");
        }
    }
}
