using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchGame : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            Play();
        }
        if(Input.GetAxisRaw("Fire1") != 0f || Input.GetAxisRaw("Fire2") != 0f)
        {
            Play();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
