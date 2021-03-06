﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyvnPos : NetworkBehaviour
{
    [SyncVar]
    private Vector3 syncPos;

   [SerializeField]
    [SyncVar]
    private string syncmapdata;

    [SyncVar]
    private Vector2 synctargetPos;

    [SyncVar]
    private bool syncupdate = false;

    [SerializeField]
    Transform myTransform;
   
    [SerializeField]
    float lerpRate = 15;
    [SerializeField]
    GameObject testwall;

    [SerializeField]
    bool testup;
    void Start()
    {

    }


    private void FixedUpdate()
    {
        TransmitPosition();
        LerpPos();
        if (syncupdate && !isLocalPlayer)
        {
            syncupdate = false;
            Map.instance.updateMap = false;
            Map.instance.mapInt = stringtoarray(syncmapdata);
        }

        if (testup && isLocalPlayer)
        {
            Cmdtest(new Vector2(0, 0));
        }
    }



    [Command]
    void Cmdtest(Vector2 pos)
    {
        Rpctest(pos);
    }

    [ClientRpc]
    void Rpctest(Vector2 pos)
    {
        testwall.transform.position = pos;
    }
    /// <summary>
    /// サーバーへ位置変換を要求します。
    /// </summary>
    public void UpdateMePosition(GameObject targetObj,Vector3 targetpos,Vector2 TargetXY)
    {
        CmdUpdateWall(targetObj, targetpos, TargetXY);
    }

    [Command]
    void CmdUpdateWall(GameObject obj,Vector3 pos,Vector2 _XY)
    {
        obj.transform.position = pos;
        obj.GetComponent<MoveWall>().XY.x = _XY.x;
        obj.GetComponent<MoveWall>().XY.y = _XY.y;
        Debug.Log("UpdatePositionWall");
    }

    [ClientCallback]
    void UpdateWallPos(GameObject obj, Vector3 pos, Vector2 _XY)
    {
       
    }


    /// <summary>
    /// WinLoss
    /// </summary>
    #region WinLose

    // 1=PLayer01,2=Player02
    public void CheckWinLose(int playerNum)
    {
        CmdWinOrLose(playerNum);
    }

    [Command]
    void CmdWinOrLose(int playerNum)
    {
        RpcUpdateWinOrLose(playerNum);
    }

    [ClientRpc]
    void RpcUpdateWinOrLose(int playerNum)
    {
        if (playerNum == 1)
        {
            GameManager.instance.RedPartGet++;
        }

        else if (playerNum == 2)
        {
            GameManager.instance.BluePartGet++;
        }
    }
    #endregion

    void LerpPos()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvidePostionToServer(Vector3 pos,string inputstring,bool tmptrigger)
    {
        syncPos = pos;
        syncmapdata = inputstring;
        syncupdate = tmptrigger;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer)
        {
            CmdProvidePostionToServer(myTransform.position,Arraytostring(Map.instance.mapInt),Map.instance.updateMap);
        }
    }
    #region testing
    int[] inputdata(int[,] tmpin)
    {
        int tmpindex = 0;
        int[] tmpout = new int[tmpin.GetLength(0)*tmpin.GetLength(1)];
        for(int i = 0; i < tmpin.GetLength(0); i++)
        {
            for(int j = 0; j < tmpin.GetLength(1); j++)
            {
                tmpout[tmpindex] = tmpin[i,j];
                tmpindex++;
            }
        }
        return tmpout;
    }
    int[,] outputdata(int[] tmpout)
    {
        int tmpcount = (int)(Mathf.Sqrt(tmpout.Length));
        int tmpindex = 0;
        int[,] tmpreturn = new int[tmpcount,tmpcount];
        for(int i = 0;i < tmpcount; i++)
        {
            for(int j = 0; j < tmpcount; j++)
            {
                tmpreturn[i, j] = tmpout[tmpindex];
                tmpindex++;
            }
        }
        return tmpreturn;
    }
    #endregion

    string Arraytostring(int[,] inputdata)
    {
        string converteddata = inputdata.GetLength(0).ToString() + inputdata.GetLength(1).ToString();
        for (int i = 0; i < inputdata.GetLength(0); i++)
        {
            for(int j = 0; j < inputdata.GetLength(1); j++)
            {
                if (inputdata[i, j] < 10)
                    converteddata += "0" + inputdata[i, j].ToString();
                else
                    converteddata += inputdata[i, j].ToString();
            }
        }
        return converteddata;
    }

    int[,] stringtoarray(string inputdata)
    {
        int[,] convertedarray = new int[int.Parse(inputdata.Substring(0,1)), int.Parse(inputdata.Substring(1, 1))];
        int tmpindex = 2;
        for(int i = 0; i < convertedarray.GetLength(0); i++)
        {
            for(int j=0;j < convertedarray.GetLength(0); j++)
            {
                string tmpstring = inputdata.Substring(tmpindex, 2);
                convertedarray[i,j] = int.Parse(tmpstring);
                tmpindex += 2;
            }
        }
        return convertedarray;
    }
    void printout(int[] output)
    {
        string tmpdata = "";
        for(int i = 0; i < output.Length; i++)
        {
            tmpdata += output[i] + " ";
        }
        Debug.Log("is server?" + isServer);
        Debug.Log("is client?" + isClient);
        Debug.Log(tmpdata);
    }
}
