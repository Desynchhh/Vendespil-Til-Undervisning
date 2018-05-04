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
        instantiatedButton.GetComponent<Button>().onClick.AddListener(() => transform.root.Find("Manager").GetComponent<MenuManager>().OpenQuestionPanel(EventSystem.current.currentSelectedGameObject.name));
        instantiatedButton.GetComponent<ButtonManager>().LoadInfo();
        instantiatedButton.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() => DeleteButton(EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject.GetComponent<ButtonManager>().id));
        instantiatedButton.name = prefabEditButton.name + instantiatedButton.GetComponent<ButtonManager>().id;
        //instantiatedButton.name = prefabGameButton.name + transform.root.Find("Manager").GetComponent<Editor>().Read().ToString();
        instantiatedButton.GetComponentInChildren<Text>().text = instantiatedButton.GetComponent<ButtonManager>().question;
        SpawnGameButton(instantiatedButton.GetComponent<ButtonManager>().id, instantiatedButton.GetComponent<ButtonManager>().question, instantiatedButton.GetComponent<ButtonManager>().answer, instantiatedButton.GetComponent<ButtonManager>().wrongAnswer1, instantiatedButton.GetComponent<ButtonManager>().wrongAnswer2, instantiatedButton.GetComponent<ButtonManager>().wrongAnswer3);
    }

    public void SpawnGameButton(int _id, string _question, string _answer, string _wrongAnswer1, string _wrongAnswer2, string _wrongAnswer3)
    {
        instantiatedButton = Instantiate(prefabGameButton, contentGame.transform);
        instantiatedButton.GetComponentInChildren<Text>().text = _question;
        instantiatedButton.name = prefabGameButton.name + _id;
        //instantiatedButton.name = prefabGameButton.name + transform.root.Find("Manager").GetComponent<Editor>().Read().ToString();
        instantiatedButton.GetComponent<ButtonManager>().id = _id;
        instantiatedButton.GetComponent<ButtonManager>().question = _question;
        instantiatedButton.GetComponent<ButtonManager>().answer = _answer;
        instantiatedButton.GetComponent<ButtonManager>().wrongAnswer1 = _wrongAnswer1;
        instantiatedButton.GetComponent<ButtonManager>().wrongAnswer2 = _wrongAnswer2;
        instantiatedButton.GetComponent<ButtonManager>().wrongAnswer3 = _wrongAnswer3;
    }

    public void DeleteButton(int id)
    {
        foreach(Transform child in contentEdit.transform)
        {
            if(child.GetComponent<ButtonManager>().id == id)
            {
                Debug.Log(child.GetComponent<ButtonManager>().id);
                Debug.Log(id);
                transform.root.Find("Manager").GetComponent<Editor>().RemoveSingle(id);
                Destroy(child.gameObject);
            }
            if(child.GetComponent<ButtonManager>().id >= id)
            {
                child.GetComponent<ButtonManager>().id--;
            }
        }
        foreach(Transform child in contentGame.transform)
        {
            if (child.GetComponent<ButtonManager>().id == id)
                Destroy(child.gameObject);
        }
    }

    public void DeleteAll()
    {
        foreach(Transform child in contentEdit.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in contentGame.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
