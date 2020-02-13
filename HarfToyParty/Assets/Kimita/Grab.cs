using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    Player player;
    int playernum;
    private void Start()
    {
        player = GetComponent<Player>();
        playernum = (gameObject.name == "Player1") ?(int)MapKind.Player1: (int)MapKind.Player2;
    }

    public bool PullWall (ref int num)
    {
        if (!GrabWallCheck(ref num)) return false;

        return true;
    }

    private bool GrabWallCheck(ref int num)
    {
        int x=0, y=0;
        
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                if (Map.instance.mapInt[j, i] == playernum)
                {
                    x = i;
                    y = j;
                }
            }
        }
        
        switch (player.rot)
        {
            case 0:// 上
                if (y - 1 < 0) return false;
                if (!(Map.instance.mapInt[y - 1, x] >= (int)MapKind.Movewall0 && Map.instance.mapInt[y - 1, x] <= (int)MapKind.Movewall10)) return false;
                if (Map.instance.MoveWalls[Map.instance.mapInt[y - 1, x] - (int)MapKind.Movewall0].GetComponent<MoveWall>().grab) return false;
                Map.instance.MoveWalls[Map.instance.mapInt[y - 1, x] - (int)MapKind.Movewall0].GetComponent<MoveWall>().grab = true;
                num = Map.instance.mapInt[y - 1, x];
                break;
            case 1:// 下
                if (y + 1 > 6) return false;
                if (!(Map.instance.mapInt[y + 1, x] >= (int)MapKind.Movewall0 && Map.instance.mapInt[y + 1, x] <= (int)MapKind.Movewall10)) return false;
                if (Map.instance.MoveWalls[Map.instance.mapInt[y + 1, x] - (int)MapKind.Movewall0].GetComponent<MoveWall>().grab) return false;
                Map.instance.MoveWalls[Map.instance.mapInt[y + 1, x] - (int)MapKind.Movewall0].GetComponent<MoveWall>().grab = true;
                num = Map.instance.mapInt[y + 1, x];
                break;
            case 2:// 右
                if (x + 1 > 6) return false;
                if (!(Map.instance.mapInt[y, x+1] >= (int)MapKind.Movewall0 && Map.instance.mapInt[y, x + 1] <= (int)MapKind.Movewall10)) return false;
                if (Map.instance.MoveWalls[Map.instance.mapInt[y, x + 1] - (int)MapKind.Movewall0].GetComponent<MoveWall>().grab) return false;
                Map.instance.MoveWalls[Map.instance.mapInt[y, x + 1] - (int)MapKind.Movewall0].GetComponent<MoveWall>().grab = true;
                num = Map.instance.mapInt[y, x + 1];
                break;
            case 3:// 左
                if (x - 1 < 0) return false;
                if (!(Map.instance.mapInt[y, x - 1] >= (int)MapKind.Movewall0 && Map.instance.mapInt[y, x - 1] <= (int)MapKind.Movewall10)) return false;
                if (Map.instance.MoveWalls[Map.instance.mapInt[y, x - 1] - (int)MapKind.Movewall0].GetComponent<MoveWall>().grab) return false;
                Map.instance.MoveWalls[Map.instance.mapInt[y, x - 1] - (int)MapKind.Movewall0].GetComponent<MoveWall>().grab = true;
                num = Map.instance.mapInt[y, x - 1];
                break;
            default:
                break;

        }
        return true;
    }

}
