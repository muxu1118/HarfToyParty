using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * タップを検知する
 */
public class Tap : MonoBehaviour
{
    
    private static Vector3 TouchPosition = Vector3.zero;
    // タップしたオブジェクト
    protected GameObject tapObject;

    public virtual void GetTap()
    {
        // スマホタップの取得 touchCountが0以上でタップ判定
        if (Input.touchCount <= 0) return;

        // タッチ情報の取得
        Touch touch = Input.GetTouch(0);

        // タップしているところを取得(使うか微妙)
        TouchPosition.x = touch.position.x;
        TouchPosition.y = touch.position.y;

        // タップしたオブジェクトを取得
        if(touch.phase == TouchPhase.Began ){
            // タッチした位置からRayを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            // Rayを飛ばす
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit)){
                // Rayを飛ばしてあたったオブジェクトが自分自身じゃない時
                if (hit.collider.gameObject != this.gameObject){
                    tapObject = hit.collider.gameObject; 
                }
            }
        }

    }
}

public class Jissou:Tap{
    public override void GetTap(){
        base.GetTap();
        
    }
}
