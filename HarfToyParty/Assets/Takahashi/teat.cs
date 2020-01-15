using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class teat : MonoBehaviour
{
    [SerializeField]
    //Image[] image;
    Sprite[] image;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PartGet()
    {
        Debug.Log("hazime");
        i++;
        Debug.Log(i);
        switch (i)
        {
            case 1:
                image[0] = Resources.Load<Sprite>("Sprites/Part/gimmick_body3_B");
                //Debug.LogError("来た");
                break;
            case 2:
                image[1]= Resources.Load<Sprite>("Sprites/Part/gimmick_body01_B");
                break;
            case 3:
                image[2]= Resources.Load<Sprite>("Sprites/Part/gimmick_body2_B");
                break;
        }
    }
}
