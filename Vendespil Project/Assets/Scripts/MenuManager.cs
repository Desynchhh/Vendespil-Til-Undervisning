using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject PanelMainMenu;
    public GameObject PanelEditMenu;
    public GameObject PanelCreateQuestion;
    public GameObject PanelEditQuestion;
    public GameObject PanelGame;
    public GameObject PanelQuestion;
    public GameObject PanelResults;

    public void PlayGame()
    {
        PanelQuestion.SetActive(false);
        PanelMainMenu.SetActive(false);
        PanelGame.SetActive(true);
    }

    public void GoToQuestion()
    {
        PanelGame.SetActive(false);
        PanelQuestion.SetActive(true);
    }

    public void GoToResults()
    {
        PanelGame.SetActive(false);
        PanelResults.SetActive(true);
    }

    public void GoToEditMenu()
    {
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
        PanelResults.SetActive(false);
        PanelGame.SetActive(false);
        PanelEditMenu.SetActive(false);
        PanelMainMenu.SetActive(true);
    }
   
    public void OpenQuestionPanel(string questionName)
    {
        GameObject panel = transform.parent.Find("PanelEditMenu").Find("ScrollView").GetChild(0).GetChild(0).Find(questionName).GetChild(1).gameObject;
        panel.SetActive(!panel.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
