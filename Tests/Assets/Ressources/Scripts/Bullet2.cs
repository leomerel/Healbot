using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public int damage = 5;
    public GameObject hitEffect;

    private float timer = 0f;
    private float limit = 3f;

    private int maxAllies = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            return;
        }
        Bullet bull = collision.GetComponent<Bullet>();
        if (bull != null)
        {
            return;
        }
        else
        {
            PlayerScript ps = collision.GetComponent<PlayerScript>();
            if (ps != null)
            {
                ps.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > limit)
        {
            Destroy(gameObject);
        }

        increaseDamage();
    }

    void increaseDamage()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        if (maxAllies - gos.Length > 0)
        {
            damage = 5 + 2 * (maxAllies - gos.Length);
        }

    }
}
