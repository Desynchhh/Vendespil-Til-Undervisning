using Assets.Scripts;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditData : MonoBehaviour
{

    public List<QuestionForEdit> questions = new List<QuestionForEdit>();

    private void OnEnable()
    {
        StartCoroutine(LoadQuestionsForEdit());
    }

    public void Save() //int EditID, string question, string rightAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3
    {
        //Send data til databasen så det bliver gemt.
    }

    public void RemoveAll()
    {
        //Lav api så man kan fjerne alle spørgsmål der er lavet af en bruger.
    }

    public void RemoveSingle(int ID)
    {
        StartCoroutine(deleteQuestionById(ID));
    }

    IEnumerator LoadQuestionsForEdit()
    {
        questions.Clear();
        ApiHandler api = GameObject.Find("ApiHandler").GetComponentInChildren<ApiHandler>();

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("action", "getQuestionsByUserId");
        post.Add("id", UserData.User.id.ToString());

        WWW result = api.POST(post);

        yield return new WaitUntil(() => result.isDone == true);

        var N = JSON.Parse(result.text);

        foreach (var item in N)
        {
            if (N["error"] == null)
            {
                QuestionForEdit x = new QuestionForEdit();
                x.IdNumber = item.Value["id"];
                x.question = item.Value["question"];
                x.rightAnswer = item.Value["correctAnwser"];
                x.wrongAnswer1 = item.Value["wrongAnwser1"];
                x.wrongAnswer2 = item.Value["wrongAnwser2"];
                x.wrongAnswer3 = item.Value["wrongAnwser3"];
                questions.Add(x);
            }
        }
    }

    IEnumerator deleteQuestionById(int ID)
    {
        ApiHandler api = GameObject.Find("ApiHandler").GetComponentInChildren<ApiHandler>();

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("action", "deleteQuestionById");
        post.Add("id", ID.ToString());

        WWW result = api.POST(post);

        yield return new WaitUntil(() => result.isDone == true);

        //var N = JSON.Parse(result.text);
    }

    [Serializable]
    public class QuestionForEdit
    {
        public int IdNumber;
        public string question;
        public string rightAnswer;
        public string wrongAnswer1;
        public string wrongAnswer2;
        public string wrongAnswer3;
    }
}