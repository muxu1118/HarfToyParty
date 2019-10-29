using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerStatus
{
    float speed;

}

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    protected Map map = Map.instance;
    private void Start()
    {


    }
}
