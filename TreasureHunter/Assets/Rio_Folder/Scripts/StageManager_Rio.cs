using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//map���� ���� �� ������ ���������� Ŭ���� ������ �����ϰ� �ʹ�
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
            TXT_Clear.SetActive(true);
            //GameManager.gm.gState = GameManager.GameState.Pause;
            Invoke("GoNextStage", 2);
        }
    }

   public void Callme()
    {
        TXT_Clear.SetActive(true);
        GameManager.gm.gState = GameManager.GameState.Pause;


        Invoke("GoNextStage", 4);
    }

    //���� ���� ������ �� �������� �����ϴ� �Լ�
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

        if (SceneManager.GetActiveScene().name == "Scene_06_BossPlay")
        {
            SceneManager.LoadScene(7);
        }
    }

    public void Restart()
    {
        print("ȣ��!!");
        //������ ��ȣ�� �ٽ� �ε��Ѵ�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    //Enemy���� STATE.DIE �����϶����� ȣ���� �Լ�
    public void minEnemy()
    {
        EnemysCnt--;
    }
}
