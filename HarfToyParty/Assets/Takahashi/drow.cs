using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drow : MonoBehaviour
{
    PartDesplay partDesplay;
    [SerializeField]
    Image ani_Part, otouto_throwPart;

    // Start is called before the first frame update
    void Start()
    {
        partDesplay = GameObject.Find("character").GetComponent<PartDesplay>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void drowshori()
    {
        ani_Part.sprite = Resources.Load<Sprite>("Sprites/NewGimmick/R_Face");
        otouto_throwPart.sprite = Resources.Load<Sprite>("Sprites/NewGimmick/B_Face");
    }
}
