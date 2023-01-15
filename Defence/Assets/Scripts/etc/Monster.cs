using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Monster : MonoBehaviour
{
    private int type;
    private float size;

    private ObjectPool pool;

    private void Start()
    {
        pool = gameManager.I.monsterObjectPool;
    }
    void Update()
    {
        if (transform.position.y < -6.0f || transform.position.x < -4.0f || transform.position.x > 4.0f)
        {
            MonsterDestroy();
        }
    }
    public void MonsterCreate()
    {   
        
        float x = Random.Range(-2.71f, 2.7f);
        float y = Random.Range(3.0f, 5.0f);


        type = Random.Range(1, 5);

        switch (type)
        {
            case 1:
                {
                    size = 1.2f;
                    transform.position = new Vector3(x, y, type);
                    GetComponent<SpriteRenderer>().color = new Color(255 / 255.0f, 255 / 255.0f, 0 / 255.0f, 255);
                    break;
                }
            case 2:
                {
                    size = 1.0f;
                    transform.position = new Vector3(x, y, type);
                    GetComponent<SpriteRenderer>().color = new Color(0 / 255.0f, 255 / 255.0f, 0 / 255.0f, 255);
                    break;
                }
            case 3:
                {
                    size = 0.8f;
                    transform.position = new Vector3(x, y, type);
                    GetComponent<SpriteRenderer>().color = new Color(0 / 255.0f, 0 / 255.0f, 255 / 255.0f, 255);
                    break;
                }
            case 4:
                {
                    size = 2.0f;
                    transform.position = new Vector3(x, y, type);
                    GetComponent<SpriteRenderer>().color = new Color(110 / 255.0f, 110 / 255.0f, 110 / 255.0f, 255);
                    break;
                }

        }


        transform.localScale = new Vector3(size, size, 0);
        transform.rotation=  new Quaternion(0, 0, 0, 0);
        gameObject.SetActive(true);
    }

    public void MonsterDestroy()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        pool.objectPoolList[0].Enqueue(this.gameObject);
        this.gameObject.SetActive(false);

    }




}
