using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour, IPickupable
{
    // How much ammo is contained in this ammo pickup.
    [SerializeField] int ammo;


    // Implementing the PickUp() method from the IPickupable interface.
    public void PickUp(PlayerController player)
    {
        // Call the PickupAmmo method from the PlayerController who attempted to pick up this ammo, and pass in how much ammo is in this pickup.
        player.PickupAmmo(ammo);
    }

    // Checking to see if the player enters the collider of this object.
    // Note that we are using OnTriggerEnter because we don't want the ammo pickup to be a physical object that can block the player from moving.
    // Using a trigger instead of a solid collider ensures collisions can be detected without physically blocking things.
    private void OnTriggerEnter(Collider other)
    {
        // Make sure the thing that touched it is in fact a player.
        if (other.gameObject.tag == "Player")
        {
            // Call the PickUp method on the player that walked over the ammo.
            PickUp(other.gameObject.GetComponent<PlayerController>());
            // Destroy this ammo pickup.
            Destroy(gameObject);
        }
    }
}
