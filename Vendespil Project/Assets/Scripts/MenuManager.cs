using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject PanelMainMenu;
    public GameObject PanelEditMenu;
    public GameObject PanelCreateQuestion;
    public GameObject PanelEditQuestion;
    public GameObject PanelQuestion;
    public GameObject PanelResults;
    public GameObject PanelWarning;
    public GameObject PanelLogin;

    [Header("Background")]
    public GameObject bgOverlay;
    public Image background;
    public Sprite bgDefault;
    public Sprite bgGame;

    public void PlayGame()
    {
        PanelResults.SetActive(false);
        PanelMainMenu.SetActive(false);
        PanelQuestion.SetActive(true);
        background.sprite = bgGame;
    }

    public void CloseWarning()
    {
        PanelWarning.SetActive(false);
    }

    public void GoToResults()
    {
        background.sprite = bgDefault;
        PanelQuestion.SetActive(false);
        PanelResults.SetActive(true);
    }

    public void GoToEditMenu()
    {
        PanelQuestion.SetActive(false);
        PanelCreateQuestion.SetActive(false);
        PanelEditQuestion.SetActive(false);
        PanelMainMenu.SetActive(false);
        PanelEditMenu.SetActive(true);
    }

    public void GoToCreateQuestion()
    {
        PanelEditMenu.SetActive(false);
        PanelCreateQuestion.SetActive(true);
    }

    public void GoToEditQuestion(int editId)
    {
        transform.GetComponent<EditXML>().EditID = editId;
        transform.parent.Find("PanelEditMenu").GetComponent<CreateButton>().editId = editId;
        PanelEditMenu.SetActive(false);
        PanelEditQuestion.SetActive(true);
    }

    public void GoToMainMenu()
    {
        //bgOverlay.SetActive(false);
        //PanelResults.SetActive(false);
        //PanelEditMenu.SetActive(false);
        //PanelLogin.SetActive(false);
        // PanelMainMenu.SetActive(true);
    }
   
    //public void OpenQuestionPanel(string questionName)
    //{
    //    GameObject panel = PanelEditMenu.transform.Find("ScrollView").GetChild(0).GetChild(0).Find(questionName).GetChild(1).gameObject;
    //    panel.SetActive(!panel.activeSelf);
    //    //Debug.Log(PanelEditMenu.transform.Find("ScrollView").GetChild(0).GetChild(0).Find(questionName).name);
    //}

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
