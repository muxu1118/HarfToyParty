using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    Player player;
    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void PullWall ()
    {
        if (!GrabWallCheck()) return;
        Debug.Log("player"+player+"向き"+player.rot);
    }

    private bool GrabWallCheck()
    {
        return true;
    }

}
