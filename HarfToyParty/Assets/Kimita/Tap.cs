using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * タップを検知する
 */
public class Tap : MonoBehaviour
{
    
    protected Vector2 TouchPosition = Vector2.zero;
    protected Vector2 StartPosition = Vector2.zero;

    // タップしたオブジェクト
    protected GameObject tapObject;
    private Canvas canvas;
    protected int nowTouching;

    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public virtual void GetTap()
    {
        // スマホタップの取得 touchCountが0以上でタップ判定
        if (Input.touchCount <= 0) return;

        // タッチ情報の取得
        Touch touch = Input.GetTouch(0);



        
        
        // タップしたオブジェクトを取得
        if (touch.phase == TouchPhase.Began )
        {
            // タップしているところを取得
            TouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            StartPosition = TouchPosition;
            // タッチした位置からRayを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(TouchPosition);
            // Rayを飛ばす
            RaycastHit2D hit = new RaycastHit2D();
            if (Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, 100,2))
            {
                tapObject = hit.collider.gameObject;
            }
        }

    }
}
