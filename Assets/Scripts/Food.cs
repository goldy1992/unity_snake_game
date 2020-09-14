using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public GameHandler gameHandler;

    public SnakePart snakeHead;


    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomPositon();
    }

    public void GenerateRandomPositon()
    {
        Vector3 randomVector = new Vector3(Random.Range(-49, 49), Random.Range(-49, 49));
        //   Debug.Log("transforming random, x: " + randomVector.x + ", y: " + randomVector.y);
        Debug.Log("food created");
        transform.position = randomVector;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.tag.Equals("player")) {
            Debug.Log("collision happened with player");
            gameHandler.FoodEaten();
        }
    }

}
