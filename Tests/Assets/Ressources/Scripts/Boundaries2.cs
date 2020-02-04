using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries2 : MonoBehaviour
{
    void Update()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, width/10 + GetComponent<Renderer>().bounds.size.x / 3, width/2 - GetComponent<Renderer>().bounds.size.x / 3), Mathf.Clamp(transform.position.y, -height / 2 + GetComponent<Renderer>().bounds.size.y / 3, height / 2 - GetComponent<Renderer>().bounds.size.y / 3), transform.position.z);
    }
}
