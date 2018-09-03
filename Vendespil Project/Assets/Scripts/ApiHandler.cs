using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class ApiHandler : MonoBehaviour
    {
        [Header("API Web address")]
        public string URL = "http://localhost/api.php";

        void Start() { }

        public WWW POST(Dictionary<string, string> post)
        {
            WWWForm form = new WWWForm();
            foreach (KeyValuePair<String, String> post_arg in post)
            {
                form.AddField(post_arg.Key, post_arg.Value);
            }
            WWW www = new WWW(URL, form);

            StartCoroutine(WaitForRequest(www));
            return www;

        }

        private IEnumerator WaitForRequest(WWW www)
        {
            yield return www;

            // check for errors
            if (www.error == null)
            {
                Debug.Log("API Sucess: " + www.text);
            }
            else
            {
                Debug.Log("API Error: " + www.error);
            }
        }

    }
}
