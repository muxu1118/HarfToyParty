using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{

    private enum form
    {
        square = 0,
        Rectangle,
        cross,

    }
    [SerializeField]
    private form myForm;

    [SerializeField]
    Vector2 XY;

    private void Start()
    {
        int x = (int)XY.x, y = (int)XY.y;
        switch ((int)myForm)
        {
            case 0:
                Map.instance.mapInt[y, x] = (int)MapKind.Movewall;
                break;
            case 1:
                Map.instance.mapInt[y, x] = (int)MapKind.Movewall;
                Map.instance.mapInt[y, x + 1] = (int)MapKind.Movewall;
                break;
            case 2:
                Map.instance.mapInt[y, x] = (int)MapKind.Movewall;
                Map.instance.mapInt[y + 1, x] = (int)MapKind.Movewall;
                Map.instance.mapInt[y - 1, x] = (int)MapKind.Movewall;
                Map.instance.mapInt[y, x + 1] = (int)MapKind.Movewall;
                Map.instance.mapInt[y, x - 1] = (int)MapKind.Movewall;
                break;

        }
    }
    /// <summary>
    /// 移動できるか
    /// </summary>
    /// <returns></returns>
    public bool MoveCheck(Vector2 mov2)
    {
        
        bool isMove;
        //switch ((int)myForm)
        //{
        //    case 0:
        //        if(XY.x + mov2.x >= 6)
        //        break;
        //    case 1:
        //        Map.instance.mapInt[y, x] = (int)MapKind.Movewall;
        //        Map.instance.mapInt[y, x + 1] = (int)MapKind.Movewall;
        //        break;
        //    case 2:
        //        Map.instance.mapInt[y, x] = (int)MapKind.Movewall;
        //        Map.instance.mapInt[y + 1, x] = (int)MapKind.Movewall;
        //        Map.instance.mapInt[y - 1, x] = (int)MapKind.Movewall;
        //        Map.instance.mapInt[y, x + 1] = (int)MapKind.Movewall;
        //        Map.instance.mapInt[y, x - 1] = (int)MapKind.Movewall;
        //        break;

        //}
        return true;
    }

    private void WallMove()
    {
        
    }


}
