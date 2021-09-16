using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    //[SerializeField]
    //private Gun weapon1; // 현재 들고 있는 총. 📜Gun.cs 가 할당 됨.

    //private float currentFireRate; // 이 값이 0 보다 큰 동안에는 총알이 발사 되지 않는다. 초기값은 연사 속도인 📜Gun.cs의 fireRate 

    //private bool isReload = false;  // 재장전 중인지. 
    //[HideInInspector]
    //public bool isFineSightMode = false; // 정조준 중인지.

    //[SerializeField]
    //private Vector3 originPos;  // 원래 총의 위치(정조준 해제하면 나중에 돌아와야 하니까)

    //private AudioSource audioSource;  // 발사 소리 재생기

    //private RaycastHit hitInfo;  // 총알의 충돌 정보

    //[SerializeField]
    //private Camera theCam;  // 카메라 시점에서 정 중앙에 발사할 거라서

    //[SerializeField]
    //private GameObject hitEffectPrefab;
    

    //void Start()
    //{
    //    originPos = Vector3.zero;
    //    audioSource = GetComponent<AudioSource>();
     
    //}

    //void Update()
    //{
    //    GunFireRateCalc();
    //    TryFire();
    //    TryReload();
    //    TryFineSight();
    //}

    //private void GunFireRateCalc()
    //{
    //    if (currentFireRate > 0)
    //        currentFireRate -= Time.deltaTime;  // 즉, 1 초에 1 씩 감소시킨다.
    //}

    //private void TryFire()  // 발사 입력을 받음
    //{
    //    if(Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload)
    //    {
    //        Fire();
    //    }
    //}

    //private void Fire()  // 발사를 위한 과정
    //{
    //    if (!isReload)
    //    {
    //        if (weapon1.currentBulletCount > 0)
    //            Shoot();
    //        else
    //        {
    //            CancelFineSight();
    //            StartCoroutine(ReloadCoroutine());
    //        }       
    //    }
    //}

    //private void Shoot()  // 실제 발사 되는 과정
    //{
    //    // 발사 처리
    //    weapon1.currentBulletCount--;
    //    currentFireRate = weapon1.fireRate;  // 연사 속도 재계산
        

    //    // 피격 처리
//        Hit();

//        // 총기 반동 코루틴 실행
//        StopAllCoroutines();
//        StartCoroutine(RetroActionCoroutine());
//    }

//    private void Hit()
//    {
//        // 카메라 월드 좌표!! (localPosition이 아님)
//        if (Physics.Raycast(theCam.transform.position, theCam.transform.forward, out hitInfo, weapon1.range))
//        {
//            GameObject clone = Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
//            Destroy(clone, 2f);
//        }
//    }

//    private void TryReload()
//    {
//        if (Input.GetKeyDown(KeyCode.R) && !isReload && weapon1.currentBulletCount < weapon1.reloadBulletCount)
//        {
//            CancelFineSight();
//            StartCoroutine(ReloadCoroutine());
//        }
//    }

//    IEnumerator ReloadCoroutine()
//    {
//        if(weapon1.carryBulletCount > 0)
//        {
//            isReload = true;
//            weapon1.anim.SetTrigger("Reload");

//            weapon1.carryBulletCount += weapon1.currentBulletCount;
//            weapon1.currentBulletCount = 0;

//            yield return new WaitForSeconds(weapon1.reloadTime);  // 재장전 애니메이션이 다 재생될 동안 대기

//            if (weapon1.carryBulletCount >= weapon1.reloadBulletCount)
//            {
//                weapon1.currentBulletCount = weapon1.reloadBulletCount;
//                weapon1.carryBulletCount -= weapon1.reloadBulletCount;
//            }
//            else
//            {
//                weapon1.currentBulletCount = weapon1.carryBulletCount;
//                weapon1.carryBulletCount = 0;
//            }

//            isReload = false;
//        }
//        else
//        {
//            Debug.Log("소유한 총알이 없습니다.");
//        }
//    }

//    private void TryFineSight()
//    {
//        if(Input.GetButtonDown("Fire2") && !isReload)
//        {
//            FineSight();
//        }
//    }

//    public void CancelFineSight()
//    {
//        if (isFineSightMode)
//            FineSight();
//    }

//    private void FineSight()
//    {
//        isFineSightMode = !isFineSightMode;
//        weapon1.anim.SetBool("FineSightMode", isFineSightMode);
        
//        if(isFineSightMode)
//        {
//            StopAllCoroutines();
//            StartCoroutine(FineSightActivateCoroutine());
//        }
//        else
//        {
//            StopAllCoroutines();
//            StartCoroutine(FineSightDeActivateCoroutine());
//        }
//    }

//    IEnumerator FineSightActivateCoroutine()
//    {
//        while(weapon1.transform.localPosition != weapon1.findSightOriginPos)
//        {
//            weapon1.transform.localPosition = Vector3.Lerp(weapon1.transform.localPosition, weapon1.findSightOriginPos, 0.2f);
//            yield return null;
//        }
//    }

//    IEnumerator FineSightDeActivateCoroutine()
//    {
//        while (weapon1.transform.localPosition != originPos)
//        {
//            weapon1.transform.localPosition = Vector3.Lerp(weapon1.transform.localPosition, originPos, 0.2f);
//            yield return null;
//        }
//    }

//    IEnumerator RetroActionCoroutine()
//    {
//        Vector3 recoilBack = new Vector3(weapon1.retroActionForce, originPos.y, originPos.z);     // 정조준 안 했을 때의 최대 반동
//        Vector3 retroActionRecoilBack = new Vector3(weapon1.retroActionFineSightForce, weapon1.findSightOriginPos.y, 
//            weapon1.findSightOriginPos.z);  // 정조준 했을 때의 최대 반동

//        if(!isFineSightMode)  // 정조준이 아닌 상태
//        {
//            weapon1.transform.localPosition = originPos;

//            // 반동 시작
//            while(weapon1.transform.localPosition.x <= weapon1.retroActionForce - 0.02f)
//            {
//                weapon1.transform.localPosition = Vector3.Lerp(weapon1.transform.localPosition, recoilBack, 0.4f);
//                yield return null;
//            }

//            // 원위치
//            while (weapon1.transform.localPosition != originPos)
//            {
//                weapon1.transform.localPosition = Vector3.Lerp(weapon1.transform.localPosition, originPos, 0.1f);
//                yield return null;
//            }
//        }
//        else  // 정조준 상태
//        {
//            weapon1.transform.localPosition = weapon1.findSightOriginPos;

//            // 반동 시작
//            while(weapon1.transform.localPosition.x <= weapon1.retroActionFineSightForce - 0.02f)
//            {
//                weapon1.transform.localPosition = Vector3.Lerp(weapon1.transform.localPosition, retroActionRecoilBack, 0.4f);
//                yield return null;
//            }

//            // 원위치
//            while (weapon1.transform.localPosition != weapon1.findSightOriginPos)
//            {
//                weapon1.transform.localPosition = Vector3.Lerp(weapon1.transform.localPosition, weapon1.findSightOriginPos, 0.1f);
//                yield return null;
//            }
//        }
//    }

//    private void PlaySE(AudioClip _clip)  // 발사 소리 재생
//    {
//        audioSource.clip = _clip;
//        audioSource.Play();
//    }

//    public Gun GetGun()
//    {
//        return weapon1;
//    }
}
