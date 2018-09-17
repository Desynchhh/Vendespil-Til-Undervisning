using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListButton : MonoBehaviour {

    public int UserID;

    public void SendID()
    {
        GameObject.Find("Manager").GetComponent<MakeList>().SetData(UserID);
    }
}
