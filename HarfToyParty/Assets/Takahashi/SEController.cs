using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController: SingletonMonoBehaviour<SEController>
{
    /// <summary>
    /// 使いたいSEを入力
    /// </summary>
    public enum SEType
    {
        none = -1,
        Bomb,
        cursor1,
        BGM基本ループ,
        MainGame,
    }

    public List<AudioClip> SEList;

    public AudioSource PlaySE(SEType type,float Volume, bool isLoop = false)
    {
        AudioSource se = new GameObject(type.ToString()).AddComponent<AudioSource>();
        se.volume = Volume;
        se.clip = SEList[(int)type];
        se.gameObject.AddComponent<SEScript>();
        se.loop = isLoop;
        se.Play();
        return se;
    }
}

public class SEScript : MonoBehaviour
{
    AudioSource audionSourse;

    private void Awake()
    {
        audionSourse = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!audionSourse.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
