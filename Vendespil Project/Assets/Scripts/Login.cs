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
        LoginButton.onClick.AddListener(Click);

    }

    private void Click()
    {
        StartCoroutine(CheckLogin());
    }

    IEnumerator CheckLogin()
    {
        LoginButton.interactable = false;

        ApiHandler api = GameObject.Find("ApiHandler").GetComponentInChildren<ApiHandler>();

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("action", "loginCheck");
        post.Add("username", Username.text);
        post.Add("password", Password.text);

        WWW result = api.POST(post);

        yield return new WaitUntil(() => result.isDone == true);

        LoginButton.interactable = true;

        if (result.error != null)
        {
            GameObject.Find("Manager").GetComponent<LoginWarning>().DisplayError();
        }
        else
        {
            var N = JSON.Parse(result.text);
            bool correctLogin = N["login"].AsBool;

            if (correctLogin)
            {
                UserData.User = new User
                {
                    id = N["userdata"]["id"],
                    name = N["userdata"]["name"],
                    password = N["userdata"]["password"],
                    isAdmin = N["userdata"]["isAdmin"],
                    teamId = N["userdata"]["teamId"],
                    username = N["userdata"]["username"]
                };
                GameObject.Find("Manager").GetComponent<LoginWarning>().DisplayWarning(correctLogin, UserData.User.name);
                GameObject.Find("Manager").GetComponent<MakeList>().MakeListOnPlay();
            }
            else
            {
                GameObject.Find("Manager").GetComponent<LoginWarning>().DisplayWarning(correctLogin, null);
            }
        }
    }
}
