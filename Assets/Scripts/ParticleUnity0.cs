    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleUnity0 : MonoBehaviour
{
    public Vector3 initialVel;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start() 
    {
        //set the initial position
        transform.position = new Vector3(-1.5f, 0, 0);
        //set the initial velocity of the game object this script is attached to
        initialVel = new Vector3(0,1,1) * 10.0f;
        //get the rigid body of the game object this script is attached to
        rb = GetComponent<Rigidbody>(); 
        //give the initial velocity to the rigid body
        rb.velocity = initialVel;
        Debug.Log(Physics.gravity);//type out the value of acceleration of gravity used by Unity
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //since the rigid body component is attached, the physics engine is 
        //integrating the position and velocity automatically and updating it
        //using the default gravitational force field
    }
}
