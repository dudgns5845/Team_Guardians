using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Cinemachine;

public class DirectorAction_Rio : MonoBehaviour
{
    PlayableDirector pd;
    
    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
        
    }
    // Update is called once per frame
    void Update()
    {
        print(pd.time);
        if(pd.time >= pd.duration)
        {
            print("场车促绊 场车绢!!!");
            SceneManager.LoadScene(2);
        }
    }
}
