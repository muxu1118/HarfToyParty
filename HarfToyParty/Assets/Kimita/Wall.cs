using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // 壁の形
    private enum form
    {
        square = 0,
        Rectangle,
        cross,
        VRectangle,
    }

    [SerializeField]
    private form MyForm;

    [SerializeField]
    Vector2 XY;

    // Start is called before the first frame update
    void Start()
    {
        int x = (int)XY.x, y = (int)XY.y;
        switch ((int)MyForm)
        {
            case 0:
                Map.instance.mapInt[y,x] = (int)MapKind.Wall;
                break;
            case 1:
                Map.instance.mapInt[y, x] = (int)MapKind.Wall;
                Map.instance.mapInt[y, x + 1] = (int)MapKind.Wall;
                break;
            case 2:
                Map.instance.mapInt[y, x] = (int)MapKind.Wall;
                Map.instance.mapInt[y + 1, x] = (int)MapKind.Wall;
                Map.instance.mapInt[y - 1, x] = (int)MapKind.Wall;
                Map.instance.mapInt[y, x + 1] = (int)MapKind.Wall;
                Map.instance.mapInt[y, x - 1] = (int)MapKind.Wall;
                break;
            case 3:
                Map.instance.mapInt[y, x] = (int)MapKind.Wall;
                Map.instance.mapInt[y + 1, x] = (int)MapKind.Wall;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
