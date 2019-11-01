using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMove : MonoBehaviour
{
    [SerializeField]
    private float touchRangeX;
    [SerializeField]
    private float touchRangeY;

    [SerializeField]
    private Player MyPlayer;

    private Vector2 movePosition;
    private bool swiChe;

    protected Vector2 TouchPosition = Vector2.zero;
    protected Vector2 StartPosition = Vector2.zero;

    // タップしたオブジェクト
    protected GameObject tapObject;
    private Canvas canvas;
    protected int nowTouching;

    private void Update()
    {
        GetClick();
        Swipe();
    }

    // Swipe 
    private void GetClick()
    {
        // スマホタップの取得 touchCountが0以上でタップ判定
        if (Input.GetMouseButtonDown(0))
        {
            TouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            StartPosition = TouchPosition;
            
        }else if (Input.GetMouseButtonUp(0))
        {
            StartPosition = Vector2.zero;
            swiChe = false;
        }else if (Input.GetMouseButton(0))
        {
            TouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (StartPosition.x <= transform.position.x + touchRangeX && StartPosition.x >= transform.position.x - touchRangeX && StartPosition.y <= transform.position.y + touchRangeY && StartPosition.y >= transform.position.y - touchRangeY)
        {
            swiChe = true;
        }

    }
    private void Swipe()
    {
        if (!swiChe) return;
        if (TouchPosition.y > StartPosition.y + 0.1f && Mathf.Abs(TouchPosition.y) - StartPosition.y >= Mathf.Abs(TouchPosition.x) - StartPosition.x)
        {
            MyPlayer.Move(0);
        }
        else if (TouchPosition.y < StartPosition.y - 0.1f && Mathf.Abs(TouchPosition.y) - StartPosition.y >= Mathf.Abs(TouchPosition.x) - StartPosition.x)
        {
            MyPlayer.Move(1);
        }
        else if (TouchPosition.x > StartPosition.x + 0.1f)
        {
            MyPlayer.Move(2);
        }
        else if (TouchPosition.x < StartPosition.x - 0.1f)
        {
            MyPlayer.Move(3);
        }

    }
}
