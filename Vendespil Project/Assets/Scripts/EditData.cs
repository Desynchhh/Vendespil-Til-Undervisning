using Assets.Scripts;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditData : MonoBehaviour
{
    [Header("Int")]
    public int ID;

    [Header("InputFields")]
    public InputField questionText;
    public InputField rightAnswerText;
    public InputField wrongAnswer1Text;
    public InputField wrongAnswer2Text;
    public InputField wrongAnswer3Text;

    private string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/Data.xml";
        LoadXML(ID);
    }

    public void LoadXML(int EditID)
    {
        StartCoroutine(getQuestionById(EditID));
    }

    public void Save(int EditID, string question, string rightAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3)
    {
        //Send data til databasen så det bliver gemt.
    }

    public void RemoveAll()
    {
        //Lav api så man kan fjerne alle spørgsmål der er lavet af en bruger.
    }

    public void RemoveSingle(int ID)
    {
        deleteQuestionById(ID);
    }

    IEnumerator deleteQuestionById(int ID)
    {
        ApiHandler api = GameObject.Find("ApiHandler").GetComponentInChildren<ApiHandler>();

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("action", "deleteQuestionById");
        post.Add("id", ID.ToString());

        WWW result = api.POST(post);

        yield return new WaitUntil(() => result.isDone == true);

        var N = JSON.Parse(result.text);
    }

    IEnumerator getQuestionById(int ID)
    {
        ApiHandler api = GameObject.Find("ApiHandler").GetComponentInChildren<ApiHandler>();

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("action", "getQuestionById");
        post.Add("id", ID.ToString());

        WWW result = api.POST(post);

        yield return new WaitUntil(() => result.isDone == true);

        var N = JSON.Parse(result.text);

        if (N["error"] == null)
        {
            questionText.text = N["question"];
            rightAnswerText.text = N["correctAnwser"];
            wrongAnswer1Text.text = N["wrongAnwser1"];
            wrongAnswer2Text.text = N["wrongAnwser2"];
            wrongAnswer3Text.text = N["wrongAnwser3"];
            Debug.Log(ID + " was loaded!");
        }
    }
}