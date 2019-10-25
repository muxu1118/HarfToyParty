using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomTest : MonoBehaviour
{
    private GameObject Bomb;
    [SerializeField]
    private float timeExplosion; //爆発までの時間
    private float timeEpTrigger = 0;

    void Update()
    {
        Explosion();
    }

    #region Coroutin
    private void Explosion()
    {
        timeExplosion -= Time.deltaTime;
        if(timeExplosion >= timeEpTrigger)
        {
            timeExplosion = 5;
            
        }

        //ExplosionCoroutin();
    }

    IEnumerator ExplosionCoroutin()
    {
        timeExplosion -= Time.deltaTime;
        while (timeExplosion >= timeEpTrigger)
        {
            Destroy(this.gameObject);
        }
        yield return null;
    }
    #endregion
}
