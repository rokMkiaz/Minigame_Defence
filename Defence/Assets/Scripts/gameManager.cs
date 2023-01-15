using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI ���
using UnityEngine.SceneManagement;
using static DelegateEventStruct;

public struct DelegateEventStruct //���� ������ ����
{
    public delegate void DelegateMethod(string message);
    public delegate void FunctionHandler();

        
    public event DelegateMethod eventMethod; //Message ���
    public event FunctionHandler functionHandler;//Delegate Function : ������ ����
    public void GameOver(int hp , string s="��������")//Event �߻��� Life.cs�� �ֽ��ϴ�.
    {
        if(hp<1) 
        {
            eventMethod(s);
            functionHandler();
            Time.timeScale = 0;
        }
    }

}


public class gameManager : MonoBehaviour
{
    public event DelegateMethod eventMethod;
    //[SerializeField]private GameObject monster;
    [SerializeField]private float spawnTimer=0.5f;

    [SerializeField] private Text timeText;
    [SerializeField] private GameObject endMessage;

    private DelegateEventStruct delegateEventClass;
    public DelegateEventStruct DelegateEventClass(){ return delegateEventClass; }


    public ObjectPool monsterObjectPool;
    private float gameTime;

    public static gameManager I; //�̱���
    void Awake()
    {
        I = this;
        delegateEventClass = new DelegateEventStruct();
        delegateEventClass.functionHandler += EndGame;
        delegateEventClass.eventMethod += DebugLog;

        monsterObjectPool=this.gameObject.GetComponent<ObjectPool>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("C_MakeMonster");
        InitGame();
    }
    void Update()
    {
        gameTime += Time.deltaTime;
        timeText.text = gameTime.ToString("F");

        //Level
        if (gameTime > 10.0f) spawnTimer = 0.4f;
        if (gameTime > 20.0f) spawnTimer = 0.2f;
        if (gameTime > 30.0f) spawnTimer = 0.1f;
        if (gameTime > 40.0f) spawnTimer = 0.05f;
    }

    void InitGame()
    {
        gameTime = 0.0f;
        Time.timeScale = 1.0f;
    }
    IEnumerator C_MakeMonster() //���� ����
    {
        Invoke("MakeMonsters", 0);
        
        yield return new WaitForSeconds(spawnTimer);

        StartCoroutine("C_MakeMonster");
    }
    void MakeMonsters()
    {
        GameObject monster = monsterObjectPool.objectPoolList[0].Dequeue();
        monster.GetComponent<Monster>().MonsterCreate();
    }


    private void DebugLog(string message)   //Event Message : Log
    {
        Debug.Log(message);
    }
    private void EndGame() //Delegate Function : ������ ����
    {
        endMessage.SetActive(true);
    }
    public void Retry() //UI
    { 
        SceneManager.LoadScene("MainScene");
        
    }

 
}
