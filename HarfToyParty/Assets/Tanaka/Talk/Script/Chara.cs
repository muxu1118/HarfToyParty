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

        Transform mytransform = a.transform;
        Transform _transform = b.transform;
    }

    public void P()
    {
        switch (TextMessage.sentenceNum)
        {
            case 1:
                a.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                b.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                a.transform.position = new Vector3(3.5f, -2.5f, 0);
                b.transform.position = new Vector3(-4.5f, -2.5f, 0);
                a.color = new Color(1, 1, 1, 1);
                b.color = new Color(0.5f, 0.5f, 0.5f, 0.6f);
                break;
            case 2:
                a.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                b.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                a.transform.position = new Vector3(4.5f, -2.5f, 0);
                b.transform.position = new Vector3(-3.5f, -2.5f, 0);
                a.color = new Color(0.5f, 0.5f, 0.5f, 0.6f);
                b.color = new Color(1, 1, 1, 1);
                break;
            case 3:
                a.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                b.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                a.transform.position = new Vector3(3.5f, -2.5f, 0);
                b.transform.position = new Vector3(-3.5f, -2.5f, 0);
                a.color = new Color(1, 1, 1, 1);
                b.color = new Color(1, 1, 1, 1);
                break;
            default:
                break;
        }
    }
}
