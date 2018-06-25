using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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

    [Header("Colors")]
    public Color bgColor;
    public Color txtColor;
    public Color wrongColor;
    public Color rightColor;

    [Header("Buttons")]
    public GameObject btnNext;
    public GameObject btnBack;

    [Header("Results Field")]
    public GameObject rightAnswerScore;

    private void Start()
    {
        bgColor = new Color32(0x00, 0x00, 0x31, 0xFF);
        wrongColor = new Color32(0xE6, 0x1E, 0x31, 0xFF);
        rightColor = new Color32(0x88, 0xD6, 0x0D, 0xFF);
        txtColor = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
    }

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
            answeredRight = 0;
            answeredWrong = 0;
            totalAnswered = 0;
            FillFields();
            GetComponent<MenuManager>().PlayGame();
        }
    }

    public void FillFields()
    {
        btnNext.GetComponent<Image>().color = bgColor;
        btnNext.GetComponentInChildren<Text>().text = "ENDELIGE SVAR";
        foreach (Transform child in parAnswers)
        {
            child.GetComponent<Button>().enabled = true;
            child.GetComponent<Image>().color = bgColor;
            child.GetChild(0).GetComponent<Text>().color = txtColor;
        }
        btnNext.GetComponent<Button>().enabled = false;
        if (totalAnswered == totalQuestions)
        {
            WriteResults();
            GetComponent<MenuManager>().GoToResults();
        }

        else if (questionField.GetComponentInChildren<Text>().text != orderedQuestions[totalAnswered].GetChild(0).GetComponent<ButtonManager>().question)
        {
            progress.text = totalAnswered + 1 + "/" + totalQuestions;

            ButtonManager questionInfo = orderedQuestions[totalAnswered].GetChild(0).GetComponent<ButtonManager>();
            rightAnswer = questionInfo.answer;
            questionField.GetComponentInChildren<Text>().text = questionInfo.question;
            SetRandomOrder(questionInfo);
        }
    }

    public void SelectAnswer()
    {
        selectedAnswer = EventSystem.current.currentSelectedGameObject;
        foreach(Transform child in parAnswers)
        {
            if (child == selectedAnswer.transform)
            {
                child.GetComponent<Image>().color = txtColor;
                child.GetComponentInChildren<Text>().color = bgColor;
            }
            else
            {
                child.GetComponent<Image>().color = bgColor;
                child.GetComponentInChildren<Text>().color = txtColor;
            }
        }
        btnNext.GetComponent<Button>().enabled = true;
    }
    public void CheckAnswer()
    {
        if (selectedAnswer.GetComponentInChildren<Text>().text == rightAnswer)
        {
            answeredRight++;
            selectedAnswer.GetComponent<Image>().color = rightColor;
            selectedAnswer.GetComponentInChildren<Text>().color = txtColor;
            btnNext.GetComponent<Image>().color = rightColor;
            btnNext.GetComponentInChildren<Text>().text = "RIGTIGT SVAR";
            btnNext.GetComponentInChildren<Text>().color = txtColor;
        }
        else
        {
            answeredWrong++;
            selectedAnswer.GetComponent<Image>().color = wrongColor;
            selectedAnswer.GetComponentInChildren<Text>().color = txtColor;
            btnNext.GetComponent<Image>().color = wrongColor;
            btnNext.GetComponentInChildren<Text>().text = "FORKERT SVAR";
            btnNext.GetComponentInChildren<Text>().color = txtColor;
            foreach (Transform child in parAnswers)
            {
                if (child.gameObject.GetComponentInChildren<Text>().text == rightAnswer && child.name != "Question")
                {
                    child.gameObject.GetComponent<Image>().color = rightColor;
                    selectedAnswer.GetComponentInChildren<Text>().color = txtColor;
                }
            }
        }
        foreach (Transform child in parAnswers)
        {
            child.GetComponent<Button>().enabled = false;
        }
        totalAnswered++;
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        FillFields();
    }

    public void WriteResults()
    {
        rightAnswerScore.GetComponentInChildren<Text>().text = string.Format("DU FIK\n {0} / {1}\n RIGTIGE!", answeredRight, totalQuestions);
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

    public void WarningQuit()
    {
        PanelWarning.transform.Find("WarningText").Find("Text").GetComponent<Text>().text = "ER DU SIKKER PÅ DU VIL GIVE OP? DIT FREMSKIDT VIL IKKE BLIVE GEMT!";
        PanelWarning.transform.Find("btnOK").gameObject.SetActive(false);
        PanelWarning.transform.Find("btnYes").gameObject.SetActive(false);
        PanelWarning.transform.Find("btnNo").gameObject.SetActive(true);
        PanelWarning.transform.Find("btnYesQuit").gameObject.SetActive(true);
        PanelWarning.SetActive(true);
    }
}
