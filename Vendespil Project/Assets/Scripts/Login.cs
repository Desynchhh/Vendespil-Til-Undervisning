using Assets.Scripts;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{


    [Header("Interface")]
    public Button LoginButton;
    public InputField Username;
    public InputField Password;


    private void Start()
    {
        LoginButton.onClick.AddListener(CheckLogin);

    }
    
    void CheckLogin()
    {
        //StartCoroutine(SendDataToPHP(Username.text, Password.text));
        ApiHandler api = GameObject.Find("ApiHandler").GetComponentInChildren<ApiHandler>();

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("action", "loginCheck");
        post.Add("username", Username.text);
        post.Add("password", Password.text);

        WWW result = api.POST(post);

        while (!result.isDone) { } // Make sure it will stay here until it's done with the API request

        string jsonData = result.text;

        var N = JSON.Parse(jsonData);

        bool correctLogin = N["login"].AsBool;

        if (correctLogin)
            Debug.Log("Correct login");
        else
            Debug.Log("Wrong login");

    }



}
