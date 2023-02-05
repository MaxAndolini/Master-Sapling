using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManController : MonoBehaviour
{
    public float health = 100f;
    public GameObject tree;
    bool isHit = true;
    public bool isDead = false;
    bool isAttack = false;
    //public AudioSource pain_sound;
    public Healthbar healthbar;
    

    // Start is called before the first frame update
    void Start()
    {
     tree = GameObject.Find("Tree");
     healthbar.UpdateBar(100);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttack)
            Move();
    }
    
    public void Move() {
        
            //transform.position = new Vector3(transform.position.x, tree.transform.position.y, transform.position.z);
            
            StartCoroutine(WaitForWalk(1.5f));
        
            
    }
    public IEnumerator WaitForWalk(float waitTime)
     {
        yield return new WaitForSeconds(waitTime);
        
        transform.position += new Vector3(0,0,2) * Time.deltaTime;
        
     }
     IEnumerator WaitForHit(float waitTime, GameObject other)
    {
        yield return new WaitForSeconds(waitTime);
        isHit = true;
        Health(other);
    }
    private void OnCollisionEnter(Collision other) {
         if (other.gameObject.CompareTag("Enemy"))
        {
            isAttack = true;
            Health(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FinishCollider"))
        {
            Death();
        }
    }


    private void OnCollisionExit(Collision other) {
        isAttack = false;
        
    }
    
    public void Health(GameObject other)
    {
        if (other)
        {
            bool enemyDead = other.GetComponent<EnemyController>().isDead;
            if (!isDead && isHit)
            {

                if (health <= 0)
                    Death();
                else
                {
                    if (enemyDead)
                    {
                        isAttack = false;
                        GameManager.Instance.ScoreAdd(150);
                    }
                    else
                    {
                        health -= 20 * GameManager.Instance.damagePersentage;
                        isHit = false;
                        AudioManager.Instance.PlaySound("Grauh");
                        healthbar.UpdateBar(health);
                        StartCoroutine(WaitForHit(1.5f, other));

                    }

                }

            }
        }
    }

    public void Death()
    {
        isDead = true;
        GameManager.Instance.AddNumberOfRoots();
        AudioManager.Instance.PlaySound("Ay");
        Destroy(gameObject);
    }
}
