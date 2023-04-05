using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [Header("Target")]
    public Transform target = null;

    [Header("Speed")]
    public float speedFactor = 1.0f;

    [Header("Behaviour")]
    public bool keepXPosition = false;
    public bool keepYPosition = false;
    public bool keepZPosition = false;

    // Update is called once per frame
    void Update()
    {
        // Determine distance to accelerate if we're far (or instant travel of camera)
        float distance = (this.transform.position - this.target.position).magnitude;

        // Calculate new Position
        Vector3 newPosition = Vector3.Lerp(this.transform.position, this.target.position, this.speedFactor * distance * Time.deltaTime);

        // Fix values
        if (this.keepXPosition == true)
            newPosition.x = this.transform.position.x;
        if (this.keepYPosition == true)
            newPosition.y = this.transform.position.y;
        if (this.keepZPosition == true)
            newPosition.z = this.transform.position.z;

        // Set new position
        this.transform.position = newPosition;
    }
}
