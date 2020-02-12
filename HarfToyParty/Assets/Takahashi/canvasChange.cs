using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasChange : MonoBehaviour
{
    [SerializeField]
    Transform[] transform;
    // Start is called before the first frame update
    void Start()
    {
        transform[0].SetSiblingIndex(0);
        transform[1].SetSiblingIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
