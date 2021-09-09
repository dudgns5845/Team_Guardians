using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab_Sound : MonoBehaviour
{
    AudioSource player;
    public List<AudioClip> clips;
    void Start()
    {
        player = GetComponent<AudioSource>();
    }

    public void CrabSound(int idx)
    {
        if (player.isPlaying && idx == 0)
        {
            return;
        }
        else
        {
            player.Stop();
            player.clip = clips[idx];
            player.Play();
        }
       
    }
}
