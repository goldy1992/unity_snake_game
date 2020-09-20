using System;
using System.Collections;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private static float SPEED = 2f;

    private static int LENGTH = 1;

    private bool chooseGreen = true;


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
        Vector3 currentDirection = this.player.GetComponent<SnakePart>().currentDirection;
        Vector3 newVector;
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentDirection != Vector3.down) {
            newVector = Vector3.up;
        } else if (Input.GetKeyDown(KeyCode.DownArrow) && currentDirection != Vector3.up) {
            newVector = Vector3.down;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentDirection != Vector3.right) {
            newVector = Vector3.left;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && currentDirection != Vector3.left) {
            newVector = Vector3.right;
        } else {
            newVector = Vector3.back;
        }

        if (newVector != Vector3.back) {
            Debug.Log("drection change happened at: " + player.transform.localPosition);
            enqueue(new ChangePoint(newVector, player.transform.localPosition));
        }

        Move();
    }

    public void FoodEaten()
    {
        Debug.Log("food eaten");
        food.GetComponent<Food>().GenerateRandomPositon();

        GameObject currentTail;
        if (tail.Count > 0)
        {
            currentTail = ((GameObject)tail[tail.Count - 1]);
        } else {
            currentTail = player;
        }


        GameObject newBody = Instantiate(snakeBodyPrefab, calculateNewPosition(currentTail), Quaternion.identity);
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
        String msg = "old pos: " + fromSnakePart.transform.localPosition;
        Vector3 toReturn = fromSnakePart.transform.localPosition;
        SnakePart snakePart = fromSnakePart.GetComponent<SnakePart>();
        Vector3 currentDirection = snakePart.currentDirection;

        if (currentDirection == Vector3.up) { 
            toReturn.y -= LENGTH;
        } else if (currentDirection == Vector3.down) {
            toReturn.y += LENGTH;
        } else if(currentDirection == Vector3.left ){
            toReturn.x += LENGTH;
        } else if (currentDirection == Vector3.right) {
            toReturn.x -= LENGTH;
        }
        msg += ", new POS: " + toReturn;
        Debug.Log(msg);
        return toReturn;
    }

    private void enqueue(ChangePoint changePoint)
    {
        foreach(GameObject snakePart in tail)
        {
            snakePart.GetComponent<SnakePart>().AddChangePoint(changePoint);
        }

        player.GetComponent<SnakePart>().currentDirection = changePoint.direction;
    }

    private void Move()
    {
        foreach (GameObject snakePart in tail)
        {
            snakePart.GetComponent<SnakePart>().Move();
        }
        player.GetComponent<SnakePart>().Move();
    }

}
