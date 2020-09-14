
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class SnakePart : MonoBehaviour
{

    Vector3 destination;

    public GameHandler gameHandler;




    private Vector2Int gridPosition;




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



    }

    public void Move(Vector3 direction) 
    {
        transform.Translate(direction * Time.deltaTime * 10);
    }

}
