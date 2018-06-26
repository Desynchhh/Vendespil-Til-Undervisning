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
    private List<Transform> unorderedQuestions = new List<Transform>();
    private List<Transform> orderedQuestions = new List<Transform>();
    private string rightAnswer;

    [Header("Variables")]
    private GameObject selectedAnswer;
    private int totalQuestions;
    private int totalAnswered;
    private int answeredRight;
    private int answeredWrong;
    [HideInInspector]
    public bool hasOrderedQuestions;

    [Header("Question Sprites")]
    public Sprite btnBlack;
    public Sprite btnWhite;
    public Sprite btnRed;
    public Sprite btnGreen;
    public Sprite nextFinalAnswer;
    public Sprite nextCorrect;
    public Sprite nextWrong;

    [Header("BG Overlay")]
    public Transform bgOverlay;
    public Sprite overlayBad;
    public Sprite overlayGood;

    [Header("Text Colors")]
    private Color txtBlack;
    private Color txtWhite;
    private Color txtRed;
    private Color txtGreen;

    [Header("Buttons")]
    public GameObject btnNext;
    public GameObject btnBack;

    [Header("Results Field")]
    public GameObject rightAnswerScore;

    [Header("Smiley")]
    public Transform smiley;
    private Image smileyFace;
    private Transform smileyCannons;
    private Transform smileyArms;
    public Sprite score0;
    public Sprite scoreLt50;
    public Sprite score50;
    public Sprite scoreGt50;
    public Sprite score100;

    private void Start()
    {
        smileyFace = smiley.Find("Face").GetComponent<Image>();
        smileyCannons = smiley.Find("Cannons");
        smileyArms = smiley.Find("Arms");
        txtBlack = new Color32(0x00, 0x00, 0x31, 0xFF);
        txtWhite = Color.white;
        txtRed = new Color32(0xE6, 0x1E, 0x31, 0xFF);
        txtGreen = new Color32(0x88, 0xD6, 0x0D, 0xFF);
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
            bgOverlay.gameObject.SetActive(false);
            FillFields();
            GetComponent<MenuManager>().PlayGame();
        }
    }

    //OBS! SCRIPT WILL NOT SHOW NEXT QUESTION IF THE 'QUESTION' ELEMENT IS THE SAME AS THE LAST!
    public void FillFields()
    {
        btnNext.GetComponent<Image>().sprite = nextFinalAnswer;
        foreach (Transform child in parAnswers)
        {
            child.GetComponent<Button>().enabled = true;
            child.GetComponent<Image>().sprite = btnBlack;
            child.GetChild(0).GetComponent<Text>().color = txtWhite;
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
                child.GetComponent<Image>().sprite = btnWhite;
                child.GetComponentInChildren<Text>().color = txtBlack;
            }
            else
            {
                child.GetComponent<Image>().sprite = btnBlack;
                child.GetComponentInChildren<Text>().color = txtWhite;
            }
        }
        btnNext.GetComponent<Button>().enabled = true;
    }
    public void CheckAnswer()
    {
        if (selectedAnswer.GetComponentInChildren<Text>().text == rightAnswer)
        {
            answeredRight++;
            selectedAnswer.GetComponent<Image>().sprite = btnGreen;
            selectedAnswer.GetComponentInChildren<Text>().color = txtWhite;
            btnNext.GetComponent<Image>().sprite = nextCorrect;
        }
        else
        {
            answeredWrong++;
            selectedAnswer.GetComponent<Image>().sprite = btnRed;
            selectedAnswer.GetComponentInChildren<Text>().color = txtWhite;
            btnNext.GetComponent<Image>().sprite = nextWrong;
            foreach (Transform child in parAnswers)
            {
                if (child.gameObject.GetComponentInChildren<Text>().text == rightAnswer && child.name != "Question")
                {
                    child.gameObject.GetComponent<Image>().sprite = btnGreen;
                    selectedAnswer.GetComponentInChildren<Text>().color = txtWhite;
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
        if(answeredRight == 0)
        {
            smileyFace.sprite = score0;
            smileyArms.gameObject.SetActive(true);
            smileyCannons.gameObject.SetActive(false);
            rightAnswerScore.GetComponentInChildren<Text>().color = txtRed;
            bgOverlay.gameObject.SetActive(true);
            bgOverlay.GetComponent<Image>().sprite = overlayBad;
        }
        else if (answeredRight == totalQuestions)
        {
            smileyFace.sprite = score100;
            smileyArms.gameObject.SetActive(false);
            smileyCannons.gameObject.SetActive(true);
            rightAnswerScore.GetComponentInChildren<Text>().color = txtGreen;
            bgOverlay.gameObject.SetActive(true);
            bgOverlay.GetComponent<Image>().sprite = overlayGood;
        }
        else if (answeredRight == totalQuestions / 2)
        {
            smileyFace.sprite = score50;
            smileyArms.gameObject.SetActive(false);
            smileyCannons.gameObject.SetActive(false);
            rightAnswerScore.GetComponentInChildren<Text>().color = txtBlack;
            bgOverlay.gameObject.SetActive(false);
        }
        else if (answeredRight < totalQuestions / 2)
        {
            smileyFace.sprite = scoreLt50;
            smileyArms.gameObject.SetActive(false);
            smileyCannons.gameObject.SetActive(false);
            rightAnswerScore.GetComponentInChildren<Text>().color = txtRed;
            bgOverlay.gameObject.SetActive(true);
            bgOverlay.GetComponent<Image>().sprite = overlayBad;
        }
        else if (answeredRight > totalQuestions / 2)
        {
            smileyFace.sprite = scoreGt50;
            smileyArms.gameObject.SetActive(false);
            smileyCannons.gameObject.SetActive(false);
            rightAnswerScore.GetComponentInChildren<Text>().color = txtGreen;
            bgOverlay.gameObject.SetActive(true);
            bgOverlay.GetComponent<Image>().sprite = overlayGood;
        }
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
