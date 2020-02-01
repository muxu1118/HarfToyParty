using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chara : MonoBehaviour
{
    [SerializeField] SpriteRenderer a;
    [SerializeField] SpriteRenderer b;
    
    // Update is called once per frame
    void Update()
    {
        P();
    }

    public void P()
    {
        switch (TextMessage.sentenceNum)
        {
            case 1:
                a.color = new Color(1, 1, 1, 1);
                b.color = new Color(0.5f, 0.5f, 0.5f, 0.6f);
                break;
            case 2:
                a.color = new Color(0.5f, 0.5f, 0.5f, 0.6f);
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
