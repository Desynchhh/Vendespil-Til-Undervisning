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
    public GameObject content;
    //private Vector3 buttonPos;
    //private float positionFinder = 81;
    public int questionNumber = 0;  //Set to the IdNumber from XML file

    public void InstantiateNewButton()
    {
        instantiatedButton = Instantiate(prefabButton, content.transform);
        instantiatedButton.name = prefabButton.name + questionNumber.ToString();
        instantiatedButton.GetComponent<Button>().onClick.AddListener(() => transform.root.GetComponent<MenuManager>().OpenQuestionPanel(EventSystem.current.currentSelectedGameObject.name));
        instantiatedButton.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => transform.root.GetComponent<EditQuestionsLocal>().EditQuestion(EventSystem.current.currentSelectedGameObject.transform.parent.parent.name));
        instantiatedButton.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() => transform.root.GetComponent<EditQuestionsLocal>().DeleteQuestion(EventSystem.current.currentSelectedGameObject.transform.parent.parent.name));
        questionNumber++;
    }
}
