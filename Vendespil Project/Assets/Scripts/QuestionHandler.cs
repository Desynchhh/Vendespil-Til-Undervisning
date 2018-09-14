using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using Assets.Scripts;

public class QuestionHandler
{
    public QuestionHandler()
    {
        Debug.Log("Loaded: QuestionHandler");

  
    }
    /*
    public List<Question> ReturnAllQuestions()
    {
        ApiHandler api = GameObject.Find("ApiHandler").GetComponentInChildren<ApiHandler>();

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("action", "loginCheck");
        post.Add("username", Username.text);
        post.Add("password", Password.text);

        WWW result = api.POST(post);

        yield return new WaitUntil(() => result.isDone == true);

        var N = JSON.Parse(result.text);


    }
    */
}
