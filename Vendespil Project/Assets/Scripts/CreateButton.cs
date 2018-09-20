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

    [Header("ID")]
    public GameObject PanelWarning;
    private int nextId = 1;
    private int maxId;

    [Header("User Questions")]
    public List<QuestionEditor.QuestionForEdit> questions = new List<QuestionEditor.QuestionForEdit>();

    [HideInInspector]
    public int editId;

    public void CreateQuestionButton()
    {
        //Spawn button for editing
        instantiatedButton = (GameObject)Instantiate(prefabEditButton, contentEdit.transform);
        Transform panel = instantiatedButton.transform.GetChild(1);
        GameObject button = instantiatedButton.transform.GetChild(0).gameObject;

        //Assign data to the button
        ButtonManager buttonData = button.GetComponent<ButtonManager>();
        buttonData.id = nextId;
        buttonData.question = createQuestion.text;
        buttonData.rightAnswer = createRightAnswer.text;
        buttonData.wrongAnswer1 = createWrongAnswer1.text;
        buttonData.wrongAnswer2 = createWrongAnswer2.text;
        buttonData.wrongAnswer3 = createWrongAnswer3.text;

        instantiatedButton.name = prefabEditButton.name + buttonData.id.ToString();
        instantiatedButton.GetComponentInChildren<Text>().text = buttonData.question;

        //Assign onClickEvents on the sub-buttons
        panel.GetChild(1).GetComponent<Button>().onClick.AddListener(() => DeleteSingleWarning(EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).gameObject.GetComponent<ButtonManager>().id));
        panel.GetChild(0).GetComponent<Button>().onClick.AddListener(() => transform.parent.Find("Manager").GetComponent<MenuManager>().GoToEditQuestion(EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<ButtonManager>().id));
        panel.GetChild(0).GetComponent<Button>().onClick.AddListener(() => GetQuestionInfo());

        //Create the question in the database
        transform.parent.Find("PanelMainMenu").GetComponent<QuestionEditor>().CreateQuestion(buttonData.question, buttonData.rightAnswer, buttonData.wrongAnswer1, buttonData.wrongAnswer2, buttonData.wrongAnswer3);

        //Handle local IDs (Remove?)
        nextId++;
        maxId = buttonData.id;
    }

    public void LoadQuestionButtons()
    {
        questions = transform.parent.Find("PanelMainMenu").GetComponent<QuestionEditor>().questions;
        foreach (var question in questions)
        {
            //Spawn button for editing
            instantiatedButton = (GameObject)Instantiate(prefabEditButton, contentEdit.transform);
            Transform panel = instantiatedButton.transform.GetChild(1);
            GameObject button = instantiatedButton.transform.GetChild(0).gameObject;

            //Assign data to the button
            ButtonManager buttonData = button.GetComponent<ButtonManager>();
            buttonData.id = question.IdNumber;
            buttonData.question = question.question;
            buttonData.rightAnswer = question.rightAnswer;
            buttonData.wrongAnswer1 = question.wrongAnswer1;
            buttonData.wrongAnswer2 = question.wrongAnswer2;
            buttonData.wrongAnswer3 = question.wrongAnswer3;

            instantiatedButton.name = prefabEditButton.name + buttonData.id;
            instantiatedButton.GetComponentInChildren<Text>().text = buttonData.question;

            //Assign onClickEvents on the sub-buttons
            panel.GetChild(1).GetComponent<Button>().onClick.AddListener(() => DeleteSingleWarning(EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).gameObject.GetComponent<ButtonManager>().id));
            panel.GetChild(0).GetComponent<Button>().onClick.AddListener(() => transform.parent.Find("Manager").GetComponent<MenuManager>().GoToEditQuestion(EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).GetComponent<ButtonManager>().id));
            panel.GetChild(0).GetComponent<Button>().onClick.AddListener(() => GetQuestionInfo());

            //Handle local IDs (Remove?)
            maxId = buttonData.id;
            nextId = maxId + 1;
            Debug.Log("next id: " + nextId);
            Debug.Log("max id: " + maxId);
        }
    }

    ButtonManager currentEditData;
    public void GetQuestionInfo()
    {
        Debug.Log(editId.ToString());
        currentEditData = contentEdit.transform.Find(prefabEditButton.name + editId.ToString()).GetChild(0).GetComponent<ButtonManager>();
        Debug.Log(currentEditData.id.ToString() + ", " + currentEditData.question.ToString());
        editQuestion.text = currentEditData.question;
        editRightAnswer.text = currentEditData.rightAnswer;
        editWrongAnswer1.text = currentEditData.wrongAnswer1;
        editWrongAnswer2.text = currentEditData.wrongAnswer2;
        editWrongAnswer3.text = currentEditData.wrongAnswer3;
    }

    public void EditButtons()
    {
        EditButtonData(currentEditData);
    }

    private void EditButtonData(ButtonManager _currentEditData)
    {
        _currentEditData.question = editQuestion.text;
        _currentEditData.rightAnswer = editRightAnswer.text;
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
        transform.parent.Find("PanelMainMenu").GetComponent<QuestionEditor>().EditQuestion(_currentEditData.id, _currentEditData.question, _currentEditData.rightAnswer, _currentEditData.wrongAnswer1, _currentEditData.wrongAnswer2, _currentEditData.wrongAnswer3);

    }

    public void ClearFields()
    {
        createQuestion.text = "";
        createRightAnswer.text = "";
        createWrongAnswer1.text = "";
        createWrongAnswer2.text = "";
        createWrongAnswer3.text = "";
    }

    public void DeleteAllWarning()
    {
        PanelWarning.transform.Find("WarningText").Find("Text").GetComponent<Text>().fontSize = 46;
        PanelWarning.transform.Find("WarningText").GetComponentInChildren<Text>().text = "ADVARSEL!\n\nDu er ved at slette ALLE spørgsmål\nEr du sikker?";
        PanelWarning.transform.Find("btnOK").gameObject.SetActive(false);
        PanelWarning.transform.Find("btnYesAll").gameObject.SetActive(true);
        PanelWarning.transform.Find("btnYesSingle").gameObject.SetActive(false);
        PanelWarning.transform.Find("btnNo").gameObject.SetActive(true);
        PanelWarning.SetActive(true);
    }

    public void DeleteSingleWarning(int btnId)
    {
        PanelWarning.transform.Find("btnYesSingle").GetComponent<Button>().onClick.RemoveAllListeners();
        PanelWarning.transform.Find("btnYesSingle").GetComponent<Button>().onClick.AddListener(() => DeleteSingle(btnId));
        PanelWarning.transform.Find("btnYesSingle").GetComponent<Button>().onClick.AddListener(() => transform.root.Find("Manager").GetComponent<MenuManager>().CloseWarning());
        PanelWarning.transform.Find("WarningText").Find("Text").GetComponent<Text>().fontSize = 44;
        PanelWarning.transform.Find("WarningText").GetComponentInChildren<Text>().text = "ADVARSEL!\n\nDu er ved at slette ÉT spørgsmål\nEr du sikker?";
        PanelWarning.transform.Find("btnOK").gameObject.SetActive(false);
        PanelWarning.transform.Find("btnYesAll").gameObject.SetActive(false);
        PanelWarning.transform.Find("btnYesSingle").gameObject.SetActive(true);
        PanelWarning.transform.Find("btnNo").gameObject.SetActive(true);
        PanelWarning.SetActive(true);
    }

    public void DeleteAll()
    {
        foreach (Transform child in contentEdit.transform)
        {
            Destroy(child.gameObject);
        }
        transform.parent.Find("PanelMainMenu").GetComponent<QuestionEditor>().RemoveAll();
        maxId = 0;
        nextId = 1;
        Debug.Log("next id: " + nextId);
        Debug.Log("max id: " + maxId);
    }

    public void DeleteSingle(int deleteId)
    {
        foreach (Transform child in contentEdit.transform)
        {
            if (child.GetChild(0).GetComponent<ButtonManager>().id == deleteId)
            {
                if (deleteId == maxId)
                {
                    if (contentEdit.transform.childCount > 1)
                    {
                        Transform lastChild = contentEdit.transform.GetChild(contentEdit.transform.childCount - 2).GetChild(0);
                        Debug.Log(lastChild.name);
                        maxId = lastChild.GetComponent<ButtonManager>().id;
                    }

                    else
                    {
                        maxId = 0;
                    }
                    nextId = maxId + 1;
                }
                else
                {
                    GetMaxID();
                }
                transform.parent.Find("PanelMainMenu").GetComponent<QuestionEditor>().RemoveSingle(deleteId);
                Destroy(child.gameObject);
            }
        }
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
}
