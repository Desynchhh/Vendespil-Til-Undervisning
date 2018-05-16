using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GameController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject PanelGame;
    public GameObject PanelQuestion;

    [Header("Fields")]
    public GameObject questionField;
    public GameObject btnAnswer1;
    public GameObject btnAnswer2;
    public GameObject btnAnswer3;
    public GameObject btnAnswer4;
    
    [Header("Question Info")]
    private GameObject selectedQuestion;
    private string rightAnswer;

    [Header("Variables")]
    private GameObject selectedAnswer;
    private int totalQuestions;
    private int answeredRight;
    private int answeredWrong;

    [Header("Back Button")]
    public GameObject btnBack;

    [Header("Results Fields")]
    public GameObject rightAnswerScore;
    public GameObject wrongAnswerScore;

    public void GoToQuestion()
    {
        selectedQuestion = EventSystem.current.currentSelectedGameObject;
        if (selectedQuestion.GetComponent<ButtonManager>().isAnswered == false)
        {
            FillFields();
            transform.GetComponent<MenuManager>().GoToQuestion();
        }
    }

    public void FillFields()
    {
        btnAnswer1.GetComponent<Image>().color = Color.white;
        btnAnswer2.GetComponent<Image>().color = Color.white;
        btnAnswer3.GetComponent<Image>().color = Color.white;
        btnAnswer4.GetComponent<Image>().color = Color.white;
        btnBack.SetActive(false);

        ButtonManager questionInfo = selectedQuestion.GetComponent<ButtonManager>();
        rightAnswer = questionInfo.answer;
        questionField.GetComponentInChildren<Text>().text = questionInfo.question;
        SetRandomOrder(questionInfo);
    }

    public void CheckAnswer()
    {
        selectedAnswer = EventSystem.current.currentSelectedGameObject;
        if(selectedQuestion.GetComponent<ButtonManager>().isAnswered == false)
        {
            if (selectedAnswer.GetComponentInChildren<Text>().text == rightAnswer)
            {
                answeredRight++;
                selectedAnswer.GetComponent<Image>().color = Color.green;
            }
            else
            {
                answeredWrong++;
                selectedAnswer.GetComponent<Image>().color = Color.red;
                foreach (Transform child in PanelQuestion.transform)
                {
                    if(child.gameObject.GetComponentInChildren<Text>().text == rightAnswer && child.name != "Question")
                    {
                        child.gameObject.GetComponent<Image>().color = Color.green;
                    }
                }
            }
            selectedQuestion.GetComponent<Image>().color = selectedAnswer.GetComponent<Image>().color;
            selectedQuestion.GetComponent<ButtonManager>().isAnswered = true;
            btnBack.SetActive(true);
        }
    }

    public void WriteResults()
    {
        rightAnswerScore.GetComponentInChildren<Text>().text = string.Format("Du svarede rigtigt på {0} ud af {1} spørgsmål!", answeredRight, totalQuestions);
        wrongAnswerScore.GetComponentInChildren<Text>().text = string.Format("Du svarede forkert på {0} ud af {1} spørgsmål!", answeredWrong, totalQuestions);
    }

    private void SetRandomOrder(ButtonManager questionInfo)
    {
        List<string> answersList = new List<string>();
        List<string> randomOrderList = new List<string>();
        answersList.Add(questionInfo.answer);
        answersList.Add(questionInfo.wrongAnswer1);
        answersList.Add(questionInfo.wrongAnswer2);
        answersList.Add(questionInfo.wrongAnswer3);

        for(int i = 0; i <= 3; i++)
        {
            string temp;
            int randomIndex = Random.Range(0, answersList.Count);
            Debug.Log("random index: " + randomIndex.ToString());
            temp = answersList[randomIndex];
            randomOrderList.Add(answersList[randomIndex]);
            answersList.RemoveAt(randomIndex);
            Debug.Log(i.ToString() + " " + temp);
        }
        btnAnswer1.GetComponentInChildren<Text>().text = randomOrderList[0];
        btnAnswer2.GetComponentInChildren<Text>().text = randomOrderList[1];
        btnAnswer3.GetComponentInChildren<Text>().text = randomOrderList[2];
        btnAnswer4.GetComponentInChildren<Text>().text = randomOrderList[3];
    }

    public void GetTotalQuestions()
    {
        totalQuestions = 0;
        foreach(Transform child in PanelGame.transform.Find("Scroll View").GetChild(0).GetChild(0))
        {
            totalQuestions++;
        }
    }
}
