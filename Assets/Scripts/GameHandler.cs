using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    GameObject food;

    GameObject player;

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
        
    }

    public void FoodEaten()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("body");

    }


}
