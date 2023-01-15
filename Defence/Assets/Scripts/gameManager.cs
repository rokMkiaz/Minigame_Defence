using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI 사용
using UnityEngine.SceneManagement;
using static DelegateEventStruct;

public struct DelegateEventStruct //순수 데이터 전달
{
    public delegate void DelegateMethod(string message);
    public delegate void FunctionHandler();

        
    public event DelegateMethod eventMethod; //Message 출력
    public event FunctionHandler functionHandler;//Delegate Function : 끝난후 실행
    public void GameOver(int hp , string s="게임종료")//Event 발생은 Life.cs에 있습니다.
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

    public static gameManager I; //싱글톤
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
    IEnumerator C_MakeMonster() //몬스터 생성
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
    private void EndGame() //Delegate Function : 끝난후 실행
    {
        endMessage.SetActive(true);
    }
    public void Retry() //UI
    { 
        SceneManager.LoadScene("MainScene");
        
    }

 
}
