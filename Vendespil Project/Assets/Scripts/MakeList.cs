using Assets.Scripts;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;
public class MakeList : MonoBehaviour {

    public GameObject Content;
    public GameObject Prefab;

    IEnumerator AddList()
    {
        //StartCoroutine(SendDataToPHP(Username.text, Password.text));
        ApiHandler api = GameObject.Find("ApiHandler").GetComponentInChildren<ApiHandler>();

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("action", "getAllUsers");
        //post.Add("action", "getAllUsersWithQuestionsByTeamId");
        //post.Add("id", UserData.User.teamId.ToString());

        WWW result = api.POST(post);

        yield return new WaitUntil(() => result.isDone == true);

        var N = JSON.Parse(result.text);

        foreach (Transform child in Content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var item in N)
        {
            if(N["error"] == null)
            {
                GameObject newList = (GameObject)Instantiate(Prefab, Content.transform);
                newList.name = item.Value["username"];
                newList.GetComponentInChildren<Text>().text = item.Value["name"];
                newList.GetComponent<ListButton>().UserID = item.Value["id"];
            }
        }

        //Debug.Log(N["userdata"]["name"]);
        //Debug.Log(result.text);
    }

    public void SetData(int ID)
    {
        Debug.Log(ID);
    }

    private void OnEnable()
    {
        StartCoroutine(AddList());
    }
}