using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFlick : MonoBehaviour
{

    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Flick();
    }
    private void Flick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchStartPos = new Vector2(Input.mousePosition.x,
                                        Input.mousePosition.y);
        }

    }
}
