using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleIntegrator : MonoBehaviour
{


    public Vector3 initialPos;
    public Vector3 initialVel;
    public Vector3 acceleration;

    public Vector3 newPos;
    public Vector3 newVel;

    // Start is called before the first frame update
    void Start()
    {
        //Set initial values of position and velocity vectors
        //Set acceleration vector
        transform.position = new Vector3(1.5f, 0, 0);
        initialPos = transform.position;
        initialVel = new Vector3(0, 1, 1) * 10.0f;
        //acceleration of gravity for force = mass * acceleration
        acceleration = - 9.8f * Vector3.up;

        //Initialize the iterator variables
        newVel = initialVel;
        newPos = initialPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //home made physics iterator for a force
        newVel += acceleration * Time.deltaTime;// update the velocity with a force (mass 1)
        newPos += newVel * Time.deltaTime;//update position with new velocity
        transform.position = newPos; //give new position to the transform
    }
}
