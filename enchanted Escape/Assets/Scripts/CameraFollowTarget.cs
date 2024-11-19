using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public GameObject followTarget;
    private Camera camera;

    void Awake()
    {
        // get the Camera component when the game runs
        // note if this script is not on the same GameObject as the Camera component, there will be an error
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // get the X and Y position of the follow target and the Z position of the camera.
        // if the camera Z position is zero or position, the screen will be blank, so we are setting it to -10 (any negative number will work)
        Vector3 newPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, -10);

        // set camera position to new position
        camera.transform.position = newPosition;
    }
}
