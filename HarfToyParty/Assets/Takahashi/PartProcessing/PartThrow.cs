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

    void Start()
    {
        //Middle_x = (throwingObj.transform.position.x + targetObj.transform.position.x) / 2;
        //Debug.Log(Middle_x);        
    }    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            partThrow();
        }

        //透明度を下げる
        if (colorflag)
        {
            colorDown -= Time.deltaTime;
            throwingObj.GetComponent<Image>().color = new Color(255, 255, 255, colorDown);
        }
        //下がりきったらおしまい
        else if(colorDown < 0)
        {
            colorflag = !colorflag;
            colorDown = 1;
        }        
    }

    /// <summary>
    /// パーツを放物線を描いて飛ばす
    /// </summary>
    public void partThrow()
    {
        //パーツのカラーを徐々に薄くするトリガー
        colorflag = !colorflag;
        //放物線上を求める
        pos = new Vector2(x * 10 + 2 / (9.8f * 10 * 10), y * 10 + 2 / (9.8f * 10 * 10));
        //一度だけ力を加える
        rb2.AddForce(pos, ForceMode2D.Impulse);
        
        StartCoroutine("switching");        
    }

    //力を加えたオブジェクトに重力をかける
    IEnumerator switching()
    {        
        yield return new WaitForSeconds(1.5f);

        rb2.gravityScale = 7;
    }    
}
