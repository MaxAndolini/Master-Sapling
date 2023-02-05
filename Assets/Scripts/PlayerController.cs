using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private int position = 3;
    private Vector3[] points = new Vector3[5];

    void Start()
    {
        // Initialize the position of the 5 points
        points[0] = new Vector3(98.0f, 102.0f, 75.0f);
        points[1] = new Vector3(102.5f, 102.0f, 75.0f);
        points[2] = new Vector3(108.0f, 102.0f, 75.0f);
        points[3] = new Vector3(114.0f, 102.0f, 75.0f);
        points[4] = new Vector3(119.0f, 102.0f, 75.0f);

        // Set the character's initial position
        transform.position = points[position];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (position < 4)
            {
                position++;
                transform.position = points[position];
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (position > 0)
            {
                position--;
                transform.position = points[position];
            }
        }
    }
}
