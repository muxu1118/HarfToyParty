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

    PartDesplay partDesplay;

    [SerializeField]
    public PartKind kind;
    private int MyPartNum;
    //private bool server;
    private GameObject player;
    [SerializeField]
    private Vector2 XY;

    Sprite[] redSp = new Sprite[3];
    Sprite[] blueSp = new Sprite[3];
    WinLoss winlos;

    private SpriteRenderer spriteR = new SpriteRenderer();
    
    private void Start()
    {
        
        winlos =  FindObjectOfType<WinLoss>();
        partDesplay = GameObject.Find("character").GetComponent<PartDesplay>();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        //player= player = GameObject.FindObjectOfType<SyvnPos>().gameObject; 
        switch (winlos.stageCount)
        {
            case 0:
                kind = ((int)kind >= (int)PartKind.R_Leg && (int)kind <= (int)PartKind.R_Hand) ? PartKind.R_Leg: PartKind.B_Leg;
                gameObject.name = ("" + kind);
                spriteR.sprite = Resources.Load<Sprite>("Sprites/NewGimmick/"+gameObject.name);
                break;
            case 1:
                kind = ((int)kind >= (int)PartKind.R_Leg && (int)kind <= (int)PartKind.R_Hand) ? PartKind.R_Face : PartKind.B_Face;
                gameObject.name = ("" + kind);
                spriteR.sprite = Resources.Load<Sprite>("Sprites/NewGimmick/"+gameObject.name);
                break;
            case 2:
                kind = ((int)kind >= (int)PartKind.R_Leg && (int)kind <= (int)PartKind.R_Hand) ? PartKind.R_Hand : PartKind.B_Hand;
                gameObject.name = ("" + kind);
                spriteR.sprite = Resources.Load<Sprite>("Sprites/NewGimmick/"+gameObject.name);
                break;

        }
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

    }

    public void GetPart()
    {
        if (Map.instance.mapInt[(int)XY.y, (int)XY.x] != MyPartNum && (Map.instance.mapInt[(int)XY.y, (int)XY.x] == (int)MapKind.Player1 || Map.instance.mapInt[(int)XY.y, (int)XY.x] == (int)MapKind.Player2))
        {
            if (Map.instance.mapInt[(int)XY.y, (int)XY.x] == (int)MapKind.Player1 && (kind >= 0 && (int)kind <= 2)/*&& server*/)
            {
                Time.timeScale = 0;
                Debug.Log("koko");
                //player.GetComponent<SyvnPos>().CheckWinLose(1);
                gameObject.SetActive(false);
                GameManager.instance.RedPartGet++;
                //ameManager.instance.StageCount++;
                partDesplay.PartGet(gameObject.name, 1);
                Time.timeScale = 0;
            }
            else if (Map.instance.mapInt[(int)XY.y, (int)XY.x] == (int)MapKind.Player2 && (int)kind >= 3 && (int)kind <= 5/* && !server*/)
            {
                Time.timeScale = 0;
                Debug.Log("koko2");
                //player.GetComponent<SyvnPos>().CheckWinLose(2);
                gameObject.SetActive(false);
                GameManager.instance.BluePartGet++;
                //GameManager.instance.StageCount++;
                partDesplay.PartGet(gameObject.name, 2);
                Time.timeScale = 0;
            }


        }
        else if (Map.instance.mapInt[(int)XY.y, (int)XY.x] != MyPartNum)
        {
            Map.instance.mapInt[(int)XY.y, (int)XY.x] = MyPartNum;
        }
    }
}
