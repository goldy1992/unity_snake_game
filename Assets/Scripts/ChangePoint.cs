using UnityEngine;

public class ChangePoint
{

    public Vector3 changePosition;

    public Vector3 direction;
    public ChangePoint(Vector3 direction, Vector3 changePosition)
    {
        this.direction = direction;
        this.changePosition = changePosition;
    }


}
