using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
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

    private static readonly Vector2 UP_SPEED = new Vector2(0, SPEED);
    private static readonly Vector2 DOWN_SPEED = new Vector2(0, -1 * SPEED);
    private static readonly Vector2 LEFT_SPEED = new Vector2(-1 * SPEED, 0);
    private static readonly Vector2 RIGHT_SPEED = new Vector2(SPEED, 0);

    private Vector3 currentVector;

    GameObject food;

    public Food foodScript;

    public SnakePart playerScript;

    GameObject player;

    ArrayList tail = new ArrayList();

    public GameObject snakeBodyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        this.food = GameObject.FindGameObjectWithTag("food");
        this.player = GameObject.FindGameObjectWithTag("player");
        Debug.Log("game has started");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 translationVector;

        // move each tail towards previous tail
        if (tail.Count > 0)
        {
            if (tail.Count > 1)
            {
                for (int i = tail.Count - 1; i > 0; i--)
                {
                    GameObject currentTail = (GameObject)tail[i];
                    GameObject nextTail = (GameObject)tail[i - 1];

                    translationVector = nextTail.transform.position - currentTail.transform.position;
                    // Debug.Log("transforming tail " + i + " by " + translationVector);
                    transform.Translate(translationVector * Time.deltaTime * 10);
                    // currentTail.transform.position = nextTail.transform.position;
                }
            }

            GameObject firstTail = ((GameObject)tail[tail.Count - 1]);
            translationVector = player.transform.position - firstTail.transform.position;
       //     Debug.Log("transforming tail first tail by " + translationVector);
            firstTail.transform.Translate(translationVector * Time.deltaTime * 10);


        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && currentDirection != Direction.DOWN)
        {
            currentDirection = Direction.UP;
            currentVector = UP_SPEED;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && currentDirection != Direction.UP)
        {
            currentDirection = Direction.DOWN;
            currentVector = DOWN_SPEED;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentDirection != Direction.RIGHT)
        {
            currentDirection = Direction.LEFT;
            currentVector = LEFT_SPEED;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentDirection != Direction.LEFT)
        {
            currentDirection = Direction.RIGHT;
            currentVector = RIGHT_SPEED;
        }


        // move snake head
        playerScript.Move(currentVector);
    }

    public void FoodEaten()
    {
        Debug.Log("food eaten");
        foodScript.GenerateRandomPositon();
        GameObject newBody = Instantiate(snakeBodyPrefab, new Vector3(0, 0), Quaternion.identity);
        GameObject currentTail = null;
        if (tail.Count > 0)
        {
            currentTail = (GameObject)tail[tail.Count - 1];
        } else {
            currentTail = player;
        }
        newBody.transform.position = currentTail.transform.position - new Vector3(0, 25);
        tail.Add(newBody);
    }


}
