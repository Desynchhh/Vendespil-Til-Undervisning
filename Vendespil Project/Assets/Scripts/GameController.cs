using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Animations;


public class GameController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject PanelQuestion;
    public GameObject PanelWarning;

    [Header("Fields & UI")]
    public Transform parAnswers;
    public GameObject questionField;
    public GameObject btnAnswer1;
    public GameObject btnAnswer2;
    public GameObject btnAnswer3;
    public GameObject btnAnswer4;
    public Text progress;

    [Header("Question Info")]
    public GameObject contentEdit;

    [Header("Sorting Lists")]
    public List<Transform> unorderedQuestions = new List<Transform>();
    public List<Transform> orderedQuestions = new List<Transform>();
    private string rightAnswer;

    [Header("Variables")]
    private GameObject selectedAnswer;
    private int totalQuestions;
    private int totalAnswered;
    private int answeredRight;
    private int answeredWrong;
    public bool hasOrderedQuestions;

    [Header("Buttons")]
    public GameObject btnNext;
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

        if (totalQuestions == 0)
        {
            hasOrderedQuestions = false;
            PanelWarning.transform.Find("WarningText").Find("Text").GetComponent<Text>().text = "DER ER IKKE OPRETTET NOGLE SPØRGSMÅL!\n\n OPRET MINDST ÉT SPØRGSMÅL FOR AT SPILLE";
            PanelWarning.transform.Find("btnOK").gameObject.SetActive(true);
            PanelWarning.transform.Find("btnYes").gameObject.SetActive(false);
            PanelWarning.transform.Find("btnNo").gameObject.SetActive(false);
            PanelWarning.SetActive(true);
        }
        else
        {
            hasOrderedQuestions = true;
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
    }


    public void FillFields()
    {
        foreach (Transform child in parAnswers)
        {
            child.GetComponent<Button>().enabled = true;
        }
        btnNext.GetComponent<Button>().enabled = false;
        if (totalAnswered == totalQuestions)
        {
            WriteResults();
            GetComponent<MenuManager>().GoToResults();
        }

        else if (questionField.GetComponentInChildren<Text>().text != orderedQuestions[totalAnswered].GetComponent<ButtonManager>().question)
        {
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
        }
    }

    public void CheckAnswer()
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
            foreach (Transform child in parAnswers)
            {
                if (child.gameObject.GetComponentInChildren<Text>().text == rightAnswer && child.name != "Question")
                {
                    child.gameObject.GetComponent<Image>().color = Color.green;
                }
            }
        }
        foreach (Transform child in parAnswers)
        {
            child.GetComponent<Button>().enabled = false;
        }
        totalAnswered++;
        btnNext.GetComponent<Button>().enabled = true;
    }

    public void WriteResults()
    {
        //if (answeredRight == totalQuestions)
        //{
        //    wrongAnswerScore.SetActive(false);
        //    rightAnswerScore.GetComponentInChildren<Text>().text = "TILLYKKE! DU SVAREDE RIGTIGT PÅ ALLE SPØRGSMÅL!";
        //}
        //else if (answeredWrong == totalQuestions)
        //{
        //    rightAnswerScore.SetActive(false);
        //    wrongAnswerScore.GetComponentInChildren<Text>().text = "DU SVAREDE FORKERT PÅ HVERT ENESTE SPØRGSMÅL. BEDRE HELD NÆSTE GANG!";
        //}
        //else
        //{
            rightAnswerScore.GetComponentInChildren<Text>().text = string.Format("DU FIK\n {0} / {1}\n RIGTIGE!", answeredRight, totalQuestions);
            //wrongAnswerScore.GetComponentInChildren<Text>().text = string.Format("DU SVAREDE FORKERT PÅ {0} UD AF {1} SPØRGSMÅL!", answeredWrong, totalQuestions);
        //}
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
