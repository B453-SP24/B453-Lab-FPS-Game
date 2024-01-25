using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    // Define a method that must be implemented on Classes that implement this interface.
    // The method should be called when the player picks something up, and it will take a PlayerController as an argument (so it knows who is picking it up).
    public void PickUp(PlayerController player);
}
