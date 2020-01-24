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

    public bool PullWall ()
    {
        if (!GrabWallCheck()) return false;
        Debug.Log("壁があります");
        return true;
    }

    private bool GrabWallCheck()
    {
        Debug.Log(player.rot);
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
                Debug.Log((MapKind)Map.instance.mapInt[y - 1, x]);
                if (y - 1 < 0) return false;
                if (!(Map.instance.mapInt[y - 1, x] >= (int)MapKind.Movewall0 && Map.instance.mapInt[y - 1, x] <= (int)MapKind.Movewall10)) return false;
                break;
            case 1:// 下
                if (y + 1 >= 6) return false;
                if (!(Map.instance.mapInt[y + 1, x] >= (int)MapKind.Movewall0 && Map.instance.mapInt[y + 1, x] <= (int)MapKind.Movewall10)) return false;
                break;
            case 2:// 右
                if (x + 1 >= 6) return false;
                if (!(Map.instance.mapInt[y, x+1] >= (int)MapKind.Movewall0 && Map.instance.mapInt[y, x + 1] <= (int)MapKind.Movewall10)) return false;
                break;
            case 3:// 左
                if (x - 1 < 0) return false;
                if (!(Map.instance.mapInt[y, x - 1] >= (int)MapKind.Movewall0 && Map.instance.mapInt[y, x - 1] <= (int)MapKind.Movewall10)) return false;
                break;
            default:
                break;

        }
        return true;
    }

}
