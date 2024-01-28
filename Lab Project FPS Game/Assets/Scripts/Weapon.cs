using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Details")]
    [Tooltip("Name of the weapon.")]
    [SerializeField] protected string gunName;
    [Tooltip("Current bullets loaded into the weapon.")]
    [SerializeField] protected int currentBullets;
    [Tooltip("The maximum amount of bullets this weapon can hold at once.")]
    [SerializeField] protected int maxBullets;

    [Header("References")]
    [Tooltip("Decal to use for bullet holes.")]
    [SerializeField] protected GameObject bulletHole;

    // Fire a bullet from the weapon.
    public virtual void Shoot()
    {
        // Check to make sure there are bullets loaded into the gun.
        if (currentBullets > 0)
        {
            // Define a RaycastHit object (this stores data about an object that a raycast hits.)
            RaycastHit hit;

            // Shoot the raycast from the main camera, in the forward direction the camera is facing, store the data in "hit" and the ray distance is infinite.
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
            {
                // Instantiate the decal gameobject for the bullet hole at the location the ray hit and save a reference to it.
                GameObject decal = Instantiate(bulletHole, hit.point, Quaternion.identity);
                // Make the forward (Z) axis of the decal face in the opposite direction of the normal vector of the thing that it hit.
                decal.transform.forward = -hit.normal;
                // Move the decal backwards 1 centimeters away from the surface that it hit.
                decal.transform.Translate(Vector3.back * 0.01f);

                // Save the world position of the decal.
                Vector3 worldPosition = decal.transform.position;
                // Save the world rotation of the decal.
                Quaternion worldRotation = decal.transform.rotation;
                // Make the object the ray hit the parent of the decal. This is necessary to make sure the bullethole stays on the surface of the object if the object moves.
                // NOTE: This will automatically move the bullethole to the incorrect spot because its location and rotation are now relative to the parent.
                decal.transform.SetParent(hit.transform);
                // Move the bullethole back to the right position it was in before.
                decal.transform.position = worldPosition;
                // Move the bullethole back to the right rotation it was in before.
                decal.transform.rotation = worldRotation;

                // Subtract one bullet from the bullets that are currently loaded in the weapon.
                currentBullets--;
            }
        }
    }

    // New modified method to reload the weapon which returns how many rounds were inserted.
    // NOTE: Because it returns an int, we can use it like an int in any calculation.
    // For example: playerAmmo -= Reload(5); is valid. It will be seen as an int and not a method.
    public virtual int Reload(int rounds)
    {
        int roundsThatCouldBeInserted;
        // There are a lot of ways to do this. Let's just have it return how many rounds were added into the gun.

        // First let's make sure the gun isn't already full (meaning full magazine plus 1 in the chamber).
        if (currentBullets > maxBullets)
        {
            // Do nothing
        }
        else
        {

            // Find out how many rounds could be inserted into the gun.
            // First check to see if it's completely empty.
            if (currentBullets == 0)
            {
                // If it's completely empty, changing mags would mean we can insert the maxBullets.
                roundsThatCouldBeInserted = maxBullets - currentBullets;
            }
            // If the gun has at least one bullet in it, when we reload there will still be one in the chamber.
            // So we can insert (max + 1) - current.
            else
            {
                // maxBullets would be 1 more than normal (bullet in chamber) minus the currentBullets.
                roundsThatCouldBeInserted = (maxBullets + 1) - currentBullets;
            }
            // Now that we know how many rounds could be inserted, let's find out how many we will actually insert.
            // If the rounds attempted to be loaded is less than or equal to the roundsThatCouldBeInserted, just use them all.
            if (rounds <= roundsThatCouldBeInserted)
            {
                // Add the new rounds to the gun.
                currentBullets += rounds;
                // We'll just return "rounds" as how many got loaded in since we used them all.
                return rounds;
            }
            // Now we need to take into account the situation if the player is trying to insert more rounds than the gun can accept.
            else
            {
                // Since the gun can accept less rounds than we're passing in, we know we're going to put in all
                // the rounds that could possibly be inserted, so we'll return roundsThatCouldBeInserted.
                // Add the new bullets to the gun.
                currentBullets += roundsThatCouldBeInserted;
                return roundsThatCouldBeInserted;
            }
        }
        // To prevent any possible errors, we return 0 if none of the above logic works correctly.
        return 0;
    }
}