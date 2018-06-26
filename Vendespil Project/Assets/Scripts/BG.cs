using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{

    public RectTransform[] Background;

    public GameObject Spawn;
    public GameObject End;

    public float Speed;

    void FixedUpdate()
    {
        foreach(RectTransform temp in Background)
        {
            temp.position -= Vector3.down * Time.deltaTime * Speed;
            temp.position -= Vector3.left * Time.deltaTime * Speed / 5;

            if (temp.position.y > End.transform.position.y)
            {
                temp.position = Spawn.transform.position;
            }
        }
    }
}
