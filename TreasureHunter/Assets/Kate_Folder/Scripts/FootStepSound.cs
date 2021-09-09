using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    //걷기 사운드  
    public List<AudioClip> FootStep_WalkClip;
    //뛰기 사운드
    public List<AudioClip> FootStep_RunClip;
    //재생할 플레이어
    public AudioSource Player;
    //애니메이션 특정 프레임에 호출하는 이벤트
    public void FootStepSounds()
    {
       
        Player.PlayOneShot(FootStep_WalkClip[1]);
        Player.PlayOneShot(FootStep_RunClip[1]);
    }

}
