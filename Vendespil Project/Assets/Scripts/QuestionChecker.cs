using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionChecker : MonoBehaviour
{
    [Header("Variables")]
    private bool passedCheck = false;
    [Header("Too lazy to move CreateButton script")]
    public GameObject PanelEditMenu;

    [Header("Warning Panel")]
    public GameObject PanelWarning;

    [Header("Create Menu")]
    public Transform createParFields;
    public InputField createQuestion;
    public InputField createAnswer;
    public InputField createWrongAnswer1;
    public InputField createWrongAnswer2;
    public InputField createWrongAnswer3;

    [Header("Edit Menu")]
    public Transform editParFields;
    public InputField editQuestion;
    public InputField editAnswer;
    public InputField editWrongAnswer1;
    public InputField editWrongAnswer2;
    public InputField editWrongAnswer3;

    public void CheckCreate()
    {
        CreateCheckDupes();
        if (passedCheck)
            CreateCheckBlank();
    }

    public void CheckEdit()
    {
        EditCheckDupes();
        if (passedCheck)
            EditCheckBlank();
    }

    private void CreateCheckDupes()
    {
        List<string> itemsToCheck = new List<string>();
        itemsToCheck.Add(createQuestion.text);
        itemsToCheck.Add(createAnswer.text);
        itemsToCheck.Add(createWrongAnswer1.text);
        itemsToCheck.Add(createWrongAnswer2.text);
        itemsToCheck.Add(createWrongAnswer3.text);

        int counter = 0;
        for (int i = 0; i <= itemsToCheck.Count - 1; i++)
        {
            Debug.Log(counter);
            if (itemsToCheck[i] == itemsToCheck[0] && itemsToCheck[0] != "")
                counter++;
            if (itemsToCheck[i] == itemsToCheck[1] && itemsToCheck[1] != "")
                counter++;
            if (itemsToCheck[i] == itemsToCheck[2] && itemsToCheck[2] != "")
                counter++;
            if (itemsToCheck[i] == itemsToCheck[3] && itemsToCheck[3] != "")
                counter++;
            if (itemsToCheck[i] == itemsToCheck[4] && itemsToCheck[4] != "")
                counter++;
            Debug.Log(counter);
            if (counter >= 2)
            {
                Debug.Log("Dupe detected!");
                passedCheck = false;
                PanelWarning.transform.Find("WarningText").Find("Text").GetComponent<Text>().fontSize = 58;
                PanelWarning.transform.Find("WarningText").Find("Text").GetComponent<Text>().text = "DU KAN IKKE HAVE 2 ELLER FLERE ENS SVARMULIGHEDER!";
                PanelWarning.transform.Find("btnOK").gameObject.SetActive(true);
                PanelWarning.transform.Find("btnYesAll").gameObject.SetActive(false);
                PanelWarning.transform.Find("btnYesSingle").gameObject.SetActive(false);
                PanelWarning.transform.Find("btnNo").gameObject.SetActive(false);
                PanelWarning.SetActive(true);
                break;
            }
            else
            {
                counter = 0;
                passedCheck = true;
            }
        }
    }

    private void EditCheckDupes()
    {
        List<string> itemsToCheck = new List<string>();
        itemsToCheck.Add(editQuestion.text);
        itemsToCheck.Add(editAnswer.text);
        itemsToCheck.Add(editWrongAnswer1.text);
        itemsToCheck.Add(editWrongAnswer2.text);
        itemsToCheck.Add(editWrongAnswer3.text);

        int counter = 0;
        for (int i = 0; i <= itemsToCheck.Count - 1; i++)
        {
            if (itemsToCheck[i] == itemsToCheck[0] && itemsToCheck[0] != "")
                counter++;
            if (itemsToCheck[i] == itemsToCheck[1] && itemsToCheck[1] != "")
                counter++;
            if (itemsToCheck[i] == itemsToCheck[2] && itemsToCheck[2] != "")
                counter++;
            if (itemsToCheck[i] == itemsToCheck[3] && itemsToCheck[3] != "")
                counter++;
            if (itemsToCheck[i] == itemsToCheck[4] && itemsToCheck[4] != "")
                counter++;
            if (counter >= 2)
            {
                Debug.Log("Dupe detected!");
                passedCheck = false;
                PanelWarning.transform.Find("WarningText").Find("Text").GetComponent<Text>().fontSize = 58;
                PanelWarning.transform.Find("WarningText").Find("Text").GetComponent<Text>().text = "DU KAN IKKE HAVE 2 ELLER FLERE ENS SVARMULIGHEDER!";
                PanelWarning.transform.Find("btnOK").gameObject.SetActive(true);
                PanelWarning.transform.Find("btnYesAll").gameObject.SetActive(false);
                PanelWarning.transform.Find("btnYesSingle").gameObject.SetActive(false);
                PanelWarning.transform.Find("btnNo").gameObject.SetActive(false);
                PanelWarning.SetActive(true);
                break;
            }
            else
            {
                counter = 0;
                passedCheck = true;
            }
        }
    }

    private void CreateCheckBlank()
    {
        foreach (Transform child in createParFields)    //this replaces the if statement down below
        {
            if (child.GetComponent<InputField>().text == "")
            {
                Debug.Log("Blank detected!");
                passedCheck = false;
                PanelWarning.transform.Find("WarningText").Find("Text").GetComponent<Text>().fontSize = 50;
                PanelWarning.transform.Find("WarningText").Find("Text").GetComponent<Text>().text = "DER SKAL VÆRE MINIMUM 2 SVARMULIGHEDER OG 1 SPØRGSMÅL";
                PanelWarning.transform.Find("btnOK").gameObject.SetActive(true);
                PanelWarning.transform.Find("btnYesAll").gameObject.SetActive(false);
                PanelWarning.transform.Find("btnYesSingle").gameObject.SetActive(false);
                PanelWarning.transform.Find("btnNo").gameObject.SetActive(false);
                PanelWarning.SetActive(true);
            }
            else
            {
                passedCheck = true;
                break;
            }
        }
        //if (createParFields.transform.GetChild(0).GetComponent<InputField>().text == "" || createParFields.transform.GetChild(1).GetComponent<InputField>().text == "" || createParFields.transform.GetChild(2).GetComponent<InputField>().text == "")
        //{
        //}
        if (passedCheck)
        {
            PanelEditMenu.GetComponent<CreateButton>().SpawnEditButton();
            PanelEditMenu.GetComponent<CreateButton>().ClearFields();
            GetComponent<MenuManager>().GoToEditMenu();
        }
    }

    private void EditCheckBlank()
    {
        foreach (Transform child in editParFields)  //this replaces the if statement down below
        {
            if (child.GetComponent<InputField>().text == "")
            {
                Debug.Log("Blank detected!");
                passedCheck = false;
                PanelWarning.transform.Find("WarningText").Find("Text").GetComponent<Text>().fontSize = 50;
                PanelWarning.transform.Find("WarningText").Find("Text").GetComponent<Text>().text = "DER SKAL VÆRE MINIMUM 2 SVARMULIGHEDER OG 1 SPØRGSMÅL";
                PanelWarning.transform.Find("btnOK").gameObject.SetActive(true);
                PanelWarning.transform.Find("btnYesAll").gameObject.SetActive(false);
                PanelWarning.transform.Find("btnYesSingle").gameObject.SetActive(false);
                PanelWarning.transform.Find("btnNo").gameObject.SetActive(false);
                PanelWarning.SetActive(true);
            }
            else
            {
                passedCheck = true;
                break;
            }
        }
        //if (editParFields.transform.GetChild(0).GetComponent<InputField>().text == "" || editParFields.transform.GetChild(1).GetComponent<InputField>().text == "" || editParFields.transform.GetChild(2).GetComponent<InputField>().text == "")
        //{
        //}

        if (passedCheck)
        {
            PanelEditMenu.GetComponent<CreateButton>().EditButtons();
            GetComponent<MenuManager>().GoToEditMenu();
        }
    }
}
