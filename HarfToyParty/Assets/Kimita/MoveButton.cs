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

    [SerializeField]
    private Player MyPlayer;

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
        if (TouchPosition.x > StartPosition.x + 0.1f&& Mathf.Abs(TouchPosition.y) - StartPosition.y <= Mathf.Abs(TouchPosition.x) - StartPosition.x)
        {
            Debug.Log("右に移動");
            MyPlayer.Move(2);
        }
        else if (TouchPosition.x < StartPosition.x - 0.1f && Mathf.Abs(TouchPosition.y) - StartPosition.y <= Mathf.Abs(TouchPosition.x) - StartPosition.x)
        {
            Debug.Log("左に移動");
            MyPlayer.Move(3);
        }
        else if (TouchPosition.y > StartPosition.y + 0.1f&& Mathf.Abs(TouchPosition.y) - StartPosition.y >= Mathf.Abs(TouchPosition.x) - StartPosition.x)
        {
            Debug.Log("上に移動");
            MyPlayer.Move(0);
        }
        else if (TouchPosition.y < StartPosition.y - 0.1f && Mathf.Abs(TouchPosition.y) - StartPosition.y >= Mathf.Abs(TouchPosition.x) - StartPosition.x)
        {
            MyPlayer.Move(1);
            Debug.Log("下に移動");
        }

    }


}
