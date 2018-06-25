using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;


public class CreateButton : MonoBehaviour
{
    [Header("Object To Spawn")]
    public GameObject prefabEditButton;
    private GameObject instantiatedButton;

    [Header("Positioning")]
    public GameObject contentEdit;

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

    public GameObject PanelWarning;
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
        instantiatedButton.name = prefabEditButton.name + nextId;
        Transform panel = instantiatedButton.transform.GetChild(1);
        GameObject button = instantiatedButton.transform.GetChild(0).gameObject;
        ButtonManager buttonData = button.GetComponent<ButtonManager>();
        buttonData.LoadInfo(nextId);
        button.GetComponent<Button>().onClick.AddListener(() => transform.root.Find("Manager").GetComponent<MenuManager>().OpenQuestionPanel(EventSystem.current.currentSelectedGameObject.transform.parent.name));
        buttonData.id = nextId;
        panel.GetChild(1).GetComponent<Button>().onClick.AddListener(() => DeleteButton(EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).gameObject.GetComponent<ButtonManager>().id));
        panel.GetChild(0).GetComponent<Button>().onClick.AddListener(() => transform.parent.Find("Manager").GetComponent<MenuManager>().GoToEditQuestion(EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<ButtonManager>().id));
        panel.GetChild(0).GetComponent<Button>().onClick.AddListener(() => GetQuestionInfo());
        button.GetComponentInChildren<Text>().text = buttonData.question;
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
        instantiatedButton.name = prefabEditButton.name + nextId;
        Transform panel = instantiatedButton.transform.GetChild(1);
        GameObject button = instantiatedButton.transform.GetChild(0).gameObject;
        ButtonManager buttonData = button.GetComponent<ButtonManager>();
        buttonData.LoadInfo(nextId);
        button.GetComponent<Button>().onClick.AddListener(() => transform.root.Find("Manager").GetComponent<MenuManager>().OpenQuestionPanel(EventSystem.current.currentSelectedGameObject.transform.parent.name));
        buttonData.id = nextId;
        panel.GetChild(1).GetComponent<Button>().onClick.AddListener(() => DeleteButton(EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).gameObject.GetComponent<ButtonManager>().id));
        panel.GetChild(0).GetComponent<Button>().onClick.AddListener(() => transform.parent.Find("Manager").GetComponent<MenuManager>().GoToEditQuestion(EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<ButtonManager>().id));
        panel.GetChild(0).GetComponent<Button>().onClick.AddListener(() => GetQuestionInfo());
        button.GetComponentInChildren<Text>().text = _question;
        buttonData.id = _id;
        buttonData.question = _question;
        buttonData.answer = _answer;
        buttonData.wrongAnswer1 = _w1;
        buttonData.wrongAnswer2 = _w2;
        buttonData.wrongAnswer3 = _w3;
        nextId = _id + 1;
        maxId = buttonData.id;
        Debug.Log("next id: " + nextId);
        Debug.Log("max id: " + maxId);
    }

    ButtonManager currentEditData;
    public void GetQuestionInfo()
    {
        Debug.Log(editId.ToString());
        currentEditData = contentEdit.transform.Find(prefabEditButton.name + editId.ToString()).GetChild(0).GetComponent<ButtonManager>();
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
            if (child.GetChild(0).GetComponent<ButtonManager>().id == editId)
            {
                child.GetComponentInChildren<Text>().text = _currentEditData.question;
            }
        }
    }

    public void DeleteButton(int deleteId)
    {
        foreach (Transform child in contentEdit.transform)
        {
            if (child.GetChild(0).GetComponent<ButtonManager>().id == deleteId)
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
                Destroy(child.gameObject);
            }
        }
        Debug.Log("next id: " + nextId);
        Debug.Log("max id: " + maxId);
    }

    public void DeleteAllWarning()
    {
        PanelWarning.transform.GetChild(0).GetComponentInChildren<Text>().text = "ADVARSEL!\n\nDu er ved at slette ALLE spørgsmål\nEr du sikker?";
        PanelWarning.transform.Find("btnOK").gameObject.SetActive(false);
        PanelWarning.transform.Find("btnYes").gameObject.SetActive(true);
        PanelWarning.transform.Find("btnYesQuit").gameObject.SetActive(false);
        PanelWarning.transform.Find("btnNo").gameObject.SetActive(true);
        PanelWarning.SetActive(true);
    }

    public void DeleteAll()
    {
        foreach (Transform child in contentEdit.transform)
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
