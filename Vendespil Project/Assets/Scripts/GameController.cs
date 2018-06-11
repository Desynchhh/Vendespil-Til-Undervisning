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
    public Text progress;

    [Header("Question Info")]
    public GameObject contentEdit;
    public List<Transform> unorderedQuestions = new List<Transform>();
    public List<Transform> orderedQuestions = new List<Transform>();
    //private GameObject selectedQuestion;
    private string rightAnswer;

    [Header("Variables")]
    private GameObject selectedAnswer;
    private int totalQuestions;
    private int totalAnswered;
    private int answeredRight;
    private int answeredWrong;

    [Header("Back Button")]
    public GameObject btnBack;

    [Header("Results Fields")]
    public GameObject rightAnswerScore;
    public GameObject wrongAnswerScore;

    public void SetQuestionsRandomOrder()
    {
        if (orderedQuestions.Count >= 0 || unorderedQuestions.Count >= 0)
        {
            totalQuestions = 0;
            totalAnswered = 0;
            unorderedQuestions.Clear();
            orderedQuestions.Clear();
        }

        foreach (Transform child in contentEdit.transform)
        {
            unorderedQuestions.Add(child);
            totalQuestions++;
        }

        for (int i = 0; i <= totalQuestions - 1; i++)
        {
            Transform temp;
            int randomIndex = Random.Range(0, unorderedQuestions.Count);
            temp = unorderedQuestions[randomIndex];
            orderedQuestions.Add(unorderedQuestions[randomIndex]);
            unorderedQuestions.RemoveAt(randomIndex);
        }
        FillFields();
    }


    public void FillFields()
    {
        if (totalAnswered == totalQuestions)
        {
            WriteResults();
            GetComponent<MenuManager>().GoToResults();
        }

        else if (questionField.GetComponentInChildren<Text>().text != orderedQuestions[totalAnswered].GetComponent<ButtonManager>().question)
        {
            Debug.Log("totalAnswered before function: " + totalAnswered);
            btnAnswer1.GetComponent<Image>().color = Color.white;
            btnAnswer2.GetComponent<Image>().color = Color.white;
            btnAnswer3.GetComponent<Image>().color = Color.white;
            btnAnswer4.GetComponent<Image>().color = Color.white;
            btnBack.SetActive(false);
            progress.text = totalAnswered + 1 + "/" + totalQuestions;

            ButtonManager questionInfo = orderedQuestions[totalAnswered].GetComponent<ButtonManager>();
            rightAnswer = questionInfo.answer;
            questionField.GetComponentInChildren<Text>().text = questionInfo.question;
            SetRandomOrder(questionInfo);
            Debug.Log("totalAnswered after function: " + totalAnswered);
        }
    }

    public void CheckAnswer()
    {
        if (orderedQuestions[totalAnswered].GetComponent<ButtonManager>().isAnswered == false)
        {
            selectedAnswer = EventSystem.current.currentSelectedGameObject;
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
                    if (child.gameObject.GetComponentInChildren<Text>().text == rightAnswer && child.name != "Question")
                    {
                        child.gameObject.GetComponent<Image>().color = Color.green;
                    }
                }
            }
            totalAnswered++;
        }
    }

    public void WriteResults()
    {
        if (answeredRight == totalQuestions)
        {
            wrongAnswerScore.SetActive(false);
            rightAnswerScore.GetComponentInChildren<Text>().text = "Tillykke! Du svarede rigtigt på alle spørgsmål!";
        }
        else if (answeredWrong == totalQuestions)
        {
            rightAnswerScore.SetActive(false);
            wrongAnswerScore.GetComponentInChildren<Text>().text = "Du svarede forkert på hvert eneste spørgsmål. Bedre held næste gang!";
        }
        else
        {
            rightAnswerScore.GetComponentInChildren<Text>().text = string.Format("Du svarede rigtigt på {0} ud af {1} spørgsmål!", answeredRight, totalQuestions);
            wrongAnswerScore.GetComponentInChildren<Text>().text = string.Format("Du svarede forkert på {0} ud af {1} spørgsmål!", answeredWrong, totalQuestions);
        }
    }

    private void SetRandomOrder(ButtonManager questionInfo)
    {
        List<string> answersList = new List<string>();
        List<string> randomOrderList = new List<string>();
        answersList.Add(questionInfo.answer);
        answersList.Add(questionInfo.wrongAnswer1);
        answersList.Add(questionInfo.wrongAnswer2);
        answersList.Add(questionInfo.wrongAnswer3);

        for (int i = 0; i <= 3; i++)
        {
            string temp;
            int randomIndex = Random.Range(0, answersList.Count);
            temp = answersList[randomIndex];
            randomOrderList.Add(answersList[randomIndex]);
            answersList.RemoveAt(randomIndex);
        }
        btnAnswer1.GetComponentInChildren<Text>().text = randomOrderList[0];
        btnAnswer2.GetComponentInChildren<Text>().text = randomOrderList[1];
        btnAnswer3.GetComponentInChildren<Text>().text = randomOrderList[2];
        btnAnswer4.GetComponentInChildren<Text>().text = randomOrderList[3];
    }
}
