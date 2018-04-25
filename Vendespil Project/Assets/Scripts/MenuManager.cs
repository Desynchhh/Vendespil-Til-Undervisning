using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //public GameObject MenuGame;
    public GameObject MenuMain;
    public GameObject MenuEdit;

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);   //load game scene
        Debug.Log("loading game..");
        //MenuMain.SetActive(false);
        //MenuGame.SetActive(true);     //Activate game panel
    }

    public void GoToEdit()
    {
        MenuMain.SetActive(false);
        MenuEdit.SetActive(true);
    }

    public void GoToMain()
    {
        //MenuGame.SetActive(false);
        MenuEdit.SetActive(false);
        MenuMain.SetActive(true);
    }

    public void AddQuestion()
    {
        //Create a new button for the XML file
        //Edit the created question with EditQuestion() function
    }

    public void DeleteAllQuestions()
    {
        //Delete ALL question from XML file
    }

    public void OpenQuestionPanel(string goName)
    {
        if(transform.Find("PanelEditMenu").Find(goName).GetChild(1).gameObject.active == false)
        {
            transform.Find("PanelEditMenu").Find(goName).GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.Find("PanelEditMenu").Find(goName).GetChild(1).gameObject.SetActive(false);
        }
    }

    public void EditQuestion()
    {
        //Deactivate MenuEdit panel
        //Activate "edit question panel"
    }

    public void DeleteQuestion()
    {
        //Delete question from XML file
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
