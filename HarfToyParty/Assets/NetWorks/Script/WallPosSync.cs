using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class WallPosSync : NetworkBehaviour
{
    [SyncVar]
    private Vector2 syncwall;

    [SerializeField]
    private Transform wallpos;

    [SyncVar]
    public bool UpdatePos = false;

    [SerializeField]
    private float lerp=15;


    private void Start()
    {
        
        syncwall = GetComponent<Transform>().position;
    }

    private void FixedUpdate()
    {
        //if (UpdatePos) 
        //{
        //    LerpPos();
        //    TransmitPos();
        //}

        if (UpdatePos) 
        {
            if (!isServer) 
                {
                CmdChangePos();
            }
            
        }
        
    }

    void LerpPos()
    {
        if (!isLocalPlayer) 
        {
            wallpos.position = Vector2.Lerp(wallpos.position, syncwall, lerp * Time.deltaTime);
        }
    }

    [Command]
    void CmdChangePos()
    {
        this.transform.position = new Vector3(0, 0, 0);
    }

    [Command]
    void CmdUpdateWallPos(Vector2 wallpos)
    {
        syncwall = wallpos;
    }

    [ClientCallback]
    void TransmitPos()
    {
        if (isLocalPlayer) 
            {
            CmdUpdateWallPos(wallpos.position);
        }
    }


}
    
