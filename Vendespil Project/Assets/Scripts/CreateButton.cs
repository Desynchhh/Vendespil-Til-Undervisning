using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;


//BUG I DELETEBUTTON()
public class CreateButton : MonoBehaviour
{

    [Header("Object To Spawn")]
    public GameObject prefabEditButton;
    public GameObject prefabGameButton;
    private GameObject instantiatedButton;

    [Header("Positioning")]
    public GameObject contentEdit;
    public GameObject contentGame;

    [Header("Inputfields for Creating")]
    public InputField createQuestion;
    public InputField createRightAnswer;
    public InputField createWrongAnswer1;
    public InputField createWrongAnswer2;
    public InputField createWrongAnswer3;

    [Header("Inputfields for Editing")]
    public InputField editQuestion;
    public InputField editRightAnswer;
    public InputField editWrongAnswer1;
    public InputField editWrongAnswer2;
    public InputField editWrongAnswer3;

    private int nextId = 1;
    private int maxId;
    [HideInInspector]
    public int editId;
    private string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/Data.xml";
    }
    public void SpawnEditButton()
    {
        instantiatedButton = Instantiate(prefabEditButton, contentEdit.transform);
        ButtonManager buttonData = instantiatedButton.GetComponent<ButtonManager>();
        buttonData.LoadInfo(nextId);
        instantiatedButton.GetComponent<Button>().onClick.AddListener(() => transform.root.Find("Manager").GetComponent<MenuManager>().OpenQuestionPanel(EventSystem.current.currentSelectedGameObject.name));
        buttonData.id = nextId;
        instantiatedButton.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() => DeleteButton(EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject.GetComponent<ButtonManager>().id));
        instantiatedButton.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => transform.parent.Find("Manager").GetComponent<MenuManager>().GoToEditQuestion(EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetComponent<ButtonManager>().id));
        instantiatedButton.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => GetQuestionInfo());
        instantiatedButton.name = prefabEditButton.name + nextId;
        instantiatedButton.GetComponentInChildren<Text>().text = buttonData.question;
        SpawnGameButton(buttonData.id, buttonData.question, buttonData.answer, buttonData.wrongAnswer1, buttonData.wrongAnswer2, buttonData.wrongAnswer3);
        nextId++;
        maxId = buttonData.id;
        Debug.Log("next id: " + nextId);
        Debug.Log("max id: " + maxId);
        createQuestion.text = "";
        createRightAnswer.text = "";
        createWrongAnswer1.text = "";
        createWrongAnswer2.text = "";
        createWrongAnswer3.text = "";
    }

    public void SpawnEditButton(int _id, string _question, string _answer, string _w1, string _w2, string _w3)
    {
        instantiatedButton = Instantiate(prefabEditButton, contentEdit.transform);
        ButtonManager buttonData = instantiatedButton.GetComponent<ButtonManager>();
        instantiatedButton.GetComponent<Button>().onClick.AddListener(() => transform.root.Find("Manager").GetComponent<MenuManager>().OpenQuestionPanel(EventSystem.current.currentSelectedGameObject.name));
        instantiatedButton.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() => DeleteButton(EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject.GetComponent<ButtonManager>().id));
        instantiatedButton.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => transform.parent.Find("Manager").GetComponent<MenuManager>().GoToEditQuestion(EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetComponent<ButtonManager>().id));
        instantiatedButton.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => GetQuestionInfo());
        instantiatedButton.name = prefabEditButton.name + _id;
        instantiatedButton.GetComponentInChildren<Text>().text = _question;
        buttonData.id = _id;
        buttonData.question = _question;
        buttonData.answer = _answer;
        buttonData.wrongAnswer1 = _w1;
        buttonData.wrongAnswer2 = _w2;
        buttonData.wrongAnswer3 = _w3;
        SpawnGameButton(_id, _question, _answer, _w1, _w2, _w3);
        nextId = _id + 1;
        maxId = buttonData.id;
        Debug.Log("next id: " + nextId);
        Debug.Log("max id: " + maxId);
    }

    public void SpawnGameButton(int _id, string _question, string _answer, string _wrongAnswer1, string _wrongAnswer2, string _wrongAnswer3)
    {
        instantiatedButton = Instantiate(prefabGameButton, contentGame.transform);
        instantiatedButton.GetComponent<Button>().onClick.AddListener(() => transform.root.Find("Manager").GetComponent<GameController>().GoToQuestion());
        ButtonManager buttonData = instantiatedButton.GetComponent<ButtonManager>();
        instantiatedButton.GetComponentInChildren<Text>().text = _question;
        instantiatedButton.name = prefabGameButton.name + _id;
        buttonData.id = _id;
        buttonData.question = _question;
        buttonData.answer = _answer;
        buttonData.wrongAnswer1 = _wrongAnswer1;
        buttonData.wrongAnswer2 = _wrongAnswer2;
        buttonData.wrongAnswer3 = _wrongAnswer3;
    }

    ButtonManager currentEditData;
    public void GetQuestionInfo()
    {
        currentEditData = contentEdit.transform.Find(prefabEditButton.name + editId.ToString()).GetComponent<ButtonManager>();
        Debug.Log(currentEditData.id.ToString() + ", " + currentEditData.question.ToString());
        editQuestion.text = currentEditData.question;
        editRightAnswer.text = currentEditData.answer;
        editWrongAnswer1.text = currentEditData.wrongAnswer1;
        editWrongAnswer2.text = currentEditData.wrongAnswer2;
        editWrongAnswer3.text = currentEditData.wrongAnswer3;
    }

    public void EditButtons()
    {
        EditEditButton(currentEditData);
        EditGameButton(currentEditData);
    }

    private void EditEditButton(ButtonManager _currentEditData)
    {
        _currentEditData.question = editQuestion.text;
        _currentEditData.answer = editRightAnswer.text;
        _currentEditData.wrongAnswer1 = editWrongAnswer1.text;
        _currentEditData.wrongAnswer2 = editWrongAnswer2.text;
        _currentEditData.wrongAnswer3 = editWrongAnswer3.text;
        foreach (Transform child in contentEdit.transform)
        {
            if (child.GetComponent<ButtonManager>().id == editId)
            {
                child.GetComponentInChildren<Text>().text = _currentEditData.question;
            }
        }
    }

    private void EditGameButton(ButtonManager _currentEditData)
    {
        ButtonManager buttonData = contentGame.transform.Find(prefabGameButton.name + editId.ToString()).GetComponent<ButtonManager>();
        buttonData.question = _currentEditData.question;
        buttonData.answer = _currentEditData.answer;
        buttonData.wrongAnswer1 = _currentEditData.wrongAnswer1;
        buttonData.wrongAnswer2 = _currentEditData.wrongAnswer2;
        buttonData.wrongAnswer3 = _currentEditData.wrongAnswer3;
        foreach (Transform child in contentGame.transform)
        {
            if (child.GetComponent<ButtonManager>().id == editId)
            {
                child.GetComponentInChildren<Text>().text = buttonData.question;
            }
        }
    }

    public void DeleteButton(int deleteId)
    {
        foreach (Transform child in contentEdit.transform)
        {
            if (child.GetComponent<ButtonManager>().id == deleteId)
            {
                if (deleteId == maxId)
                {
                    if (File.Exists(filePath) && File.ReadAllLines(filePath).Length <= 12)
                    {
                        nextId = 1;
                        maxId = 0;
                        Debug.Log("maxId Reset!");
                    }
                    else
                    {
                        GetMaxID();
                    }
                }
                transform.root.Find("Manager").GetComponent<EditXML>().RemoveSingle(deleteId);
                Destroy(contentGame.transform.Find((prefabGameButton.name + deleteId).ToString()).gameObject);
                Destroy(child.gameObject);
            }
        }
        Debug.Log("next id: " + nextId);
        Debug.Log("max id: " + maxId);
    }

    public void DeleteAll()
    {
        foreach (Transform child in contentEdit.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in contentGame.transform)
        {
            Destroy(child.gameObject);
        }
        maxId = 0;
        nextId = 1;
        Debug.Log("next id: " + nextId);
        Debug.Log("max id: " + maxId);
    }

    private void GetMaxID()
    {
        maxId = -1;
        foreach (Transform child in contentEdit.transform)
        {
            maxId++;
        }
        nextId = maxId + 1;
    }

    public void CloseButtonAllPanels()
    {
        foreach (Transform child in contentEdit.transform)
        {
            child.GetChild(1).gameObject.SetActive(false);
        }
    }
}
