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
        
        if(pd.time >= pd.duration)
        {

            if (SceneManager.GetActiveScene().name == "Scene_01_Cinema")
            {
                SceneManager.LoadScene(2);
            }
            if (SceneManager.GetActiveScene().name == "Scene_03_Cinema")
            {
                SceneManager.LoadScene(4);
            }
            if (SceneManager.GetActiveScene().name == "Scene_05_Cinema")
            {
                SceneManager.LoadScene(6);
            }
            if (SceneManager.GetActiveScene().name == "Scene_07_FinalCinema") 
            {
                SceneManager.LoadScene(0);
            }

        }
    }
}
