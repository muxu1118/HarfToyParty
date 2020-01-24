using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chara : MonoBehaviour
{
    [SerializeField] SpriteRenderer a;
    [SerializeField] SpriteRenderer b;

    [SerializeField] GameObject ai;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        P();
        //if (Input.GetMouseButtonDown(0))
        //{
        //    ai.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.6f);
        //    //a.color = new Color(1, 1, 1, 0.6f);
        //}
        //if (Input.GetMouseButtonDown(1))
        //{
        //    ai.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        //    //a.color = new Color(1, 1, 1, 1);
        //}
    }

    public void P()
    {
        switch (TextMessage.sentenceNum)
        {
            case 1:
                a.color = new Color(1, 1, 1, 1);
                b.color = new Color(1, 1, 1, 0.6f);
                break;
            case 2:
                a.color = new Color(1, 1, 1, 0.6f);
                b.color = new Color(1, 1, 1, 1);
                break;
            case 3:
                a.color = new Color(1, 1, 1, 1);
                b.color = new Color(1, 1, 1, 1);
                break;
            default:
                break;
        }
    }
}
