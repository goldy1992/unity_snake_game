
using System;
using System.Collections.Generic;
using UnityEngine;

public class SnakePart : MonoBehaviour
{
    public Queue<ChangePoint> changePoints = new Queue<ChangePoint>();

    public ChangePoint currentChangePoint = null;// new ChangePoint(Vector3.zero, Vector3.zero);

    public Vector3 currentDirection;

    private static float SPEED = 4f;

    private static float rotationTime = 0.05f;

    private bool isRotating = false;

     public static Vector3 clockwise = new Vector3(0, 0f, -90f);
     public static Vector3 counterClockwise = new Vector3(0f, 0f, 90f);


    Vector3 orig = new Vector3(0f, 0f, 0f);

    // The time at which the animation started.
    private float startTime;

    private Vector3 targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
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
            if (currentChangePoint.changePosition == transform.position)
            {
                isRotating = true;
                startTime = Time.time;
                orig = transform.localEulerAngles;
                targetRotation = currentChangePoint.direction;

            }
        }

        if (isRotating)
        {
            float fracComplete = (Time.time - startTime) / rotationTime;
            transform.localEulerAngles = Vector3.Lerp(orig, targetRotation, fracComplete);
            if (fracComplete == 1.0f)
            {
                isRotating = false;
                currentChangePoint = null;
            }
        }

        //    transform.position = Vector3.Slerp(currentDirection, Vector3.down, fracComplete);
        transform.Translate(currentDirection * Time.deltaTime * SPEED);
    }

    private void setCurrentChangePoint()
    {
        if (currentChangePoint is null)
        {
            if (changePoints.Count > 0)
            {
                currentChangePoint = changePoints.Dequeue();
            }
        }
    }

    public void setQueue(Queue<ChangePoint> queue)
    {
        ChangePoint[] changePointsArray = new ChangePoint[queue.Count];
        queue.CopyTo(changePointsArray, 0);
        foreach(ChangePoint cp in changePointsArray)
        {
            this.changePoints.Enqueue(cp);
        }
        setCurrentChangePoint();

    }

    public void AddChangePoint(ChangePoint changePoint)
    {
  
        this.changePoints.Enqueue(changePoint);
        Debug.Log(this + "queue count: " + changePoints.Count);
    }

    public Queue<ChangePoint> getChangePointQueue()
    {
        return changePoints;
    }

    private void applyChangePoint(ChangePoint changePoint)
    {

        this.transform.localPosition = currentChangePoint.changePosition;
        this.currentChangePoint = null;
        setCurrentChangePoint();
    }

    public Vector3 getFacingDirection()
    {
        if(isRotating)
        {
            return targetRotation;
        } else
        {
            return transform.localEulerAngles;
        }
    }

}
