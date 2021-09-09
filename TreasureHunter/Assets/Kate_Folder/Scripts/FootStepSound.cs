using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    //�ȱ� ����  
    public List<AudioClip> FootStep_WalkClip;
    //�ٱ� ����
    public List<AudioClip> FootStep_RunClip;
    //����� �÷��̾�
    public AudioSource Player;
    //�ִϸ��̼� Ư�� �����ӿ� ȣ���ϴ� �̺�Ʈ
    public void FootStepSounds()
    {
       
        Player.PlayOneShot(FootStep_WalkClip[1]);
        Player.PlayOneShot(FootStep_RunClip[1]);
    }

}
