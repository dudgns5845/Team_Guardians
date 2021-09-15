using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//map���� ���� �� ������ ���������� Ŭ���� ������ �����ϰ� �ʹ�
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


    //���� ��� ������ �� �������� �����ϴ� �Լ�
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




    //Enemy���� STATE.DIE �����϶����� ȣ���� �Լ�
    public void minEnemy()
    {
        EnemysCnt--;
    }
}
