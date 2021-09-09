using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{

    //걷기와 달리기 클립을 추가하고 
    public List<AudioClip> FootStep_WalkSoundClip;
    public List<AudioClip> FootStep_RunSoundClip;

    //걷기와 달리기 사운드 재생할 플레이어
    public AudioSource Player;
   
    
    //애니메이션 특정 프레임에 호출하는 이벤트
    public void FootStep_Walk()
    {
        AudioClip walkcl = FootStep_WalkSoundClip[0];

        Player.PlayOneShot(walkcl);
    }
    public void FootStep_Run()
    {
        AudioClip runcl = FootStep_WalkSoundClip[0];

        Player.PlayOneShot(runcl);
    }
   

}
