using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 30f;

    private float waitTime;
    private float timer = 0f;

    void Start()
    {
        waitTime = Random.Range(0.6f, 1.5f);
    }


    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if(timer > waitTime)
        {
            timer = timer - waitTime;
            Shoot();
            waitTime = Random.Range(0.6f, 1.5f);
        }

        /*if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }*/
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent < Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
