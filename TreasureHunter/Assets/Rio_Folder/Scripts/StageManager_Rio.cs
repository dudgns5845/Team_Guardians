using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//map상의 적을 다 잡으면 스테이지를 클리한 것으로 판정하고 싶다
public class StageManager_Rio : MonoBehaviour
{
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
            Invoke("GoNextStage", 2);
        }
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




    //Enemy들이 STATE.DIE 상태일때마다 호출할 함수
    public void minEnemy()
    {
        EnemysCnt--;
    }
}
