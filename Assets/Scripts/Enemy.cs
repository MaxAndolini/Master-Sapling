using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
