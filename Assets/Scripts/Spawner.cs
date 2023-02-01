using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //This is a Singleton of the particle Spawner. there is only one instance 
    // of this Spawner, so we can store it in a static variable named s.
    static public Spawner S;
    static public List<ParticleUnity> ParticleUnitys;

    // These fields allow you to adjust the spawning behavior of the spheres
    [Header("Set in Inspector: Spawning")]
    public GameObject particleUnityPrefab;
    public Transform particleAnchor;
    public int numParticles = 100;
    public float spawnDelay = 0.1f;

    //add variables here that spawned particles will inherit    

    void Awake()
    {
        //Set the Singleton S to be this instance of particle spawner
        S = this;
        //Start instantiation of the particles
        ParticleUnitys = new List<ParticleUnity>();//the list holding the particles
        InstantiateParticleUnitys();
    }

    //a method to spawn the particles
    public void InstantiateParticleUnitys()
    {
        GameObject go = Instantiate(particleUnityPrefab);
        ParticleUnity b = go.GetComponent<ParticleUnity>();
        b.transform.SetParent(particleAnchor);
        ParticleUnitys.Add(b);
        if (ParticleUnitys.Count < numParticles)
        {
            Invoke("InstantiateParticleUnitys", spawnDelay);
        }
    }
}
