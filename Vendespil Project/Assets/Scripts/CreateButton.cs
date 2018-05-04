using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateButton : MonoBehaviour
{
    
    [Header("Object To Spawn")]
    public GameObject prefabEditButton;
    public GameObject prefabGameButton;
    private GameObject instantiatedButton;

    [Header("Positioning")]
    public GameObject contentEdit;
    public GameObject contentGame;

    public void SpawnEditButton()
    {
        instantiatedButton = Instantiate(prefabEditButton, contentEdit.transform);
        instantiatedButton.GetComponent<Button>().onClick.AddListener(() => transform.root.GetComponent<MenuManager>().OpenQuestionPanel(EventSystem.current.currentSelectedGameObject.name));
        //instantiatedButton.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => transform.root.GetComponent<EditQuestionsLocal>().EditQuestion(EventSystem.current.currentSelectedGameObject.transform.parent.parent.name));
        instantiatedButton.GetComponent<ButtonManager>().LoadInfo();
        instantiatedButton.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() => DeleteEditButton(EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject.GetComponent<ButtonManager>().id));
        instantiatedButton.name = prefabEditButton.name + instantiatedButton.GetComponent<ButtonManager>().id;
        instantiatedButton.GetComponentInChildren<Text>().text = instantiatedButton.GetComponent<ButtonManager>().question;
    }

    public void SpawnGameButton(int _id, string _question, string _answer, string _wrongAnswer1, string _wrongAnswer2, string _wrongAnswer3)
    {
        instantiatedButton = Instantiate(prefabGameButton, contentGame.transform);
        instantiatedButton.GetComponentInChildren<Text>().text = _question;
        instantiatedButton.name = prefabGameButton.name + _id;
        instantiatedButton.GetComponent<ButtonManager>().id = _id;
        instantiatedButton.GetComponent<ButtonManager>().question = _question;
        instantiatedButton.GetComponent<ButtonManager>().answer = _answer;
        instantiatedButton.GetComponent<ButtonManager>().wrongAnswer1 = _wrongAnswer1;
        instantiatedButton.GetComponent<ButtonManager>().wrongAnswer2 = _wrongAnswer2;
        instantiatedButton.GetComponent<ButtonManager>().wrongAnswer3 = _wrongAnswer3;
    }

    public void DeleteEditButton(int id)
    {
        foreach(Transform child in contentEdit.transform)
        {
            if(child.GetComponent<ButtonManager>().id == id)
            {
                Debug.Log(child.GetComponent<ButtonManager>().id);
                Debug.Log(id);
                transform.parent.Find("PanelCreateEditQuestion").GetComponent<Editor>().RemoveSingle(id);
                Debug.Log(transform.parent.Find("PanelCreateEditQuestion").GetComponent<Editor>().name);
                Destroy(child.gameObject);
            }
        }
        Debug.Log(transform.name);
    }

    public void DeleteAll()
    {
        foreach(Transform child in contentEdit.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
