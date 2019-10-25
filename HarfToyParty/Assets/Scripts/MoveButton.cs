using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 */ 

public class MoveButton : Tap
{
    public override void GetTap()
    {
        base.GetTap();
        Debug.Log(tapObject.name);
    }

}
