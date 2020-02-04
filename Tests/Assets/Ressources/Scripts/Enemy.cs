using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float moveSpeed = 2;
    private float moveSpeed2 = 2;
    private bool constantSpeed;
    private bool constantSpeed2;

    public bool isMoving;
    public bool isTargeted;

    private float walkCounter;
    private float waitCounter;

    private int walkDirection;

    public Animator animator;

    private float timer = 0f;
    private float waitTime;

    public Rigidbody2D player;

    Vector2 allypos;

    GameObject target;

    public float startHealth = 100;
    public float health;
    public Image healthBar;


    public void Heal(float amount){
        if (health <= startHealth / 4)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
            if(gos.Length > 3)
            {
                health += 4 * amount;
            }
            else
            {
                health += amount;
            }
        }
        else if (health<=(startHealth-amount)){
            
    		health += amount;
    	}
    	else if(health<startHealth){
    		health=startHealth;
    	}
    	healthBar.fillAmount = health / startHealth;
    }

    void Start()
    {
        player.isKinematic = true;
        health = startHealth;

        waitCounter = Random.Range(0, 4);
        walkCounter = Random.Range(1, 7);

        ChooseDirection();
    }

    void Update()
    {

        Move();

        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            timer -= waitTime;

            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Ally");

            if (gos.Length > 0)
            {
                int index = Random.Range(0, gos.Length);

                target = gos[index];
            }

            waitTime = Random.Range(2.5f, 5f);
        }

        if (target != null)
        {
            allypos = target.transform.position;
        }        

        healthBar.fillAmount = health / startHealth;

        
        animator.SetBool("Ismoving", isMoving);
        animator.SetBool("IsTarget",isTargeted);
        //enemypos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = allypos - player.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        player.rotation = angle - 90f;

    }

    private void Move()
    {
        if (isMoving)
        {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        /*transform.position = new Vector3(Mathf.Clamp(transform.position.x, width / 10 + GetComponent<Renderer>().bounds.size.x / 3, 
            width / 2 - GetComponent<Renderer>().bounds.size.x / 3),
            Mathf.Clamp(transform.position.y, -height / 2 + GetComponent<Renderer>().bounds.size.y / 3,
            height / 2 - GetComponent<Renderer>().bounds.size.y / 3),
            transform.position.z);*/
        float marge = 0f;
            if (transform.position.x <= (width / 10 + GetComponent<Renderer>().bounds.size.x / 3))
            {
                walkDirection = Random.Range(0, 17);
                while (walkDirection == 5 || walkDirection == 6 || walkDirection == 7 || walkDirection == 10 || walkDirection == 11 || walkDirection == 14)
                {
                    walkDirection = Random.Range(0, 17);
                }
            }
            else if (transform.position.x >= (width / 2 - GetComponent<Renderer>().bounds.size.x / 3 + marge))
            {
                walkDirection = Random.Range(0, 17);
                while (walkDirection == 1 || walkDirection == 2 || walkDirection == 3 || walkDirection == 8 || walkDirection == 9 || walkDirection == 15)
                {
                    walkDirection = Random.Range(0, 17);
                }
            }
            else if (transform.position.y >= (height / 2 - GetComponent<Renderer>().bounds.size.y / 3 - marge))
            {
                walkDirection = Random.Range(0, 17);
                while (walkDirection == 0 || walkDirection == 1 || walkDirection == 7 || walkDirection == 8 || walkDirection == 10 || walkDirection == 13)
                {
                    walkDirection = Random.Range(0, 17);
                }
            }
            else if (transform.position.y <= (-height / 2 + GetComponent<Renderer>().bounds.size.y / 3 + marge))
            {
                walkDirection = Random.Range(0, 17);
                while (walkDirection == 3 || walkDirection == 4 || walkDirection == 5 || walkDirection == 9 || walkDirection == 11 || walkDirection == 12)
                {
                    walkDirection = Random.Range(0, 17);
                }
            }


            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    player.velocity = new Vector2(0, moveSpeed);
                    break;

                case 1:
                    player.velocity = new Vector2(moveSpeed, moveSpeed);
                    break;

                case 2:
                    player.velocity = new Vector2(moveSpeed, 0);
                    break;

                case 3:
                    player.velocity = new Vector2(moveSpeed, -moveSpeed);
                    break;

                case 4:
                    player.velocity = new Vector2(0, -moveSpeed);
                    break;

                case 5:
                    player.velocity = new Vector2(-moveSpeed, -moveSpeed);
                    break;

                case 6:
                    player.velocity = new Vector2(-moveSpeed, 0);
                    break;

                case 7:
                    player.velocity = new Vector2(-moveSpeed, moveSpeed);
                    break;

                case 8:
                    player.velocity = new Vector2(moveSpeed, moveSpeed2);
                    break;

                case 9:
                    player.velocity = new Vector2(moveSpeed, -moveSpeed2);
                    break;

                case 10:
                    player.velocity = new Vector2(-moveSpeed, moveSpeed2);
                    break;

                case 11:
                    player.velocity = new Vector2(-moveSpeed, -moveSpeed2);
                    break;

                case 12:
                    player.velocity = new Vector2(0, -moveSpeed2);
                    break;

                case 13:
                    player.velocity = new Vector2(0, moveSpeed2);
                    break;

                case 14:
                    player.velocity = new Vector2(-moveSpeed2, 0);
                    break;

                case 15:
                    player.velocity = new Vector2(moveSpeed2, 0);
                    break;
            }

            if (walkCounter < 0)
            {
                isMoving = false;
                waitCounter = Random.Range(0, 4); ;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            if (!constantSpeed)
            {
                bool test = Random.value > 0.5 ? true : false;
                if (test)
                {
                    moveSpeed -= 2 * Time.deltaTime;
                }
                else
                {
                    moveSpeed += Time.deltaTime;
                }
            }
            if (!constantSpeed2)
            {
                bool test2 = Random.value > 0.5 ? true : false;
                if (test2)
                {
                    moveSpeed2 -= 2 * Time.deltaTime;
                }
                else
                {
                    moveSpeed2 += Time.deltaTime;
                }
            }
            player.velocity = Vector2.zero;
            if (waitCounter < 0)
            {
                ChooseDirection();
            }

        }
    }

    public void ChooseDirection()
    {
        constantSpeed = Random.value > 0.5 ? true : false;
        constantSpeed2 = Random.value > 0.5 ? true : false;
        moveSpeed = Random.Range(1f, 4f);
        moveSpeed2 = Random.Range(1f, 4f);
        walkDirection = Random.Range(0, 17);
        isMoving = true;
        walkCounter = Random.Range(1, 7);

    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
