using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chara : MonoBehaviour
{
    [SerializeField] SpriteRenderer redBrother;
    [SerializeField] SpriteRenderer blueBrother;
    
    // Update is called once per frame
    void Update()
    {
        BrotherEmphasis();

        Transform mytransform = redBrother.transform;
        Transform _transform = blueBrother.transform;
    }

    public void BrotherEmphasis()
    {
        switch (TextMessage.sentenceNum)
        {
            case 1:
                redBrother.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                blueBrother.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                redBrother.transform.position = new Vector3(3.5f, -2.5f, 0);
                blueBrother.transform.position = new Vector3(-4.5f, -2.5f, 0);
                redBrother.color = new Color(1, 1, 1, 1);
                blueBrother.color = new Color(0.5f, 0.5f, 0.5f, 0.6f);
                break;
            case 2:
                redBrother.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                blueBrother.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                redBrother.transform.position = new Vector3(4.5f, -2.5f, 0);
                blueBrother.transform.position = new Vector3(-3.5f, -2.5f, 0);
                redBrother.color = new Color(0.5f, 0.5f, 0.5f, 0.6f);
                blueBrother.color = new Color(1, 1, 1, 1);
                break;
            case 3:
                redBrother.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                blueBrother.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                redBrother.transform.position = new Vector3(3.5f, -2.5f, 0);
                blueBrother.transform.position = new Vector3(-3.5f, -2.5f, 0);
                redBrother.color = new Color(1, 1, 1, 1);
                blueBrother.color = new Color(1, 1, 1, 1);
                break;
            default:
                break;
        }
    }
}
