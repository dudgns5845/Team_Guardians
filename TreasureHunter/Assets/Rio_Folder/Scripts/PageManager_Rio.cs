using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PageManager_Rio : MonoBehaviour
{
    public GameObject IMG_Skull;
    public GameObject BTN_Start;
    public GameObject TXT_Title;
    private void Start()
    {
        iTween.MoveTo(TXT_Title, iTween.Hash("x", 0, "y", 28, "z", 65, "time", 1.5f, "easetype", iTween.EaseType.easeInExpo));
        iTween.ScaleTo(IMG_Skull, iTween.Hash("x", 3, "y", 3, "z", 3, "time", 1.5f, "easetype", iTween.EaseType.easeInExpo));
        iTween.ScaleTo(BTN_Start, iTween.Hash("x", 4, "y", 4, "z", 4, "time", 1.5f, "easetype", iTween.EaseType.easeInOutBack));
    }

   

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
