using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4 : MonoBehaviour
{
    public float radius = 5f;
    public float power = 500;

    [SerializeField] ParticleSystem explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerC4()
    {
        Vector3 position = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(position, radius);
        foreach (Collider thing in hitColliders)
        {
            if (thing.GetComponent<Rigidbody>())
            {
                Rigidbody rb = thing.GetComponent<Rigidbody>();
                rb.AddExplosionForce(power, position, radius, 2.0f, ForceMode.Impulse);
                
            }
        }
        explosion.Play();
    }
}
