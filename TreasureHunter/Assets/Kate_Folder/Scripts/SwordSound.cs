using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSound : MonoBehaviour
{
    //Į���Ʈ Ŭ���� �߰��ϰ� 
    public List<AudioClip> SwordSoundClips;
    //Į ���� ����� �÷��̾�
    public AudioSource SwordSoundPlayer;
    //�ִϸ��̼� Ư�� �����ӿ� ȣ���ϴ� �̺�Ʈ
    public void PlaySwordSound(int index)
    {
        SwordSoundPlayer.PlayOneShot(SwordSoundClips[index]);
    }

}
