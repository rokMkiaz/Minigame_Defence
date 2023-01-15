using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 mousePos;
    Camera Camera;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
       
        mousePos = Input.mousePosition;
        mousePos = Camera.ScreenToWorldPoint(mousePos);
        if(mousePos.y <-5.0f)
        {
            mousePos.y = -5.0f;
        }
        else if(mousePos.y > 0.1f )
        {
            mousePos.y = 0.1f;
        }

        if(mousePos.x > 3.0f)
        {
            mousePos.x = 3.0f;
        }
        else if(mousePos.x < -3.0f)
        {
            mousePos.x = -3.0f;
        }

        if (Time.timeScale != 0.0f)
        {
            transform.position = mousePos;
        }

       
       
    }
}

