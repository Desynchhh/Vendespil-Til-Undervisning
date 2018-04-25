using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateButton : MonoBehaviour
{
    public GameObject panelEditMenu;
    public GameObject prefabButton;
    private GameObject instantiatedButton;
    public static int questionNumber = 1;
    public float positionFinder = 81;

    public void InstantiateNewButton()
    {
        instantiatedButton = Instantiate(prefabButton, new Vector3(prefabButton.transform.position.x, prefabButton.transform.position.y - (positionFinder*questionNumber), 0), Quaternion.identity);
        instantiatedButton.name = prefabButton.name + questionNumber;
        instantiatedButton.transform.SetParent(panelEditMenu.transform);
        instantiatedButton.GetComponent<Button>().onClick.AddListener(() => transform.Find(panelEditMenu.name).parent.GetComponent<MenuManager>().OpenQuestionPanel(instantiatedButton.name));
        questionNumber++;
    }
}
