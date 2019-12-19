using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEScript : SingletonMonoBehaviour<SEScript>
{
    /// <summary>
    /// 使いたいSEを入力
    /// </summary>
    public enum SEType
    {
        none = -1,
        cursor1,
        BGM基本ループ,
    }

    public List<AudioClip> SEList;

    public AudioSource PlaySE(SEType type, bool isLoop = false)
    {
        AudioSource se = new GameObject(type.ToString()).AddComponent<AudioSource>();
        se.clip = SEList[(int)type];
        se.gameObject.AddComponent<SEScript>();
        se.loop = isLoop;
        se.Play();

        return se;
    }
}

public class SEController : MonoBehaviour
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
