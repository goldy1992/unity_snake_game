
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{

    private static float SPEED = 10f;


    public GameHandler gameHandler;

    enum Direction 
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT

    }


    private Vector2Int gridPosition;

    private Vector3 currentVector;

    Direction currentDirection = Direction.NONE;

    private static readonly Vector2 UP_SPEED = new Vector2(0, SPEED);
    private static readonly Vector2 DOWN_SPEED = new Vector2(0, -1 * SPEED);
    private static readonly Vector2 LEFT_SPEED = new Vector2(-1 * SPEED, 0);
    private static readonly Vector2 RIGHT_SPEED = new Vector2(SPEED, 0);

    private void Awake()
    {
  
    }

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("hit controls start");
        gridPosition = new Vector2Int(0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentDirection = Direction.UP;
            currentVector = UP_SPEED;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentDirection = Direction.DOWN;
            currentVector = DOWN_SPEED;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentDirection = Direction.LEFT;
            currentVector = LEFT_SPEED;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentDirection = Direction.RIGHT;
            currentVector = RIGHT_SPEED;
        }
           transform.Translate(currentVector * Time.deltaTime * 10);

    }

}
