using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}