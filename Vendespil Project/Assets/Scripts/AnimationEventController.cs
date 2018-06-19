using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationEventController : MonoBehaviour
{
    private GameController gameController;
    private MenuManager menuManager;
    private Animator anim;

    private void Start()
    {
        gameController = transform.root.Find("Manager").GetComponent<GameController>();
        menuManager = transform.root.Find("Manager").GetComponent<MenuManager>();
        anim = GetComponent<Animator>();
    }

    public void SetQuestionsRandomOrder()
    {

        gameController.SetQuestionsRandomOrder();
        if (gameController.hasOrderedQuestions)
        {
            anim.SetBool("hasQuestions", true);
            anim.SetBool("play", true);
        }
        else
            anim.SetBool("hasQuestions", false);
    }

    public void GoToEditMenu()
    {
        anim.SetBool("edit", true);
    }

    public void QuitGame()
    {
        anim.SetBool("quit", true);
    }

    public void MainMenuClose()
    {
        if (anim.GetBool("play") == true)
        {
            anim.SetBool("play", false);
            menuManager.PlayGame();
        }
        else if (anim.GetBool("edit") == true)
        {
            anim.SetBool("edit", false);
            menuManager.GoToEditMenu();
        }
        else if (anim.GetBool("quit") == true)
        {
            anim.SetBool("quit", false);
            menuManager.QuitGame();
        }
        anim.SetBool("hasQuestions", false);
    }
}
