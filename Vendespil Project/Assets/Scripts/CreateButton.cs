using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateButton : MonoBehaviour
{

    public GameObject instantiatedButton;
    public GameObject panelEditMenu;
    public GameObject prefabButton;
    public Vector3 buttonPos;
    private static int questionNumber = 0;
    private float positionFinder = 81;

    public void InstantiateNewButton()
    {
        if(questionNumber == 0)
        {
            instantiatedButton = Instantiate(prefabButton, panelEditMenu.transform.Find("ScrollView").GetChild(0).GetChild(0));
            buttonPos = instantiatedButton.transform.position;
        }
        else
            instantiatedButton = Instantiate(prefabButton, new Vector3(buttonPos.x, buttonPos.y-(positionFinder*questionNumber), buttonPos.z), Quaternion.identity, panelEditMenu.transform.Find("ScrollView").GetChild(0).GetChild(0));
        instantiatedButton.name = prefabButton.name + questionNumber.ToString();
        instantiatedButton.GetComponent<Button>().onClick.AddListener(() => transform.root.GetComponent<MenuManager>().OpenQuestionPanel(EventSystem.current.currentSelectedGameObject.name));
        questionNumber++;
    }
}
