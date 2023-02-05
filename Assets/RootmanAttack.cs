using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootmanAttack : MonoBehaviour
{
    private Animator Animator;

    void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Animator.SetTrigger("attack");
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(5, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(-5, 0, 0);
        }
    }
}
