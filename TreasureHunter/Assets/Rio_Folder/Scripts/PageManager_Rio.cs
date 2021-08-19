using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PageManager_Rio : MonoBehaviour
{
    public static PageManager_Rio pageManager;

    private void Awake()
    {
        if(pageManager == null)
        {
            pageManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void gotoNextPage()
    {
        SceneManager.LoadScene(1);
    }
}
