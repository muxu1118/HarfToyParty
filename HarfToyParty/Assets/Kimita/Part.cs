using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Part : MonoBehaviour
{
    public enum PartKind
    {
        R_Reg,
        R_Face,
        R_Hand,
        B_Reg,
        B_Face,
        B_Hand,
    }
    public PartKind kind;

    [SerializeField]
    private Vector2 XY;
    private SpriteRenderer spriteR = new SpriteRenderer();

    private void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        //Map.instance.mapInt[(int)XY.y, (int)XY.x] = (int)MapKind.RedPart;

    }
    private void Update()
    {
        //if (Map.instance.mapInt[(int)XY.y, (int)XY.x] != (int)MapKind.YUKA)
        //{
        //    Map.instance.mapInt[(int)XY.y, (int)XY.x] = (int)MapKind.RedPart;
        //}
    }

    public void GetPart()
    {

    }
}
