using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4 : MonoBehaviour
{
    public float radius = 5f; // The radius from the center of the explosion that objects are hit by the blast.
    public float power = 500; // The strength of the initial detonation.

    [SerializeField] ParticleSystem explosion; // Reference to the explosion particle system.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to detonate the C4.
    public void TriggerC4()
    {
        // First store the position of the C4.
        Vector3 position = transform.position;
        // Generate a sphere colider at the position of the c4 with a radius equal to the blast radius
        // Store all objects hit by this collider in an array.
        Collider[] hitColliders = Physics.OverlapSphere(position, radius);
        // Cycle through each object in the array of hit things.
        foreach (Collider thing in hitColliders)
        {
            // First check to make sure the object hit has a RigidBody component.
            if (thing.GetComponent<Rigidbody>())
            {
                // Grab a reference to the object's RigidBody component.
                Rigidbody rb = thing.GetComponent<Rigidbody>();
                // Apply an explosive force to the object using parameters from the C4 (position, radius, and power).
                // Make sure the ForceMode is set to Impulse so that the entire force is applied in one frame.
                rb.AddExplosionForce(power, position, radius, 2.0f, ForceMode.Impulse);
                
            }
        }
        // Play the explosion particle effect.
        explosion.Play();
    }
}
