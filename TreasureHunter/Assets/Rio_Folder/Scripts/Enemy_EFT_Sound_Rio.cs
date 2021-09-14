using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_EFT_Sound_Rio : MonoBehaviour
{
    AudioSource player;

    void Start()
    {
        player = GetComponent<AudioSource>();      
    }


    public List<AudioClip> SwordClips;
    public void SwordSound(int index) {
        player.PlayOneShot(SwordClips[index]);
    }


    public List<AudioClip> CrabClips;
    public void CrabSound(int index)
    {
        player.PlayOneShot(CrabClips[index]);
    }
}
