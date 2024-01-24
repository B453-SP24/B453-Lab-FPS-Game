using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour, IPickupable
{
    [SerializeField] int ammo;


    public void PickUp(PlayerController player)
    {
        player.PickupAmmo(ammo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickUp(other.gameObject.GetComponent<PlayerController>());
            Destroy(gameObject);
        }
    }
}
