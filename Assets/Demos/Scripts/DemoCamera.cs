﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DemoCamera : MonoBehaviour
{
    private float defaultRotationY = -135f;

    private float defaultRotationX = 30f;
    private float defaultPositionX = 16f;
    private float defaultPositionZ = 16f;

    private float defaultPositionXZ = Mathf.Sqrt(2) * 16f;
    private float defaultPositionY = 5f;
    private float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    private float orthoZoomSpeed = 0.1f;        // The rate of change of the orthographic size in orthographic mode.

    private float MoveSpeed = 0.1f;
    private float RotateSpeed = 1f;
    private float orthographicMin = 2.0f;
    private float orthographicMax = 10.0f;

    private Camera camera;

    public delegate void FloatDelegate(float value);
    public FloatDelegate OnCameraRotate;

    // record
    private float rotateHorizontal = 0;

    private float rotateVertical = 0;
    private float moveVertical = 0;
    private float moveHorizontal = 0;

#if UNITY_EDITOR
    private Vector3 lastPosition;
#endif

#if UNITY_EDITOR
    void OnGUI()
    {
        if (GUI.RepeatButton(new Rect(0, 0, 50, 20), "放大"))
        {
            Zoom(1f);
        }

        if (GUI.RepeatButton(new Rect(60, 0, 50, 20), "缩小"))
        {
            Zoom(-1f);
        }
        if (GUI.RepeatButton(new Rect(30, 30, 50, 20), "上转"))
        {
            Rotate(new Vector2(0, 1));
        }

        if (GUI.RepeatButton(new Rect(30, 90, 50, 20), "下转"))
        {
            Rotate(new Vector2(0, -1));
        }

        if (GUI.RepeatButton(new Rect(0, 60, 50, 20), "左转"))
        {
            Rotate(new Vector2(-1, 0));
        }

        if (GUI.RepeatButton(new Rect(60, 60, 50, 20), "右转"))
        {
            Rotate(new Vector2(1, 0));
        }

        if (GUI.RepeatButton(new Rect(30, 120, 50, 20), "↑"))
        {
            Move(new Vector2(0, 1));
        }

        if (GUI.RepeatButton(new Rect(30, 180, 50, 20), "↓"))
        {
            Move(new Vector2(0, -1));
        }

        if (GUI.RepeatButton(new Rect(0, 150, 50, 20), "←"))
        {
            Move(new Vector2(-1, 0));
        }

        if (GUI.RepeatButton(new Rect(60, 150, 50, 20), "→"))
        {
            Move(new Vector2(1, 0));
        }

    }
#endif

    public void Start()
    {
        camera = GetComponent<Camera>();
        SetCameraTransform();
    }

    public void Zoom(float deltaMagnitude)
    {
        if (camera.orthographic)
        {
            camera.orthographicSize -= deltaMagnitude * orthoZoomSpeed;
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, orthographicMin, orthographicMax);
        }
        else
        {
            camera.fieldOfView += deltaMagnitude * perspectiveZoomSpeed;
            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
        }
    }

    public void Rotate(Vector2 deltaDegree)
    {

        rotateHorizontal += deltaDegree.x * RotateSpeed;
        rotateVertical += deltaDegree.y * RotateSpeed;

        rotateVertical = Mathf.Clamp(rotateVertical, -30f, 50f);

        SetCameraTransform();
    }

    public void Move(Vector2 deltaVec)
    {
        moveHorizontal += deltaVec.x * MoveSpeed;
        moveVertical += deltaVec.y * MoveSpeed;

        SetCameraTransform();
    }
    private void SetCameraTransform()
    {
        Vector3 cameraPosition = camera.transform.position;
        Vector3 cameraRotation = camera.transform.eulerAngles;

        cameraRotation.x = defaultRotationX + rotateVertical;
        cameraRotation.y = defaultRotationY - rotateHorizontal;

        float rotateHorizontalRad = rotateHorizontal * Mathf.Deg2Rad;
        float rotateVerticalRad = (defaultRotationX + rotateVertical) * Mathf.Deg2Rad;
        float moveDegree = (defaultRotationY + rotateHorizontal + 180);
        float moveRad = moveDegree * Mathf.Deg2Rad;

        cameraPosition.x = defaultPositionX * Mathf.Cos(rotateHorizontalRad) - defaultPositionZ * Mathf.Sin(rotateHorizontalRad);
        cameraPosition.z = defaultPositionX * Mathf.Sin(rotateHorizontalRad) + defaultPositionZ * Mathf.Cos(rotateHorizontalRad);
        cameraPosition.x += Mathf.Sin(moveRad) * moveHorizontal;
        cameraPosition.z += -Mathf.Cos(moveRad) * moveHorizontal;

        cameraPosition.y = defaultPositionXZ * Mathf.Tan(rotateVerticalRad) + defaultPositionY;
        cameraPosition.y -= moveVertical;


        camera.transform.position = cameraPosition;
        camera.transform.eulerAngles = cameraRotation;

        if (OnCameraRotate != null)
        {
            OnCameraRotate(moveDegree);
        }
    }

}
