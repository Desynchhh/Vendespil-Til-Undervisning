using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateButton : MonoBehaviour
{
    
    [Header("Object To Spawn")]
    public GameObject prefabButton;
    private GameObject instantiatedButton;

    [Header("Positioning")]
    public GameObject contentEdit;
    public GameObject contentGame;

    public void SpawnEditButton()
    {
        instantiatedButton = Instantiate(prefabButton, contentEdit.transform);
        instantiatedButton.GetComponent<Button>().onClick.AddListener(() => transform.root.GetComponent<MenuManager>().OpenQuestionPanel(EventSystem.current.currentSelectedGameObject.name));
        //instantiatedButton.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => transform.root.GetComponent<EditQuestionsLocal>().EditQuestion(EventSystem.current.currentSelectedGameObject.transform.parent.parent.name));
        //instantiatedButton.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() => transform.root.GetComponent<EditQuestionsLocal>().DeleteQuestion(EventSystem.current.currentSelectedGameObject.transform.parent.parent.name));
        instantiatedButton.GetComponent<ButtonManager>().LoadInfo();
        instantiatedButton.name = prefabButton.name + instantiatedButton.GetComponent<ButtonManager>().id;
        instantiatedButton.GetComponentInChildren<Text>().text = instantiatedButton.GetComponent<ButtonManager>().question;
    }

    public void SpawnGameButton()
    {

    }
}
