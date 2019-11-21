using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MoveWall : NetworkBehaviour
{

    private enum form
    {
        square = 0,
        Rectangle,
        cross,
        VerticalRect,
    }
    [SerializeField]
    private form myForm;

    [SerializeField]
    private MapKind MyWall;

    [SerializeField]
    Vector2 XY;

    private void Start()
    {
        int x = (int)XY.x, y = (int)XY.y;
        switch ((int)myForm)
        {
            case 0:
                Map.instance.mapInt[y, x] = (int)MyWall;
                break;
            case 1:
                Map.instance.mapInt[y, x] = (int)MyWall;
                Map.instance.mapInt[y, x + 1] = (int)MyWall;
                break;
            case 2:
                Map.instance.mapInt[y, x] = (int)MyWall;
                Map.instance.mapInt[y + 1, x] = (int)MyWall;
                Map.instance.mapInt[y - 1, x] = (int)MyWall;
                Map.instance.mapInt[y, x + 1] = (int)MyWall;
                Map.instance.mapInt[y, x - 1] = (int)MyWall;
                break;
            case 3:
                Map.instance.mapInt[y, x] = (int)MyWall;
                Map.instance.mapInt[y - 1, x] = (int)MyWall;
                break;

        }
    }
    /// <summary>
    /// 移動できるか
    /// </summary>
    /// <returns></returns>
    public bool MoveCheck(Vector2 mov2, List<List<Vector3>> vec3)
    {
        int[,] map = Map.instance.mapInt;
        bool isMove;
        int x = 0;
        switch ((int)myForm)
        {
            case 0:
                if (XY.x + mov2.x >= 6 || XY.x + mov2.x < 0) return false;
                if (XY.y - mov2.y >= 6 || XY.y - mov2.y < 0) return false;
                if (map[(int)XY.y - (int)mov2.y, (int)XY.x + (int)mov2.x] == (int)MapKind.Wall || map[(int)XY.y - (int)mov2.y, (int)XY.x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y - (int)mov2.y, (int)XY.x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y - (int)mov2.y, (int)XY.x + (int)mov2.x] == (int)MapKind.Bomb) return false;
                break;
            case 1:
                Debug.Log("行くぜ");
                x = 0;
                if (XY.x + 1 + mov2.x >= 6 || XY.x + mov2.x < 0) return false;
                if (XY.y - mov2.y >= 6 || XY.y - mov2.y < 0) return false;
                Debug.Log("" + (int)XY.y + (int)mov2.y +"?"+ (int)XY.x + x + (int)mov2.x);
                if (map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player1 || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb) return false;
                x = 1;
                Debug.Log("" + (int)XY.y + (int)mov2.y + "?" + (int)XY.x + x + (int)mov2.x);
                if (map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Wall || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Player2 || map[(int)XY.y - (int)mov2.y, (int)XY.x + x + (int)mov2.x] == (int)MapKind.Bomb) return false;
                Debug.Log("抜けたぜ");
                break;
            case 2:
                //Map.instance.mapInt[y, x] = (int)MyWall;
                //Map.instance.mapInt[y + 1, x] = (int)MyWall;
                //Map.instance.mapInt[y - 1, x] = (int)MyWall;
                //Map.instance.mapInt[y, x + 1] = (int)MyWall;
                //Map.instance.mapInt[y, x - 1] = (int)MyWall;
                break;
                
        }
        StartCoroutine(WallMoveAnim(1,map,mov2,vec3));
        return true;
    }

    private void WallMove()
    {
        
    }
    IEnumerator WallMoveAnim(float wait, int[,] mapInt, Vector2 vec2, List<List<Vector3>> vec3)
    {
        float time = wait / Time.deltaTime;
        int x = (int)XY.x, y = (int)XY.y;
        switch ((int)myForm)
        {
            case 0:
                Map.instance.mapInt[y, x] = 0;
                Map.instance.mapInt[y - (int)vec2.y, x + (int)vec2.x] = (int)MyWall;
                break;
            case 1:
                Map.instance.mapInt[y, x] = 0;
                Map.instance.mapInt[y, x+1] = 0;
                Map.instance.mapInt[y - (int)vec2.y, x + (int)vec2.x] = (int)MyWall;
                Map.instance.mapInt[y - (int)vec2.y, x + 1 +(int)vec2.x] = (int)MyWall;
                break;
            case 2:
                //Map.instance.mapInt[y, x] = (int)MyWall;
                //Map.instance.mapInt[y + 1, x] = (int)MyWall;
                //Map.instance.mapInt[y - 1, x] = (int)MyWall;
                //Map.instance.mapInt[y, x + 1] = (int)MyWall;
                //Map.instance.mapInt[y, x - 1] = (int)MyWall;
                break;
        }
        while (wait >= 0)
        {
            wait -= Time.deltaTime;
            gameObject.transform.position += new Vector3((vec3[y - (int)vec2.y][x + (int)vec2.x].x - vec3[y][x].x) / time, (vec3[y - (int)vec2.y][x + (int)vec2.x].y - vec3[y][x].y) / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        XY.x += vec2.x;
        XY.y -= vec2.y;
        Debug.Log("X:" + XY.x + "Y:" + XY.y);
        gameObject.transform.position = new Vector3(vec3[y - (int)vec2.y][x + (int)vec2.x].x, vec3[y - (int)vec2.y][x + (int)vec2.x].y);
    }

}
