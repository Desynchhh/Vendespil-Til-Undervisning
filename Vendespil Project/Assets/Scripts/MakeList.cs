using Assets.Scripts;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;
public class MakeList : MonoBehaviour {

    public GameObject Content;
    public GameObject Prefab;
    public List<LoadedQuestion> questions = new List<LoadedQuestion>();

    IEnumerator AddList()
    {
        ApiHandler api = GameObject.Find("ApiHandler").GetComponentInChildren<ApiHandler>();

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("action", "getAllUsers");

        WWW result = api.POST(post);

        yield return new WaitUntil(() => result.isDone == true);

        var N = JSON.Parse(result.text);

        foreach (Transform child in Content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var item in N)
        {
            if(N["error"] == null)
            {
                GameObject newList = (GameObject)Instantiate(Prefab, Content.transform);
                newList.name = item.Value["username"];
                newList.GetComponentInChildren<Text>().text = item.Value["name"];
                newList.GetComponent<ListButton>().UserID = item.Value["id"];
            }
        }
    }

    public void SetData(int ID)
    {
        StartCoroutine(getQuestionsByUserId(ID));

    }


    IEnumerator getQuestionsByUserId(int UserID)
    {
        questions.Clear();
        ApiHandler api = GameObject.Find("ApiHandler").GetComponentInChildren<ApiHandler>();

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("action", "getQuestionsByUserId");
        post.Add("id", UserID.ToString());

        WWW result = api.POST(post);

        yield return new WaitUntil(() => result.isDone == true);

        var N = JSON.Parse(result.text);

        foreach (var item in N)
        {
            if (N["error"] == null)
            {
                LoadedQuestion x = new LoadedQuestion();
                x.IdNumber = item.Value["id"];
                x.question = item.Value["question"];
                x.rightAnswer = item.Value["correctAnwser"];
                x.wrongAnswer1 = item.Value["wrongAnwser1"];
                x.wrongAnswer2 = item.Value["wrongAnwser2"];
                x.wrongAnswer3 = item.Value["wrongAnwser3"];
                questions.Add(x);
            }
        }

        foreach(var item in questions)
        {
            print("ID: (" + item.IdNumber + ") Spørgsmål: (" + item.question + ") Rigtigt: (" + item.rightAnswer + ") Forkert1: (" + item.wrongAnswer1 + ") Forkert2: (" + item.wrongAnswer2 + ") Forkert3: (" + item.wrongAnswer3 + ")");
        }
        transform.root.Find("Manager").GetComponent<GameController>().SetQuestions(questions);
        transform.root.Find("Manager").GetComponent<GameController>().SetQuestionsRandomOrder();
    }

    public void MakeListOnPlay()
    {
        StartCoroutine(AddList());
    }


    [Serializable]
    public class LoadedQuestion
    {
        public int IdNumber;
        public string question;
        public string rightAnswer;
        public string wrongAnswer1;
        public string wrongAnswer2;
        public string wrongAnswer3;
    }
}