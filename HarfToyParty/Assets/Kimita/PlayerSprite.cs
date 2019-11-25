using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerSprite : NetworkBehaviour
{
    Sprite[] sprites = new Sprite[2];
    public bool isServe;

    // Start is called before the first frame update
    void Start()
    {
        sprites[0] = Resources.Load<Sprite>("Sprites/Ani");
        sprites[1] = Resources.Load<Sprite>("Sprites/Otouto");
        gameObject.GetComponent<SpriteRenderer>().sprite = (isServer) ? sprites[0] : sprites[1];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
