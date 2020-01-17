using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    string[] serihu;
    string ko;
    
    int[] mozisuu = new int[3];

    private void Start()
    {
        for(int i = 0; i < serihu.Length; i++)
        {
            ko = serihu[i];            
            mozisuu[i] = ko.Length;
            Debug.Log(mozisuu[i]);       
        }
        //ko = serihu[0];
        ////serihu[0] = "ko";
        //mozisuu[0] = ko.Length;
        //Debug.Log(mozisuu[0]);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
