using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{
    private float timer = 0f;
    private float limit = 0.2f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > limit)
        {
            Destroy(gameObject);
        }
    }
}
