using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Web : MonoBehaviour {

    [Header("UI")]
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

        WWW www = new WWW("http://localhost/getdata2.php", form);

        yield return www;
        if(www.error != null)
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
