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

<<<<<<< HEAD
=======
    [SyncVar]
    private string syncmapdata;

    [SyncVar]
    private bool syncupdate = false;

>>>>>>> origin/Lai
    [SerializeField]
    Transform myTransform;
    [SerializeField]
    float lerpRate = 15;
<<<<<<< HEAD
    
=======
>>>>>>> origin/Lai

    void Start()
    {
    }

    private void FixedUpdate()
    {
        TransmitPosition();
        LerpPos();
<<<<<<< HEAD

        /**/
        //TransmitWallPosition();
        //LerpWallPos();

        //if (!isServer&&WallObj.GetComponent<MoveWall>().UpdatePossition)
        //{
        //        WallObj.GetComponent<MoveWall>().UpdatePossition = false;
        //        Updateposition(WallObj.GetComponent<MoveWall>().finalPos);
        //}

=======
        if (syncupdate && !isLocalPlayer)
        {
            syncupdate = false;
            Map.instance.updateMap = false;
            Map.instance.mapInt = stringtoarray(syncmapdata);
        }
>>>>>>> origin/Lai
    }

    void LerpPos()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
<<<<<<< HEAD
    void CmdProvidePostionToServer(Vector3 pos)
    {
        syncPos = pos;
=======
    void CmdProvidePostionToServer(Vector3 pos,string inputstring,bool tmptrigger)
    {
        syncPos = pos;
        syncmapdata = inputstring;
        syncupdate = tmptrigger;  
>>>>>>> origin/Lai
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer)
        {
<<<<<<< HEAD
            CmdProvidePostionToServer(myTransform.position);
        }
    }


    /*test*/

    //void LerpWallPos()
    //{
    //    if (!isLocalPlayer)
    //    {
    //        WallObj.transform.position = Vector3.Lerp(WallObj.transform.position, syncWallPos, Time.deltaTime * lerpRate);

    //    }
    //}

    //[Command]
    //void CmdIpdateWallPosition(Vector3 wallpos)
    //{
    //    syncWallPos = wallpos;
    //}

    //[ClientCallback]
    //void TransmitWallPosition()
    //{
    //    if (isLocalPlayer)
    //    {
    //        CmdIpdateWallPosition(WallObj.transform.position);
    //    }
    //}


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
=======
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
>>>>>>> origin/Lai
}
