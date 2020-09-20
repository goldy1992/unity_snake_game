
using System.Collections.Generic;
using UnityEngine;

public class SnakePart : MonoBehaviour
{
    private Queue<ChangePoint> changePoints = new Queue<ChangePoint>();

    public ChangePoint currentChangePoint = null;// new ChangePoint(Vector3.zero, Vector3.zero);

    public Vector3 currentDirection;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hit controls start");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move()
    {
        setCurrentChangePoint();

        if (currentChangePoint != null)
        {
            if (Vector3.up == currentDirection)
            {
                if (transform.localPosition.y >= currentChangePoint.changePosition.y)
                {
                    this.currentDirection = this.currentChangePoint.direction;
                    this.transform.localPosition = currentChangePoint.changePosition;
                    this.currentChangePoint = null;
                    setCurrentChangePoint();
                }
            }
            else if (Vector3.down == currentDirection)
            {
                if (transform.localPosition.y <= currentChangePoint.changePosition.y)
                {
                    this.currentDirection = this.currentChangePoint.direction;
                    this.transform.localPosition = currentChangePoint.changePosition;
                    this.currentChangePoint = null;
                    setCurrentChangePoint();
                }
            }
            else if (Vector3.left == currentDirection)
            {
                if (transform.localPosition.x <= currentChangePoint.changePosition.x)
                {
                    this.currentDirection = this.currentChangePoint.direction;
                    this.transform.localPosition = currentChangePoint.changePosition;
                    this.currentChangePoint = null;
                    setCurrentChangePoint();
                }
            }
            else if (Vector3.right == currentDirection)
            {
                if (transform.localPosition.x >= currentChangePoint.changePosition.x)
                {
                    this.currentDirection = this.currentChangePoint.direction;
                    this.transform.localPosition = currentChangePoint.changePosition;
                    this.currentChangePoint = null;
                    setCurrentChangePoint();
                }
            }

        }
        transform.Translate(currentDirection * Time.deltaTime * 10);
    }

    private void setCurrentChangePoint()
    {
        if (currentChangePoint is null)
        {
            if (changePoints.Count > 0)
            {
                currentChangePoint = changePoints.Dequeue();
            //    currentDirection = currentChangePoint.direction;
            }
        }
    }

    public void setQueue(Queue<ChangePoint> queue)
    {
        Debug.Log("queue count: " + queue.Count);
        ChangePoint[] changePointsArray = new ChangePoint[queue.Count];
        queue.CopyTo(changePointsArray, 0);
        Debug.Log("Changepointarr: " + changePointsArray);
        foreach(ChangePoint cp in changePointsArray)
        {
            this.changePoints.Enqueue(cp);
        }
        setCurrentChangePoint();

    }

    public void AddChangePoint(ChangePoint changePoint)
    {
        if (currentChangePoint == null)
        {
            this.currentChangePoint = changePoint;
        }
        else
        {
            this.changePoints.Enqueue(changePoint);
        }
    }

    public Queue<ChangePoint> getChangePointQueue()
    {
        return changePoints;
    }

}
