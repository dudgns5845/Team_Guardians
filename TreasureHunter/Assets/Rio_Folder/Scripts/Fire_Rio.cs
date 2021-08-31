using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 목표 : ray를 발사해 enemy한테 맞음을 알린다
/// </summary>
public class Fire_Rio : MonoBehaviour
{
    public Transform FirePos;
    public GameObject fragmentEFT;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            LayFire();
        }
    }

    void LayFire()
    {
        //ray 생성
        //ray의 생성자 파라미터 - 생성 위치, 방향
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        //맞은 위치의 정보
        RaycastHit hitInfo;

        //Ray에 충돌하고 싶은 Layer
        //int layerObs = 1 << LayerMask.NameToLayer("Obstacle");
        int layer = 1 << LayerMask.NameToLayer("Player");

        //ray를 발사시켜서 어딘가에 부딪혔다면
        if(Physics.Raycast(ray, out hitInfo, 100, ~layer))
        {
            //파티클을 맞은 위치에 배치
            fragmentEFT.transform.position = hitInfo.point;

            //효과의 forward를 부딪힌 면의 normal벡터로 한다
            fragmentEFT.transform.forward = hitInfo.normal;

            //만든 효과의 particleSystem 컴포넌트 가져오기
            ParticleSystem ps = fragmentEFT.GetComponent<ParticleSystem>();

            //파티클 play
            ps.Play();

            // 맞은 녀석이 enemy라면
            enemy_Rio enemy = hitInfo.transform.GetComponent<enemy_Rio>();
            if (enemy) //참이면
            {
                // enemy에게 맞았다는 것을 알려준다
                enemy.OnDamageProcess(ray.direction);
            }
        }

        //파티클 효과음도 재생하자
        AudioSource audio = fragmentEFT.GetComponent<AudioSource>();
        audio.Play();
    }
}
