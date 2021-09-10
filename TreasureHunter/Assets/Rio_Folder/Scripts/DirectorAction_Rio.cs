using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Cinemachine;

public class DirectorAction_Rio : MonoBehaviour
{
    PlayableDirector pd;
    public Camera targetCam;
    
    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
        
    }
    // Update is called once per frame
    void Update()
    {
      
        if(pd.time >= pd.duration)
        {
            print("场车促绊 场车绢!!!");
            SceneManager.LoadScene(2);
        }
    }
}
