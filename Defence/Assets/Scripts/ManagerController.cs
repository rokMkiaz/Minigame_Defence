using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerController : MonoBehaviour
{
    public static ManagerController instance;
    public static ManagerController Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }
    public delegate void MessageDelegate(string s);
    public event MessageDelegate GetEvent;

    public void GameOver(string  s)//Event �߻��� Life.cs�� �ֽ��ϴ�.
    {
        if (Input.GetMouseButton(0))
        {
            GetEvent(s);
        }
    }
}