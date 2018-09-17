using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListButton : MonoBehaviour {

    public int UserID;

    public void SendID()
    {
<<<<<<< HEAD
        GameObject.Find("PanelPickList").GetComponent<MakeList>().SetData(UserID);
=======
        Debug.Log(UserID);
        GameObject.Find("Manager").GetComponent<MakeList>().SetData(UserID);
>>>>>>> 4bb4f8d9b8202fa1b4a5e7e124e0d98dbab76213
    }
}
