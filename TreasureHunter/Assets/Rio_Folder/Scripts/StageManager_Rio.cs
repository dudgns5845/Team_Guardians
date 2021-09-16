using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//map상의 적을 다 잡으면 스테이지를 클리한 것으로 판정하고 싶다
public class StageManager_Rio : MonoBehaviour
{
    public GameObject TXT_Clear;
    public List<GameObject> Enemys;
    int EnemysCnt;
    private void Start()
    {
        EnemysCnt = Enemys.Count;
    }

    private void Update()
    {
        if (EnemysCnt == 0)
        {
            Time.timeScale = 0.5f;
            Invoke("GoNextStage", 2);
        }
    }

   public void Callme()
    {
        TXT_Clear.SetActive(true);
        //게임 상태가 '게임중' 상태일 대만 조작할 수 잇게 한다. 
        if (GameManager.gm.gState != GameManager.GameState.Pause)
        {
            return;
        }

        Invoke("GoNextStage", 4);
    }

    //적을 모두 잡으면 현 스테이지 종료하는 함수
    void GoNextStage()
    {
        if (SceneManager.GetActiveScene().name == "Scene_02_Play01")
        {
            SceneManager.LoadScene(3);
        }

        if (SceneManager.GetActiveScene().name == "Scene_04_Play")
        {
            SceneManager.LoadScene(5);
        }
    }

    public void Restart()
    {
        print("호출!!");
        //현재씬 번호를 다시 로드한다
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    //Enemy들이 STATE.DIE 상태일때마다 호출할 함수
    public void minEnemy()
    {
        EnemysCnt--;
    }
}
