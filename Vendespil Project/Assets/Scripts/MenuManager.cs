using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject PanelMainMenu;
    public GameObject PanelEditMenu;
    public GameObject PanelCreateMenu;
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
        PanelCreateMenu.SetActive(false);
        PanelMainMenu.SetActive(false);
        PanelEditMenu.SetActive(true);
    }
   
    public void GoToCreateMenu()
    {
        PanelEditMenu.SetActive(false);
        PanelCreateMenu.SetActive(true);
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
        if(transform.parent.Find("PanelEditMenu").Find("ScrollView").GetChild(0).GetChild(0).Find(questionName).GetChild(1).gameObject.activeInHierarchy == false)
        {
            transform.parent.Find("PanelEditMenu").Find("ScrollView").GetChild(0).GetChild(0).Find(questionName).GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.parent.Find("PanelEditMenu").Find("ScrollView").GetChild(0).GetChild(0).Find(questionName).GetChild(1).gameObject.SetActive(false);
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
