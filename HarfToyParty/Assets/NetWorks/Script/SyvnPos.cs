using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyvnPos : NetworkBehaviour
{
    [SyncVar]
    private Vector3 syncPos;

    [SyncVar]
    private Vector3 syncWallPos;

    [SerializeField]
    Transform myTransform;
    [SerializeField]
    float lerpRate = 15;
    [SerializeField]
    private GameObject WallObj;

    void Start()
    {
    }

    private void FixedUpdate()
    {
        TransmitPosition();
        LerpPos();

        /**/
        TransmitWallPosition();
        LerpWallPos();

        //if (!isServer&&WallObj.GetComponent<MoveWall>().UpdatePossition)
        //{
        //        WallObj.GetComponent<MoveWall>().UpdatePossition = false;
        //        Updateposition(WallObj.GetComponent<MoveWall>().finalPos);
        //}

    }

    void LerpPos()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvidePostionToServer(Vector3 pos)
    {
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer)
        {
            CmdProvidePostionToServer(myTransform.position);
        }
    }


    /*test*/

    void LerpWallPos()
    {
        if (!isLocalPlayer)
        {
            WallObj.transform.position = Vector3.Lerp(WallObj.transform.position, syncWallPos, Time.deltaTime * lerpRate);
            
        }
    }

    [Command]
    void CmdIpdateWallPosition(Vector3 wallpos)
    {
        syncWallPos = wallpos;
    }

    [ClientCallback]
    void TransmitWallPosition()
    {
        if (isLocalPlayer)
        {
            CmdIpdateWallPosition(WallObj.transform.position);
        }
    }


    /**********************/
    //public void Updateposition(Vector3 newpos)
    //{
    //    CmdUpdatePosition(newpos);
    //}

    //[Command]
    //void CmdUpdatePosition(Vector3 newPosition)
    //{
    //    RpcUpdateWallPosition(newPosition);
    //}

    //[ClientRpc]
    //void RpcUpdateWallPosition(Vector3 newPosition)
    //{
    //    WallObj.transform.position = newPosition;
    //}
}
