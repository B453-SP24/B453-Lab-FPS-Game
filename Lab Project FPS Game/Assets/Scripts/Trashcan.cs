using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour, IInteractable
{
    // Reference to the particle system attached to the trashcan.
    [SerializeField] GameObject trashParticles;

    // Implementation of the Interact() method from the IInteractable interface.
    public void Interact()
    {
        // Check to make sure the particle system isn't playing.
        if (!trashParticles.GetComponent<ParticleSystem>().isPlaying)
        {
            // Start playing the particle effect.
            trashParticles.GetComponent<ParticleSystem>().Play();
        }
    }
}