using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginWarning : MonoBehaviour {

    public GameObject WarningPanel;
    public Button Button;
    public Text TextBox;
    private bool SuccessfulTemp;

    //
    //GameObject.Find("Manager").GetComponent<LoginWarning>().DisplayWarning(bool, string);

    private void Start()
    {
        Button.onClick.AddListener(RemoveWarning);
    }


    public void DisplayWarning(bool Successful, string RealName)
    {
        if(Successful == true)
        {
            TextBox.text = "Velkommen tilbage " + RealName;
        }
        else if(Successful != true)
        {
            TextBox.text = "ADVARSEL! \n Dit brugernavn eller kode er forkert";
        }
        WarningPanel.SetActive(true);
    }

    public void RemoveWarning()
    {
        if(SuccessfulTemp == true)
        {
            this.gameObject.GetComponent<MenuManager>().GoToMainMenu();
        }
        else
        {
            WarningPanel.SetActive(false);
        }
    }
}
