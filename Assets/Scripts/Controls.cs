
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Controls : MonoBehaviour
{

    private static float SPEED = 10f;
    enum Direction 
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT

    }

    Direction currentDirection = Direction.NONE;

    private static Vector2 UP_SPEED = new Vector2(0, SPEED);
    private static Vector2 DOWN_SPEED = new Vector2(0, -1 * SPEED);
    private static Vector2 LEFT_SPEED = new Vector2(-1 * SPEED, 0);
    private static Vector2 RIGHT_SPEED = new Vector2(SPEED, 0);


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentDirection = Direction.UP;
            rb.velocity = UP_SPEED;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentDirection = Direction.DOWN;
            rb.velocity = DOWN_SPEED;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentDirection = Direction.LEFT;
            rb.velocity = LEFT_SPEED;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentDirection = Direction.RIGHT;
            rb.velocity = RIGHT_SPEED;
        }


    }
}
