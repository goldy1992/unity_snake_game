using System.Collections;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private static float SPEED = 5f;

    private static int LENGTH = 50;
 
    Direction currentDirection = Direction.NONE;

    private static readonly Vector2 UP_SPEED = new Vector2(0, SPEED);
    private static readonly Vector2 DOWN_SPEED = new Vector2(0, -1 * SPEED);
    private static readonly Vector2 LEFT_SPEED = new Vector2(-1 * SPEED, 0);
    private static readonly Vector2 RIGHT_SPEED = new Vector2(SPEED, 0);

    private Vector3 currentVector;

    GameObject food;

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
        // move each tail towards previous tail
        if (tail.Count > 0)
        {
            if (tail.Count > 1)
            {
                for (int i = tail.Count - 1; i > 0; i--)
                {
                    SnakePart currentTail = ((GameObject) tail[i]).GetComponent<SnakePart>();
                    SnakePart nextTail = ((GameObject) tail[i - 1]).GetComponent<SnakePart>();
                    currentTail.direction = nextTail.direction;
                    currentTail.transform.localPosition = calculateNewPosition(nextTail.transform.localPosition, nextTail.direction);
                }
            }

            SnakePart firstTail = ((GameObject)tail[0]).GetComponent<SnakePart>();
            firstTail.direction = player.GetComponent<SnakePart>().direction;
            firstTail.transform.localPosition = player.transform.localPosition;
//            translationVector = player.transform.localPosition - firstTail.transform.localPosition;
       //     Debug.Log("transforming tail first tail by " + translationVector);
  //          firstTail.transform.Translate(translationVector * Time.deltaTime * 10);


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
        player.GetComponent<SnakePart>().Move(currentVector);
    }

    public void FoodEaten()
    {
        Debug.Log("food eaten");
        food.GetComponent<Food>().GenerateRandomPositon();
        GameObject newBody = Instantiate(snakeBodyPrefab, new Vector3(0, 0), Quaternion.identity);
        SnakePart currentTail = null;
        if (tail.Count > 0)
        {
            currentTail = ((GameObject)tail[tail.Count - 1]).GetComponent<SnakePart>();
        } else {
            currentTail = player.GetComponent<SnakePart>();
        }
        newBody.transform.localPosition = calculateNewPosition(currentTail.transform.localPosition, currentTail.direction);
        tail.Add(newBody);
    }


    private Vector3 calculateNewPosition(Vector3 currentPosition, Direction direction)
    {
        Vector3 toReturn = currentPosition;
        switch (direction)
        {
            case Direction.UP:
                toReturn.y -= LENGTH;
                break;
            case Direction.DOWN:
                toReturn.y += LENGTH;
                break;
            case Direction.LEFT:
                toReturn.x += LENGTH;
                break;
            case Direction.RIGHT:
                toReturn.x -= LENGTH;
                break;
        }

        return toReturn;
    }

}
