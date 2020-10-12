using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    private Vector3 zeroVector = new Vector3(0, 0);

    public GameHandler gameHandler;



    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomPositon();
    }

    public void GenerateRandomPositon()
    {
    //    Debug.Log("Parent position: " + transform.parent.localPosition);
        Vector3 randomVector = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));
        //   Debug.Log("transforming random, x: " + randomVector.x + ", y: " + randomVector.y);
     //   Debug.Log("food created: " + randomVector.x + ", " + randomVector.y);
        //transform.position = zeroVector;
        transform.localPosition = randomVector;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        if (collision.attachedRigidbody.tag.Equals("player")) {
            Debug.Log("collision happened with player");
            gameHandler.FoodEaten();
        }
    }

}
