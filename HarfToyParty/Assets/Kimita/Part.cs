using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.Networking;

public class Part : MonoBehaviour
{
    public enum PartKind
    {
        R_Leg,
        R_Face,
        R_Hand,
        B_Leg,
        B_Face,
        B_Hand,
    }
    

    public PartKind kind;
    private int MyPartNum;
    //private bool server;
    private GameObject player;
    [SerializeField]
    private Vector2 XY;
    private SpriteRenderer spriteR = new SpriteRenderer();

    private void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        //player= player = GameObject.FindObjectOfType<SyvnPos>().gameObject; ;
        switch (kind)
        {
            case PartKind.R_Leg:
                Map.instance.mapInt[(int)XY.y, (int)XY.x] = (int)MapKind.PartRLeg;
                MyPartNum = (int)MapKind.PartRLeg;
                break;
            case PartKind.R_Face:
                Map.instance.mapInt[(int)XY.y, (int)XY.x] = (int)MapKind.PartRFace;
                MyPartNum = (int)MapKind.PartRFace;
                break;
            case PartKind.R_Hand:
                Map.instance.mapInt[(int)XY.y, (int)XY.x] = (int)MapKind.PartRHand;
                MyPartNum = (int)MapKind.PartRHand;
                break;
            case PartKind.B_Leg:
                Map.instance.mapInt[(int)XY.y, (int)XY.x] = (int)MapKind.PartBLeg;
                MyPartNum = (int)MapKind.PartBLeg;
                break;
            case PartKind.B_Face:
                Map.instance.mapInt[(int)XY.y, (int)XY.x] = (int)MapKind.PartBFace;
                MyPartNum = (int)MapKind.PartBFace;
                break;
            case PartKind.B_Hand:
                Map.instance.mapInt[(int)XY.y, (int)XY.x] = (int)MapKind.PartBHand;
                MyPartNum = (int)MapKind.PartBHand;
                break;
            default:
                break;
        }
        //server = isServer;

    }
    private void Update()
    {
        GetPart();
        //if (Map.instance.mapInt[(int)XY.y, (int)XY.x] != (int)MapKind.YUKA)
        //{
        //    Map.instance.mapInt[(int)XY.y, (int)XY.x] = (int)MapKind.RedPart;
        //}
    }

    public void GetPart()
    {
        if (Map.instance.mapInt[(int)XY.y, (int)XY.x] != MyPartNum && (Map.instance.mapInt[(int)XY.y, (int)XY.x] == (int)MapKind.Player1 || Map.instance.mapInt[(int)XY.y, (int)XY.x] == (int)MapKind.Player2))
        {
            if(Map.instance.mapInt[(int)XY.y, (int)XY.x] == (int)MapKind.Player1 /*&& server*/)
            {
                //player.GetComponent<SyvnPos>().CheckWinLose(1);
                gameObject.SetActive(false);
                /* GameManager.instance.RedPartGet++;
                */
            }
            else if (Map.instance.mapInt[(int)XY.y, (int)XY.x] == (int)MapKind.Player2/* && !server*/)
            {
               // player.GetComponent<SyvnPos>().CheckWinLose(2);
                gameObject.SetActive(false);
                /*GameManager.instance.BluePartGet++;
                */
            }


        }
        else if (Map.instance.mapInt[(int)XY.y, (int)XY.x] != MyPartNum)
        {
            Map.instance.mapInt[(int)XY.y, (int)XY.x] = MyPartNum;
        }
    }
}
