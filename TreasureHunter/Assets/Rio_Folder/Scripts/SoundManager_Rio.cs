using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_Rio : MonoBehaviour
{
    public static SoundManager_Rio soundManager_Rio;

    public List<AudioClip> BGM_clips;
    public List<AudioClip> EFT_clips;

    public AudioSource BGM_player;
    public AudioSource EFT_player;

    public enum BGM_STATE {
        BGM_00,
        BGM_01,
        BGM_02,
        BGM_03
    }

    public enum EFT_STATE {
        CLICK,
    }

    private void Awake()
    {
        if(soundManager_Rio == null)
        {
            soundManager_Rio = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        BGM_PLAY(BGM_STATE.BGM_00);
    }

    public void BGM_PLAY(BGM_STATE type)
    {
        BGM_player.clip = BGM_clips[(int)type];
        BGM_player.Play();
    }
    public void BGM_STOP()
    {
        BGM_player.Stop();

    }
    public void EFT_PLAY(EFT_STATE type)
    {
        AudioClip clip = BGM_clips[(int)type];
        EFT_player.PlayOneShot(clip);
    }
}
