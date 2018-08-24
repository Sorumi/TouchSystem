﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DemoCamera : MonoBehaviour
{
    private float defaultRotationY = -135f;
    private float defaultPositionX = 16f;
    private float defaultPositionZ = 16f;
    private float defaultPositionY = 16f;
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
    private float rotateDegree = 0;
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

        if (GUI.RepeatButton(new Rect(0, 30, 50, 20), "左转"))
        {
            Rotate(-1f);
        }

        if (GUI.RepeatButton(new Rect(60, 30, 50, 20), "右转"))
        {
            Rotate(1f);
        }

        if (GUI.RepeatButton(new Rect(30, 60, 50, 20), "↑"))
        {
            Move(new Vector2(0, 1));
        }

        if (GUI.RepeatButton(new Rect(30, 120, 50, 20), "↓"))
        {
            Move(new Vector2(0, -1));
        }

        if (GUI.RepeatButton(new Rect(0, 90, 50, 20), "←"))
        {
            Move(new Vector2(-1, 0));
        }

        if (GUI.RepeatButton(new Rect(60, 90, 50, 20), "→"))
        {
            Move(new Vector2(1, 0));
        }

    }
#endif

    public void Start()
    {
        camera = GetComponent<Camera>();
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

    public void Rotate(float deltaDegree)
    {

        rotateDegree += deltaDegree * RotateSpeed;

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

        cameraRotation.y = defaultRotationY - rotateDegree;

        float rotateRad = rotateDegree * Mathf.Deg2Rad;
        float moveDegree = (defaultRotationY + rotateDegree + 180);
        float moveRad = moveDegree * Mathf.Deg2Rad;

        cameraPosition.x = defaultPositionX * Mathf.Cos(rotateRad) - defaultPositionZ * Mathf.Sin(rotateRad);
        cameraPosition.z = defaultPositionX * Mathf.Sin(rotateRad) + defaultPositionZ * Mathf.Cos(rotateRad);
        cameraPosition.x += Mathf.Sin(moveRad) * moveHorizontal;
        cameraPosition.z += -Mathf.Cos(moveRad) * moveHorizontal;

        cameraPosition.y = defaultPositionY - moveVertical;

        camera.transform.position = cameraPosition;
        camera.transform.eulerAngles = cameraRotation;

        if (OnCameraRotate != null)
        {
            OnCameraRotate(moveDegree);
        }
    }

}
