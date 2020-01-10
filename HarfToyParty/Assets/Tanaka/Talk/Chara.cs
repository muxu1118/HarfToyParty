using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chara : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer a;
    [SerializeField]
    SpriteRenderer b;


    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<SpriteRenderer>();
        b = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            a.color = new Color(1, 1, 1, 0.6f);
        }
        if (Input.GetMouseButtonDown(1))
        {
            a.color = new Color(1, 1, 1, 1);
        }

    }
    public void P(string player)
    {
        string playerNum = player;
        switch (playerNum)
        {
            case "1":
                a.color = new Color(1, 1, 1, 1);
                b.color = new Color(1, 1, 1, 0.6f);
                break;
            case "2":
                a.color = new Color(1, 1, 1, 0.6f);
                b.color = new Color(1, 1, 1, 1);
                break;

        }
    }
}
