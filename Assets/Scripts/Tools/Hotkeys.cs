using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hotkeys : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            IntroParameters.skipIntro = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            IntroParameters.skipIntro = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}