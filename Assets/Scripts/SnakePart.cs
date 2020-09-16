
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class SnakePart : MonoBehaviour
{

    Vector3 destination;

    public Direction direction;

    public GameHandler gameHandler;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("hit controls start");
    }

    // Update is called once per frame
    void Update()
    {



    }

    public void Move(Vector3 direction) 
    {
        transform.Translate(direction * Time.deltaTime * 10);
    }

}
