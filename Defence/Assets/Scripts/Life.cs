using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int hp;

    public DelegateEventStruct delegateEventClass;

    private void Start()
    {
        hp=1;
        delegateEventClass = gameManager.I.DelegateEventClass(); //구독
        delegateEventClass.functionHandler += LifeDestroy; //Life false

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag=="Monster")
        {
            hp--;
            delegateEventClass.GameOver(hp);

        }
    }
    private void LifeDestroy() //끝난 후 실행
    {
        gameObject.SetActive(false);
    }


}
