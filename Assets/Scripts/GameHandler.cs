using System;
using System.Collections;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    private static int LENGTH = 1;

    private bool chooseGreen = true;


    GameObject food;

    GameObject player;

    ArrayList tail = new ArrayList();

    public GameObject snakeBodyPrefab;


    private Quaternion currentRotation;

    private Vector3 currentDirection;
    // Start is called before the first frame update
    void Start()
    {
        this.food = GameObject.FindGameObjectWithTag("food");
        this.player = GameObject.FindGameObjectWithTag("player");
        Debug.Log("game has started");
        this.currentRotation = player.transform.rotation;
        Debug.Log("player initial rotation = " + player.transform.rotation);
        this.currentDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentDirection != Vector3.down) {
            if (!isMoving())
            {
                this.player.GetComponent<SnakePart>().currentDirection = Vector3.up;
                this.currentDirection = Vector3.up;
            }
            else
            {
                Vector3 newVector = Vector3.up;
                Vector3 newRotation = calculateNewRotation(newVector);
                this.currentDirection = newVector;
                enqueue(new ChangePoint(newRotation, player.transform.localPosition));
            }
        } 
        else if (Input.GetKeyDown(KeyCode.DownArrow) && currentDirection != Vector3.up) {
            Vector3 newVector = Vector3.down;
            Vector3 newRotation = calculateNewRotation(newVector);
            this.currentDirection = newVector;
            enqueue(new ChangePoint(newRotation, player.transform.localPosition));
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentDirection != Vector3.right) {
            if (!isMoving())
            {
                this.player.GetComponent<SnakePart>().currentDirection = Vector3.up;
                this.currentDirection = Vector3.up;
            }
                else
            {
                Vector3 newVector = Vector3.left;
                Vector3 newRotation = calculateNewRotation(newVector);
                this.currentDirection = newVector;
                enqueue(new ChangePoint(newRotation, player.transform.localPosition));
            }
        } 
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentDirection != Vector3.left) {
            if (!isMoving())
            {
                this.player.GetComponent<SnakePart>().currentDirection = Vector3.up;
            }
            else
            {
                Vector3 newVector = Vector3.right;
                Vector3 newRotation = calculateNewRotation(newVector);
                this.currentDirection = newVector;
                enqueue(new ChangePoint(newRotation, player.transform.localPosition));
            }
        } 
        Move();
    }

    public void FoodEaten()
    {
        food.GetComponent<Food>().GenerateRandomPositon();

        GameObject currentTail;
        if (tail.Count > 0)
        {
            currentTail = ((GameObject)tail[tail.Count - 1]);
        } else {
            currentTail = player;
        }

        GameObject newBody = Instantiate(snakeBodyPrefab, calculateNewPosition(currentTail), currentTail.transform.localRotation);
        SnakePart newSnakePart = newBody.GetComponent<SnakePart>();
        SnakePart currentSnakePart = currentTail.GetComponent<SnakePart>();

        newSnakePart.setQueue(currentTail.GetComponent<SnakePart>().getChangePointQueue());
        newSnakePart.currentDirection = currentSnakePart.currentDirection;
        SpriteRenderer renderer = newBody.GetComponent<SpriteRenderer>();
        if (chooseGreen)
        {
            renderer.color = Color.green;
        }
        else
        {
            renderer.color = Color.white;
        }

        chooseGreen = !chooseGreen;

          tail.Add(newBody);
    }


    private Vector3 calculateNewPosition(GameObject fromSnakePart)
    {
        Vector3 toReturn = fromSnakePart.transform.localPosition;
        SnakePart snakePart = fromSnakePart.GetComponent<SnakePart>();

     //   snakePart.
        Vector3 currentDirection = snakePart.currentDirection;

        Vector3 currentRotation = fromSnakePart.transform.localPosition;

        toReturn.y += LENGTH * Mathf.Cos(currentRotation.z);
        toReturn.x += LENGTH * Mathf.Sin(currentRotation.z);
        Debug.Log("current x: " + fromSnakePart.transform.localPosition.x
                + ", current y: " + fromSnakePart.transform.localPosition.y
                + ", new x: " + toReturn.x
                + ", new y: " + toReturn.y);


        //if (currentDirection == Vector3.up) { 
        //    toReturn.y -= LENGTH;
        //} else if (currentDirection == Vector3.down) {
        //    toReturn.y += LENGTH;
        //} else if(currentDirection == Vector3.left ){
        //    toReturn.x += LENGTH;
        //} else if (currentDirection == Vector3.right) {
        //    toReturn.x -= LENGTH;
        //}
        return toReturn;
    }

    private void enqueue(ChangePoint changePoint)
    {
        foreach(GameObject snakePart in tail)
        {
            snakePart.GetComponent<SnakePart>().AddChangePoint(changePoint);
        }
        player.GetComponent<SnakePart>().currentChangePoint = changePoint;
    }

    private void Move()
    {
        foreach (GameObject snakePart in tail)
        {
            snakePart.GetComponent<SnakePart>().Move();
        }
        player.GetComponent<SnakePart>().Move();
    }

    private bool isMoving()
    {
        return player.GetComponent<SnakePart>().currentDirection != Vector3.zero;
    }

    private Vector3 calculateNewRotation(Vector3 newDirection)
    {
        if (currentDirection == Vector3.up) {
            if (newDirection == Vector3.left) {
                return player.transform.eulerAngles + SnakePart.counterClockwise;
            } else if (newDirection == Vector3.right) {
                return player.transform.eulerAngles + SnakePart.clockwise;
            }
        }
        else if (currentDirection == Vector3.down)
        {
            if (newDirection == Vector3.left) {
                return player.transform.eulerAngles + SnakePart.clockwise;
            } else if (newDirection == Vector3.right) {
                return player.transform.eulerAngles + SnakePart.counterClockwise;
            }
        }
        else if (currentDirection == Vector3.left)
        {
            if (newDirection == Vector3.up)
            {
                return player.transform.eulerAngles + SnakePart.clockwise;
            }
            else if (newDirection == Vector3.down)
            {
                return player.transform.eulerAngles + SnakePart.counterClockwise;
            }
        }
        else if (currentDirection == Vector3.right)
        {
            if (newDirection == Vector3.up)
            {
                return player.transform.eulerAngles + SnakePart.counterClockwise;
            }
            else if (newDirection == Vector3.down)
            {
                return player.transform.eulerAngles + SnakePart.clockwise;
            }
        }

        return player.transform.localEulerAngles;
    }

}
