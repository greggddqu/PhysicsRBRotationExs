using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEquation : MonoBehaviour
{
    

    public Vector3 initialPos;
    public Vector3 initialVel;
    public Vector3 acceleration;
    public Vector3 posNow;
    public float timeNow;

    // Start is called before the first frame update
    void Start()
    {
        //Set initial values of position and velocity vectors
        //Set initial time
        //Set acceleration vector
        transform.position = new Vector3(0, 0, 0);
        initialPos = transform.position;
        initialVel = new Vector3(0, 1, 1) * 10.0f; 
        acceleration = -0.5f * 9.8f * Vector3.up;
        timeNow = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        //calculate the trajectory of a projectile with default mass 1
        posNow.x = initialPos.x + initialVel.x * timeNow;
        posNow.y = initialPos.y + (initialVel.y + acceleration.y * timeNow) * timeNow;
        posNow.z = initialPos.z + initialVel.z * timeNow;
        //update the transform position with the calculated position
        transform.position = posNow; 
        //Debug.Log(timeNow + "  " + Time.time);
        timeNow += Time.deltaTime;

    }
}
