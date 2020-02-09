using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 0.5f;

    public float moveSpeed = 4f;

    public float healCapacity = 100;
    public float healLeft;
    public Image healBar;
    private float reload = 3f;

    public float timer = 0f;
    public float waitTime = 0.5f;

    public Rigidbody2D rb;

    public Animator animator;

    Vector2 movement;

    private float action;
    private float previousAction=0;

    public GameObject aimedBot;
    public GameObject[] bots;
    public PlayerScript bot;


    void Start(){
    	healLeft = healCapacity;
    	if (bots == null)
            bots = GameObject.FindGameObjectsWithTag("Ally");
    }

    // Update is called once per frame.
    void Update()
    {
        checkWin();

        Retard();

    	timer += Time.deltaTime;
    	if(timer>=waitTime){
    		timer -= waitTime;
    		if(healLeft<=healCapacity- reload)
            {
    			healLeft += reload;
    		}
            else if (healLeft <= healCapacity)
            {
                healLeft = healCapacity;
            }
        }

    	
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        action = Input.GetAxisRaw("Fire2");

        aimedBot = FindClosestEnemy();

        if (previousAction!=action){
			if(action == 1){
	        	if(aimedBot!=null){
	        		bot = aimedBot.GetComponent<PlayerScript>();
	        		float amount = 5f;
	        		if(healLeft-amount>=0){
	        			bot.Heal(amount);
	        			healLeft-=amount;
                        Shoot();
                    }
	        	}
	        }
	        previousAction=action;
		}
        healBar.fillAmount = healLeft / healCapacity;

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void Retard()
    {
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (allies.Length < enemies.Length + 2)
        {
            reload = 6f;
        }
        else
        {
            reload = 3f;
        }
    }

    void Shoot()
    {
        GameObject cross = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = cross.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    void FixedUpdate(){
    	rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        rb.rotation = angle-90f;
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Ally");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            go.GetComponent<PlayerScript>().isTargeted = false;
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        if (closest != null)
        {
            closest.GetComponent<PlayerScript>().isTargeted = true;
        }

        return closest;
    }

    private void checkWin()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        if(gos.Length == 0)
        {
            SceneManager.LoadScene("WinP1");
        }
        gos = GameObject.FindGameObjectsWithTag("Ally");
        if(gos.Length == 0)
        {
            SceneManager.LoadScene("WinP2");
        }
    }
}
