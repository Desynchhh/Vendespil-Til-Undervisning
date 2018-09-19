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
    private int userID;

    [Header("Sorting Lists")]
    public List<MakeList.LoadedQuestion> selectedQuestions = new List<MakeList.LoadedQuestion>();
    public List<MakeList.LoadedQuestion> unorderedQuestions = new List<MakeList.LoadedQuestion>();
    public List<MakeList.LoadedQuestion> orderedQuestions = new List<MakeList.LoadedQuestion>();
    private string rightAnswer;

    [Header("Variables")]
    private GameObject selectedAnswer;
    private int totalQuestions;
    private int totalAnswered;
    private int answeredRight;
    private int answeredWrong;

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

    [Header("Results Field")]
    public GameObject panelResult;

    [Header("Smiley")]
    public Image smileyFace;
    public Transform smileyCannons;
    public Transform smileyArms;
    public Sprite score0;
    public Sprite scoreLt50;
    public Sprite score50;
    public Sprite scoreGt50;
    public Sprite score100;

    private void Start()
    {
        txtBlack = new Color32(0x00, 0x00, 0x31, 0xFF);
        txtWhite = Color.white;
        txtRed = new Color32(0xE6, 0x1E, 0x31, 0xFF);
        txtGreen = new Color32(0x88, 0xD6, 0x0D, 0xFF);
    }

    public void SetQuestions(List <MakeList.LoadedQuestion> questions)
    {
        selectedQuestions = questions;
    }

    public void SetQuestionsRandomOrder()
    {
        orderedQuestions.Clear();

        foreach (var question in selectedQuestions)
        {
            unorderedQuestions.Add(question);
            totalQuestions++;
        }

        if (totalQuestions == 0)
        {
            PanelWarning.transform.Find("WarningText").Find("Text").GetComponent<Text>().fontSize = 46;
            PanelWarning.transform.Find("WarningText").Find("Text").GetComponent<Text>().text = "DER ER IKKE OPRETTET NOGEN SPØRGSMÅL!\n\n OPRET MINDST ÉT SPØRGSMÅL FOR AT SPILLE";
            PanelWarning.transform.Find("btnOK").gameObject.SetActive(true);
            PanelWarning.transform.Find("btnYesAll").gameObject.SetActive(false);
            PanelWarning.transform.Find("btnYesSingle").gameObject.SetActive(false);
            PanelWarning.transform.Find("btnNo").gameObject.SetActive(false);
            PanelWarning.SetActive(true);
        }
        else
        {
            for (int i = 0; i <= totalQuestions - 1; i++)
            {
                int randomIndex = Random.Range(0, unorderedQuestions.Count);
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
        btnNext.GetComponent<Button>().enabled = false;
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

        else if (questionField.GetComponentInChildren<Text>().text != orderedQuestions[totalAnswered].question)
        {
            progress.text = totalAnswered + 1 + "/" + totalQuestions;
            
            rightAnswer = orderedQuestions[totalAnswered].rightAnswer;
            questionField.GetComponentInChildren<Text>().text = orderedQuestions[totalAnswered].question;
            SetRandomOrder(orderedQuestions[totalAnswered].rightAnswer, orderedQuestions[totalAnswered].wrongAnswer1, orderedQuestions[totalAnswered].wrongAnswer2, orderedQuestions[totalAnswered].wrongAnswer3);
        }
    }

    public void SelectAnswer()
    {
        selectedAnswer = EventSystem.current.currentSelectedGameObject;
        foreach (Transform child in parAnswers)
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
        btnNext.GetComponent<Button>().enabled = false;
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
        bgOverlay.gameObject.SetActive(true);
        if (answeredRight == 0)
        {
            smileyFace.sprite = score0;
            smileyArms.gameObject.SetActive(true);
            smileyCannons.gameObject.SetActive(false);
            panelResult.GetComponentInChildren<Text>().color = txtRed;
            bgOverlay.GetComponent<Image>().sprite = overlayBad;
        }
        else if (answeredRight == totalQuestions)
        {
            smileyFace.sprite = score100;
            smileyArms.gameObject.SetActive(false);
            smileyCannons.gameObject.SetActive(true);
            panelResult.GetComponentInChildren<Text>().color = txtGreen;
            bgOverlay.GetComponent<Image>().sprite = overlayGood;
        }
        else if (answeredRight < totalQuestions / 2)
        {
            smileyFace.sprite = scoreLt50;
            smileyArms.gameObject.SetActive(false);
            smileyCannons.gameObject.SetActive(false);
            panelResult.GetComponentInChildren<Text>().color = txtRed;
            bgOverlay.GetComponent<Image>().sprite = overlayBad;
        }
        else if (answeredRight > totalQuestions / 2)
        {
            smileyFace.sprite = scoreGt50;
            smileyArms.gameObject.SetActive(false);
            smileyCannons.gameObject.SetActive(false);
            panelResult.GetComponentInChildren<Text>().color = txtGreen;
            bgOverlay.GetComponent<Image>().sprite = overlayGood;
        }
        else if (answeredRight == totalQuestions / 2)
        {
            smileyFace.sprite = score50;
            smileyArms.gameObject.SetActive(false);
            smileyCannons.gameObject.SetActive(false);
            panelResult.GetComponentInChildren<Text>().color = txtBlack;
            bgOverlay.gameObject.SetActive(false);
        }
        panelResult.GetComponentInChildren<Text>().text = string.Format("DU FIK\n {0} / {1}\n RIGTIGE!", answeredRight, totalQuestions);
    }

    private void SetRandomOrder(string rightAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3)
    {
        List<string> answersList = new List<string>();
        List<string> randomOrderList = new List<string>();
        answersList.Add(rightAnswer);
        answersList.Add(wrongAnswer1);
        if(wrongAnswer2 != "")
            answersList.Add(wrongAnswer2);
        if (wrongAnswer3 != "")
            answersList.Add(wrongAnswer3);
        int listCount = answersList.Count-1;
        for (int i = 0; i <= listCount; i++)
        {
            int randomIndex = Random.Range(0, answersList.Count);
            randomOrderList.Add(answersList[randomIndex]);
            answersList.RemoveAt(randomIndex);
        }
        btnAnswer1.GetComponentInChildren<Text>().text = randomOrderList[0];
        btnAnswer2.GetComponentInChildren<Text>().text = randomOrderList[1];
        if (listCount > 1)
        {
            btnAnswer3.GetComponentInChildren<Text>().text = randomOrderList[2];
            btnAnswer3.SetActive(true);
        }
        else
            btnAnswer3.SetActive(false);
        if (listCount > 2)
        {
            btnAnswer4.GetComponentInChildren<Text>().text = randomOrderList[3];
            btnAnswer4.SetActive(true);
        }
        else
            btnAnswer4.SetActive(false);
    }

    public void Reset()
    {
        unorderedQuestions.Clear();
        orderedQuestions.Clear();
        totalAnswered = 0;
        totalQuestions = 0;
    }
}
