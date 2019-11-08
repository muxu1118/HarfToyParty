using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int myNumber;
    Map map;
    public int rot = 2;
    private GameObject bomb;
    private Vector2 bombPos = new Vector2(1,1);
    private void Start()
    {

        map = Map.instance;
        myNumber = (gameObject.name == "Player1") ? (int)MapKind.Player1: (int)MapKind.Player2;
        int x, y;
        x = (gameObject.name == "Player1") ? 1 : 5;
        y = (gameObject.name == "Player1") ? 0 : 5;
        map.mapInt[y, x] = myNumber;
    }

    private void Update()
    {
        //if (bomb.activeSelf)
        //{
        //    switch (rot)
        //    {
        //        case 0:// 上
        //            bomb.transform.position = new Vector2(0, bombPos.y);
        //            break;
        //        case 1:// 下
        //            bomb.transform.position = new Vector2(0, -bombPos.y);
        //            break;
        //        case 2:// 右
        //            bomb.transform.position = new Vector2(bombPos.x,0);
        //            break;
        //        case 3:// 左
        //            bomb.transform.position = new Vector2(-bombPos.x, 0);
        //            break;
        //    }
        //}
        switch (rot)
        {
            case 0:// 上
                transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case 1:// 下
                transform.eulerAngles = new Vector3(0, 0, 270);
                break;
            case 2:// 右
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case 3:// 左
                transform.eulerAngles = new Vector3(0, 0, 180);
                break;
        }
    }

    public void Move(int x)
    {
        switch (x)
        {
            case 0:// 上
                rot = 0;
                map.Move((MapKind)myNumber, new Vector2(0, 1));
                
                break;
            case 1:// 下
                rot = 1;
                map.Move((MapKind)myNumber, new Vector2(0, -1));
                break;
            case 2:// 右
                rot = 2;
                map.Move((MapKind)myNumber, new Vector2(1, 0));
                break;
            case 3:// 左
                rot = 3;
                map.Move((MapKind)myNumber, new Vector2(-1, 0));
                break;

        }
    }
    
    


}
