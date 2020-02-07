using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartThrow : MonoBehaviour
{
    [SerializeField]
    GameObject throwingObj, targetObj;
    [SerializeField]
    int x, y;

    [SerializeField]
    Rigidbody2D rb2;
    Vector2 pos;

    float Middle_x, Middle_y;
    float colorDown = 6;

    bool colorflag = false;
    float Scale_x, Scale_y;
    Vector2 obj;
    Vector2 initial;
    Vector2 initial_scale;

    void Start()
    {
        //Middle_x = (throwingObj.transform.position.x + targetObj.transform.position.x) / 2;
        //Debug.Log(Middle_x);  
        obj = gameObject.transform.localScale;
        initial = gameObject.transform.position;
        initial_scale = gameObject.transform.localScale;
        //initial = gameObject.transform.localScale;
    }    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            partThrow();
        }
        //StartCoroutine("switching");
        //partThrow();

        //透明度を下げる
        if (colorflag)
        {
            colorDown -= Time.deltaTime;
            throwingObj.GetComponent<Image>().color = new Color(255, 255, 255, colorDown);

            obj.x -= 0.001f;
            obj.y -= 0.001f;
            
            gameObject.transform.localScale = obj;
        }
        //下がりきったらおしまい
        if (colorDown < 0)
        {
            colorflag = !colorflag;
            throwingObj.GetComponent<Image>().color = new Color(255, 255, 255, 255); //色を初期化
            gameObject.transform.position = initial;                                 //位置を初期化
            gameObject.transform.localScale = initial_scale;                         //大きさを初期化
            obj = initial_scale;                                                     //大きさを変更していた値を初期化
            rb2.gravityScale = 0;                                                    //重力を初期化
            rb2.constraints = RigidbodyConstraints2D.FreezeAll;                      //移動しないようにする
            colorDown = 6;
            gameObject.SetActive(false);
            Debug.Log("戻ったよ");
        }        
    }

    /// <summary>
    /// パーツを放物線を描いて飛ばす
    /// </summary>
    public void partThrow()
    {
        rb2.constraints = RigidbodyConstraints2D.None;
        rb2.gravityScale = -1;
        //パーツのカラーを徐々に薄くするトリガー
        colorflag = !colorflag;
        //放物線上を求める
        pos = new Vector2(x * 10 + 2 / (9.8f * 10 * 10), x * 10 + 2 / (9.8f * 10 * 10));
        //一度だけ力を加える
        rb2.AddForce(pos, ForceMode2D.Impulse);
        StartCoroutine("switching");
        
    }

    //力を加えたオブジェクトに重力をかける
    IEnumerator switching()
    {        
        yield return new WaitForSeconds(1.5f);

        rb2.gravityScale = 9;
    }    
}
