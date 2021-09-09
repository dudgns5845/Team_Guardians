using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{

    //�ȱ�� �޸��� Ŭ���� �߰��ϰ� 
    public List<AudioClip> FootStep_WalkSoundClip;
    public List<AudioClip> FootStep_RunSoundClip;

    //�ȱ�� �޸��� ���� ����� �÷��̾�
    public AudioSource Player;
   
    
    //�ִϸ��̼� Ư�� �����ӿ� ȣ���ϴ� �̺�Ʈ
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
