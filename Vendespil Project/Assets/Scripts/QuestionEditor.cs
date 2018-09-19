using Assets.Scripts;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionEditor : MonoBehaviour {

    public List<QuestionForEdit> questions = new List<QuestionForEdit>();

    private void OnEnable()
    {
        StartCoroutine(LoadQuestionsForEdit());
    }

    public void RemoveAll()
    {
        //Lav api så man kan fjerne alle spørgsmål der er lavet af en bruger.
    }

    public void RemoveSingle(int ID)
    {
        StartCoroutine(deleteQuestionById(ID));
    }

    IEnumerator deleteAllQuestionsByUserId()
    {
        questions.Clear();
        ApiHandler api = GameObject.Find("ApiHandler").GetComponentInChildren<ApiHandler>();

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("action", "deleteAllQuestionsByUserId");
        post.Add("id", UserData.User.id.ToString());

        WWW result = api.POST(post);

        yield return new WaitUntil(() => result.isDone == true);

        var N = JSON.Parse(result.text);
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
                x.rightAnswer = item.Value["correctAnswer"];
                x.wrongAnswer1 = item.Value["wrongAnswer1"];
                x.wrongAnswer2 = item.Value["wrongAnswer2"];
                x.wrongAnswer3 = item.Value["wrongAnswer3"];
                questions.Add(x);
            }
        }
    }

    IEnumerator insertNewQuestion(string question, string correctAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3)
    {
        questions.Clear();
        ApiHandler api = GameObject.Find("ApiHandler").GetComponentInChildren<ApiHandler>();

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("action", "insertNewQuestion");
        post.Add("question", question);
        post.Add("correctAnswer", correctAnswer);
        post.Add("wrongAnswerOne", wrongAnswer1);
        post.Add("wrongAnswerTwo", wrongAnswer2);
        post.Add("wrongAnswerThree", wrongAnswer3);
        post.Add("teamId", UserData.User.teamId.ToString());
        post.Add("userId", UserData.User.id.ToString());

        WWW result = api.POST(post);

        yield return new WaitUntil(() => result.isDone == true);

        var N = JSON.Parse(result.text);
    }

    IEnumerator editQuestionById(int id, string question, string correctAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3)
    {
        questions.Clear();
        ApiHandler api = GameObject.Find("ApiHandler").GetComponentInChildren<ApiHandler>();

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("action", "insertNewQuestion");
        post.Add("id", id.ToString());
        post.Add("question", question);
        post.Add("correctAnswer", correctAnswer);
        post.Add("wrongAnswerOne", wrongAnswer1);
        post.Add("wrongAnswerTwo", wrongAnswer2);
        post.Add("wrongAnswerThree", wrongAnswer3);
        post.Add("teamId", UserData.User.teamId.ToString());
        post.Add("userId", UserData.User.id.ToString());

        WWW result = api.POST(post);

        yield return new WaitUntil(() => result.isDone == true);

        var N = JSON.Parse(result.text);
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
