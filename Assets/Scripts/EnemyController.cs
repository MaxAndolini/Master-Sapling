using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 100;
    public GameObject tree;
    public bool isDead;
    public Healthbar healthbar;
    public Animator animator;
    private bool isAttack;
    private bool isHit = true;
    private ParticleSystem particle;
    private GameObject rootman;
    
    private void Start()
    {
        tree = GameObject.Find("Tree");
        healthbar.UpdateBar(100);
    }
    
    private void Update()
    {
        if (!isAttack)
            Move();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Rootman"))
        {
            isAttack = true;
            animator.SetBool("attack", true);
            if (particle) particle.Play();
            Health(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Death();
            GameManager.Instance.DecreaseHealth();
        }
    }

    public void Move()
    {
        StartCoroutine(WaitForWalk(1.5f));
    }

    public IEnumerator WaitForWalk(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        transform.position += new Vector3(0, 0, -2) * Time.deltaTime;
    }

    private IEnumerator WaitForHit(float waitTime, GameObject other)
    {
        yield return new WaitForSeconds(waitTime);
        isHit = true;
        Health(other);
    }

    public void Health(GameObject go)
    {
        if (go)
        {
            var bootman = go.GetComponent<RootManController>();
            if (bootman)
            {
                var enemyDead = bootman.isDead;
                if (!isDead && isHit)
                {
                    if (health <= 0)
                    {
                        Death();
                    }
                    else
                    {
                        if (enemyDead)
                        {
                            animator.SetBool("attack", false);
                            animator.SetBool("run", true);
                            isAttack = false;
                            if (particle) particle.Stop();
                        }
                        else
                        {
                            health -= 25;
                            isHit = false;
                            AudioManager.Instance.PlaySound("Grauh");
                            healthbar.UpdateBar(health);
                            if (health > 0)
                                StartCoroutine(WaitForHit(1.5f, go));
                            else
                                Death();
                        }
                    }
                }
            }
            else
            {
                animator.SetBool("attack", false);
                animator.SetBool("run", true);
                isAttack = false;
                if (particle) particle.Stop();
            }
        }
        else
        {
            animator.SetBool("attack", false);
            animator.SetBool("run", true);
            isAttack = false;
            if (particle) particle.Stop();
        }
    }

    public void Death()
    {
        isDead = true;
        AudioManager.Instance.PlaySound("Droplet2");
        Destroy(gameObject);
    }
}