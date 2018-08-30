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

        // Make sure it will stay here until it's done with the API request
        while (!result.isDone)
        {
            // TODO: Gør så den automatisk hopper ud efter 25 sekunder
        } 

        var N = JSON.Parse(result.text);
        bool correctLogin = N["login"].AsBool;
        
        if (correctLogin)
        {
            UserData.User = new User
            {
                id = N["userdata"]["username"],
                name = N["userdata"]["name"],
                password = N["userdata"]["password"],
                isAdmin = N["userdata"]["isAdmin"],
                teamId = N["userdata"]["teamId"],
                username = N["userdata"]["username"]
            };
            GameObject.Find("Manager").GetComponent<LoginWarning>().DisplayWarning(correctLogin, UserData.User.name);
        }
        else
        {
            GameObject.Find("Manager").GetComponent<LoginWarning>().DisplayWarning(correctLogin, null);
        }
        


    }



}
