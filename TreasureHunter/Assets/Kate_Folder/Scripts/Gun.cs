using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gun : MonoBehaviour
{
    public List<GameObject> effectSound = new List<GameObject>();
    public Dictionary<string, GameObject> effectSoundDic = new Dictionary<string, GameObject>();

    public Transform[] projectileSpawn;
   
    public float msBetweenShots = 0.2f;
  
    public int projectilesPerMag = 20;
    public float reloadTime = 0.2f;
    public Text text;



    int projectilesRemainingInMag;
    bool isReloading;

    float nextShotTime;

    void Start()
    {
        foreach (GameObject go in effectSound)
        {
            effectSoundDic.Add(go.name, go);
        }
        projectilesRemainingInMag = projectilesPerMag;
        text.text = projectilesRemainingInMag + "/" + projectilesPerMag;
    }

    void Update()
    {
        text.text = projectilesRemainingInMag + "/" + projectilesPerMag;
    }

    void LateUpdate()
    {
        if (!isReloading && projectilesRemainingInMag == 0)
        {
            Reload();
        }
    }

    public void Shoot()
    {
        if (!isReloading && Time.time > nextShotTime && projectilesRemainingInMag > 0)
        {
            for (int i = 0; i < projectileSpawn.Length; i++)
            {
                projectilesRemainingInMag--;
                nextShotTime = Time.time + msBetweenShots; 
            }
           
        }
    }

    public void Reload()
    {
        StartCoroutine(AnimateReload());
    }

    IEnumerator AnimateReload()
    {
        GameObject.Instantiate(effectSoundDic["ReloadSound"]);
        isReloading = true;
        yield return new WaitForSeconds(0.2f);

        float reloadSpeed = 1f / reloadTime;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * reloadSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            yield return null;
        }

        isReloading = false;
        projectilesRemainingInMag = projectilesPerMag;
    }

}
