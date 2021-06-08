using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public float smoothingTime = 2.5f;
    public float minX = 5;
    public float maxX = 5;
    public float minY = 5;
    public float maxY = 5;

    private Vector3 finalCameraPosition;
    private Vector2 clampedPosition;
    // Update is called once per frame
    void LateUpdate()
    {
        //Keep the camera position between min and max position bounds
        clampedPosition = new Vector2(Mathf.Clamp(target.position.x, minX, maxX), Mathf.Clamp(target.position.y, minY, maxY));
        finalCameraPosition = new Vector3(clampedPosition.x, clampedPosition.y, transform.position.z);
        
        if (transform.position != finalCameraPosition) {
            transform.position = Vector3.Lerp(transform.position , finalCameraPosition,smoothingTime * Time.deltaTime);
        }
        
    }
}
