using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSound : MonoBehaviour
{
    //칼사운트 클립을 추가하고 
    public List<AudioClip> SwordSoundClips;
    //칼 사운드 재생할 플레이어
    public AudioSource SwordSoundPlayer;
    //애니메이션 특정 프레임에 호출하는 이벤트
    public void PlaySwordSound(int index)
    {
        SwordSoundPlayer.PlayOneShot(SwordSoundClips[index]);
    }

}
