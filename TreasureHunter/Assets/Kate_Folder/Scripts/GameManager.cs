using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    //���� ���� UI ����
    public GameObject gameLabel;

    //���ӻ��� UI�ؽ�Ʈ ������Ʈ ����
    Text gameText; 

    //�̱��� ���� 
    public static GameManager gm;

    private void Awake()
    {
        if(gm==null)
        {
            gm = this;
        }
    }

    //���� ���� ���� 
    public enum GameState
    {
        Ready, 
        Run,
        Pause,
        GameOver
    }

    //���� ���� ���� ����
    public GameState gState;

    //�÷��̾� ���� Ŭ���� ����
    PlayerMove player;

    //�ɼ�ȭ�� UI ������Ʈ ����
    public GameObject gameOption;

    //�ɼ� ȭ�� �ѱ�
    public void OpenOptionWindow()
    {
        //�ɼ� â�� Ȱ��ȭ �Ѵ�. 
        gameOption.SetActive(true);
        //���Ӽӵ��� 0������� ��ȯ�Ѵ�.
        Time.timeScale = 0f;
        //���� ���¸� �Ͻ����� ���·� �����Ѵ�. 
        gState = GameState.Pause;
    }
    
    //����ϱ� �ɼ�
    public void CloseOptionWindow()
    {
        //�ɼ�â�� ��Ȱ��ȭ �Ѵ�
        gameOption.SetActive(false);
        //���Ӽӵ��� 1������� ��ȯ�Ѵ�. 
        Time.timeScale = 1f;
        //���ӻ��¸� ������ ���·� �����Ѵ�.
        gState = GameState.Run;
    }

    //�ٽ��ϱ� �ɼ�
    public void RestartGame()
    {
        //���� �ӵ��� 1������� ��ȯ�Ѵ�
        Time.timeScale = 1f;
        //����� ��ȣ�� �ٽ� �ε��Ѵ�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //���� ���� �ɼ�
    public void QuitGame()
    {
        //���ø����̼��� �����Ѵ�. 
        Application.Quit(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        //�ʱ� ���� ���´� �غ� ���·� �����Ѵ�. 
        gState = GameState.Ready;

        //���� ���� UI ������Ʈ���� Text ������Ʈ�� �����´�.
        gameText = gameLabel.GetComponent<Text>();

        //��� �ؽ�Ʈ�� ������ "Ready"���Ѵ�. 
        gameText. text = "Ready...";

        //����ؽ�Ʈ�� ������ ��Ȳ������ �Ѵ�. 
        gameText.color = new Color32(255, 185, 0, 255);

        //���� �غ� -> �غ��� ���·� ��ȯ�ϱ�
        StartCoroutine(ReadyToStart());

        //�÷��̾� ������Ʈ�� ã�� �� �÷��̾��� PlayerMove ������Ʈ �޾ƿ���
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
;    }

    IEnumerator ReadyToStart()
    {
        //2�ʰ� ����Ѵ�.. 
        yield return new WaitForSeconds(2f);

        //���� �ؽ�Ʈ�� ������ "Go!"�� �Ѵ�. 
        gameText.text = "Go!";

        //0.5�ʰ� ����ϳ�.. 
        yield return new WaitForSeconds(0.5f);

        //���� �ؽ�Ʈ�� ��Ȱ��ȭ �Ѵ�. 
        gameLabel.SetActive(false);

        //���¸� ������ ���·� �����Ѵ�.
        gState = GameState.Run;
    }

    // Update is called once per frame
    void Update()
    {
        //���� �÷��̾��� hp�� 0 ���϶��....
        if (player. hp <=0)
        {

            //�÷��̾��� �ִϸ��̼��� �����
            player.GetComponentInChildren<Animator>().SetFloat("MoveMotion", 0f);

            //���� �ؽ�Ʈ�� Ȱ��ȭ �Ѵ� 
            gameLabel.SetActive(true);
            //�����ؽ�Ʈ�� ������ "���ӿ���"�� �Ѵ�. 
            gameText.text = "Game Over";

            //���� �ؽ�Ʈ�� ������ ���� ������ �Ѵ�. 
            gameText.color = new Color32(255, 0, 0, 255);

            //���� �ؽ�Ʈ�� �ڽ� ������Ʈ�� Ʈ������ ������Ʈ�� �����´�
            Transform buttons = gameText.transform.GetChild(0);

            //��ư ������Ʈ�� Ȱ��ȭ�Ѵ�
            buttons.gameObject.SetActive(true);

            //���¸� '���ӿ���' ���·� �����Ѵ�. 
            gState = GameState.GameOver;
        }
    }
}