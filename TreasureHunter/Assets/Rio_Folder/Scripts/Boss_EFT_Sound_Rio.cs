using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_EFT_Sound_Rio : MonoBehaviour
{
    AudioSource player;

    public List<AudioClip> clips;


    public ParticleSystem ps;
    public Transform rPos;

    private void Start()
    {
        player = GetComponent<AudioSource>();
    }

    //0 ǥȿ
    //1 ������
    //2 ���� 1
    //3 ���� 2
    public void BossSound(int idx)
    {
        player.PlayOneShot(clips[idx]);
    }

    public void psPlay()
    {
        ps.transform.position = rPos.position;
        ps.Play();
    }
}
