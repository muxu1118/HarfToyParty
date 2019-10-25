using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 */ 

public class MoveButton : Tap
{
    [SerializeField]
    private float touchRangeX;
    [SerializeField]
    private float touchRangeY;

    private Vector2 movePosition;
    private bool swiChe;

    private void Update()
    {
        GetTap();
        Swipe();
    }

    // Swipe 
    public override void GetTap()
    {
        base.GetTap();
        if (Input.touchCount <= 0) return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Ended)
        {
            StartPosition = Vector2.zero;
            swiChe = false;
        }


        if (StartPosition.x <= transform.position.x+touchRangeX&&StartPosition.x >= transform.position.x - touchRangeX&& StartPosition.y <= transform.position.y + touchRangeY && StartPosition.y >= transform.position.y  -touchRangeY)
        {
            swiChe = true;
        }

    }
    private void Swipe()
    {
        if (!swiChe) return;
        Touch touch = Input.GetTouch(0);
        TouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        Debug.Log("Touch"+TouchPosition);
        Debug.Log("Start" + StartPosition);
        if (TouchPosition.x > StartPosition.x + 0.1f)
        {
            Debug.Log("右に移動");
        }
        else if (TouchPosition.x < StartPosition.x - 0.1f)
        {
            Debug.Log("左に移動");
        }
        else if (TouchPosition.x < StartPosition.x - 0.1f)
        {
            Debug.Log("左に移動");
        }

    }


}
