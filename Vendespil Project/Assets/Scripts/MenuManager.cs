using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

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

    public void PlayGame()
    {
        PanelResults.SetActive(false);
        PanelMainMenu.SetActive(false);
        PanelQuestion.SetActive(true);
    }

    public void CloseWarning()
    {
        PanelWarning.SetActive(false);
    }

    public void GoToResults()
    {
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
        PanelResults.SetActive(false);
        PanelEditMenu.SetActive(false);
        PanelMainMenu.SetActive(true);
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
