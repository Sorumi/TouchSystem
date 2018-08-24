using UnityEngine;
using System.Collections;


public class Demo1 : MonoBehaviour
{
    private DemoCamera demoCamera;

    void Start()
    {
        demoCamera = FindObjectOfType(typeof(DemoCamera)) as DemoCamera;

        TapRecognizer tapRecognizer = new TapRecognizer();
        tapRecognizer.zIndex = 1;
        tapRecognizer.gestureRecognizedEvent += r =>
        {
            Debug.Log("Tap: " + r);
        };

        PanRecognizer panRecognizer = new PanRecognizer();
        panRecognizer.zIndex = 2;
        panRecognizer.gestureBeginEvent += r =>
        {
            Debug.Log("Pan Begin -----: " + r);
        };

        panRecognizer.gestureRecognizedEvent += r =>
        {
            Debug.Log("Pan : " + r);
            demoCamera.Rotate(-r.deltaPosition.x * 0.1f);
        };

        panRecognizer.gestureEndEvent += r =>
        {
            Debug.Log("Pan End -----: " + r);
        };

        PanRecognizer panTwoRecognizer = new PanRecognizer(2);
        panTwoRecognizer.zIndex = 3;
        panTwoRecognizer.gestureBeginEvent += r =>
        {
            Debug.Log("Pan Two Begin -----: " + r);
        };

        panTwoRecognizer.gestureRecognizedEvent += r =>
        {
            Debug.Log("Pan Two : " + r);
            demoCamera.Move(r.deltaPosition * 0.05f);
        };

        panTwoRecognizer.gestureEndEvent += r =>
        {
            Debug.Log("Pan Two End -----: " + r);
        };

        PinchRecognizer pinchRecognizer = new PinchRecognizer();
        pinchRecognizer.zIndex = 4;
        pinchRecognizer.gestureBeginEvent += r =>
        {
            Debug.Log("Pinch Begin -----: " + r);
        };

        pinchRecognizer.gestureRecognizedEvent += r =>
        {
            Debug.Log("Pinch : " + r);
            demoCamera.Zoom(r.deltaDistance * 0.05f);
        };

        pinchRecognizer.gestureEndEvent += r =>
        {
            Debug.Log("Pinch End -----: " + r);
        };

        TouchSystem.addRecognizer(tapRecognizer);
        TouchSystem.addRecognizer(panRecognizer);
        TouchSystem.addRecognizer(panTwoRecognizer);
        TouchSystem.addRecognizer(pinchRecognizer);

    }
}