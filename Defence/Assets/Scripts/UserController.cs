using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ManagerController.Instance.GetEvent += MessageSend;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnClickButton();
        }
    }


    void MessageSend(string message)
    {
        Debug.Log(message);
    }

    private void OnDestroy()
    {
        ManagerController.Instance.GetEvent -= MessageSend;
    }

    public void OnClickButton()
    {
        ManagerController.Instance.GameOver(gameObject.name);
    }


}