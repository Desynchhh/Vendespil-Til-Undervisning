using Assets.Scripts;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;
public class MakeList : MonoBehaviour {

    public GameObject Content;

    public void AddList()
    {
        //StartCoroutine(SendDataToPHP(Username.text, Password.text));
        ApiHandler api = GameObject.Find("ApiHandler").GetComponentInChildren<ApiHandler>();

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("action", "getQuestionsByTeamId");
        post.Add("teamId", UserData.User.teamId.ToString());

        WWW result = api.POST(post);

        // Make sure it will stay here until it's done with the API request
        while (!result.isDone)
        {
            // TODO: Gør så den automatisk hopper ud efter 25 sekunder
        }

        var N = JSON.Parse(result.text);
        Debug.Log(result.text);
    }

    private void OnEnable()
    {
        AddList();
    }
}
