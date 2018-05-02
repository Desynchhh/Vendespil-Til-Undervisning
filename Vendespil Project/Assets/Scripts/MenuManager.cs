using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject PanelMainMenu;
    public GameObject PanelEditMenu;
    public GameObject PanelGame;

    public void PlayGame()
    {
        PanelMainMenu.SetActive(false);
        PanelGame.SetActive(true);
    }

    public void GoToEditMenu()
    {
        PanelMainMenu.SetActive(false);
        PanelEditMenu.SetActive(true);
    }

    public void GoToMainMenu()
    {
        PanelGame.SetActive(false);
        PanelEditMenu.SetActive(false);
        PanelMainMenu.SetActive(true);
    }
   
    public void OpenQuestionPanel(string questionName)
    {
        if(transform.Find("PanelEditMenu").Find("ScrollView").GetChild(0).GetChild(0).Find(questionName).GetChild(1).gameObject.activeInHierarchy == false)
        {
            transform.Find("PanelEditMenu").Find("ScrollView").GetChild(0).GetChild(0).Find(questionName).GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.Find("PanelEditMenu").Find("ScrollView").GetChild(0).GetChild(0).Find(questionName).GetChild(1).gameObject.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
