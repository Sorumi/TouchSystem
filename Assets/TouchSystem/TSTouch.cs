using UnityEngine;
using System.Collections;

public class TSTouch
{
    public readonly int fingerId;
    public Vector2 position;
    public Vector2 startPosition;
    public Vector2 lastPosition
    {
        get { return position - deltaPosition; }
    }
    public Vector2 deltaPosition;
    public float deltaTime;

    public TouchPhase phase = TouchPhase.Ended;

    public TSTouch(int id)
    {
        fingerId = id;
    }

    public TSTouch updateByTouch(Touch touch)
    {
        position = touch.position;
        deltaPosition = touch.deltaPosition;
        deltaTime = touch.deltaTime;

        // tapCount = touch.tapCount;

        if (touch.phase == TouchPhase.Began)
        {
            startPosition = position;
        }
        if (touch.phase == TouchPhase.Canceled)
            phase = TouchPhase.Ended;
        else
            phase = touch.phase;

        return this;
    }

    public override string ToString()
    {
        return string.Format("[TSTouch] fingerId: {0}, phase: {1}, position: {2}, startPosition: {3}", fingerId, phase, position, startPosition);
    }
}