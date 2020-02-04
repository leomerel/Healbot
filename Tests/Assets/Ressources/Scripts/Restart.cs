using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;
public class Restart : MonoBehaviour
{

    public TextMeshProUGUI tm;

    private void Start()
    {
        InvokeRepeating("Blink", 0, 0.5f);
    }

    void Update()
    {
        if (Input.GetAxisRaw("Start") != 0f)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    void Blink()
    {
        if (tm.color.a == 0)
        {
            tm.color = new Color(255, 255, 255, 255);
        }
        else
        {
            tm.color = new Color(255, 255, 255, 0);
        }

    }
}