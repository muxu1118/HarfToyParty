using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class BreakWall : MonoBehaviour
{

    [SerializeField]
    float lerpRate = 15;

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

    public MapKind MyWallP { get => MyWall; set => MyWall = value; }

    private void Start()
    {
        int x = (int)XY.x, y = (int)XY.y;
        switch ((int)myForm)
        {
            case 0:
                Map.instance.mapInt[y, x] = (int)MyWallP;
                break;
            case 1:
                Map.instance.mapInt[y, x] = (int)MyWallP;
                Map.instance.mapInt[y, x + 1] = (int)MyWallP;
                break;
            case 2:
                Map.instance.mapInt[y, x] = (int)MyWallP;
                Map.instance.mapInt[y + 1, x] = (int)MyWallP;
                Map.instance.mapInt[y - 1, x] = (int)MyWallP;
                Map.instance.mapInt[y, x + 1] = (int)MyWallP;
                Map.instance.mapInt[y, x - 1] = (int)MyWallP;
                break;
            case 3:
                Map.instance.mapInt[y, x] = (int)MyWallP;
                Map.instance.mapInt[y + 1, x] = (int)MyWallP;
                break;

        }
    }

    private void Update()
    {
    }
    
   
}