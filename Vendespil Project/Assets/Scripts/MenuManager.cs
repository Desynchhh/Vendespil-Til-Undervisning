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
    public GameObject PanelQuestion;
    public GameObject PanelResults;

    public void PlayGame()
    {
        PanelMainMenu.SetActive(false);
        PanelQuestion.SetActive(true);
    }

    public void GoToResults()
    {
        PanelQuestion.SetActive(false);
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
        PanelEditMenu.SetActive(false);
        PanelMainMenu.SetActive(true);
    }
   
    public void OpenQuestionPanel(string questionName)
    {
        GameObject panel = transform.parent.Find("PanelEditMenu").Find("ScrollView").GetChild(0).GetChild(0).Find(questionName).GetChild(1).gameObject;
        panel.SetActive(!panel.activeSelf);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
