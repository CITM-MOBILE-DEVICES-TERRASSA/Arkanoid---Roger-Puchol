using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    void Start()
    {
        //if (Camera.main)
        //    Camera.main.aspect = 109.0f / 128.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Action"))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
