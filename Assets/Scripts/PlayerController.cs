using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject effect;
    private readonly Vector3[] points = new Vector3[5];
    private int position = 2;

    private void Start()
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

    private void Update()
    {
        if (!GameManager.Instance.paused)
        {
            if (Input.GetKeyDown(KeyCode.D))
                if (position < 4)
                {
                    position++;
                    transform.position = points[position];
                }

            if (Input.GetKeyDown(KeyCode.A))
                if (position > 0)
                {
                    position--;
                    transform.position = points[position];
                }

            if (GameManager.Instance.score >= 1000 && GameManager.Instance.level != 2)
            {
                Instantiate(effect, transform.position, Quaternion.identity);
                GameManager.Instance.numberOfRoots += 2;
                GameManager.Instance.damagePersentage *= 1.5f;
                GameManager.Instance.ChangeLevel(2);
                AudioManager.Instance.PlaySound("PowerUpLong");
            }
            else if (GameManager.Instance.score >= 3000 && GameManager.Instance.level != 3)
            {
                Instantiate(effect, transform.position, Quaternion.identity);
                GameManager.Instance.numberOfRoots += 2;
                GameManager.Instance.damagePersentage *= 1.5f;
                GameManager.Instance.ChangeLevel(3);
                AudioManager.Instance.PlaySound("PowerUpLong");
            }
        }
    }

    public void ResetPosition()
    {
        position = 2;
        transform.position = points[position];
    }
}