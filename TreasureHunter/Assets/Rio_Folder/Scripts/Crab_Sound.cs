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
        AudioClip clip = clips[idx];
        player.PlayOneShot(clip);
    }
}
