using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class teat : MonoBehaviour
{
    [SerializeField]
    //Image[] image;
    //Sprite[] image;
    SpriteRenderer[] spriteRenderers;
    int i = 0;

    [SerializeField]
    GameObject background;

    private void Start()
    {
        background.transform.position = new Vector3(1000, 540, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //PartGet();
            
        }
    }

    public void PartGet()
    {
        Debug.Log("hazime");
        i++;
        Debug.Log(i);
        switch (i)
        {
            case 1:
                spriteRenderers[0].sprite = Resources.Load<Sprite>("Sprites/old/Part/gimmick_body3_B");
                //Debug.LogError("来た");
                break;
            case 2:
                spriteRenderers[1].sprite = Resources.Load<Sprite>("Sprites/old/Part/gimmick_body01_B");
                break;
            case 3:
                spriteRenderers[2].sprite = Resources.Load<Sprite>("Sprites/old/Part/gimmick_body2_B");
                break;
        }
    }
}
