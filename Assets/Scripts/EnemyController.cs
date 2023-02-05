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

    // Start is called before the first frame update
    private void Start()
    {
        tree = GameObject.Find("Tree");
        healthbar.UpdateBar(100);
        /*if (gameObject.name.IndexOf("Flame", StringComparison.OrdinalIgnoreCase))
        {
            var gameParticle = gameObject.transform.Find("Particle");
            Debug.Log(gameParticle);
            if (gameParticle)
            {
                particle = gameParticle.GetComponent<ParticleSystem>();
                particle.Stop();
            }
        }*/
    }

    // Update is called once per frame
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
                            Debug.Log("enemy go yok 2");
                            animator.SetBool("attack", false);
                            animator.SetBool("run", true);
                            isAttack = false;
                            if (particle) particle.Stop();
                        }
                        else
                        {
                            health -= 25;
                            isHit = false;
                            Debug.Log("health: " + health);
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
        Destroy(gameObject);
    }
}