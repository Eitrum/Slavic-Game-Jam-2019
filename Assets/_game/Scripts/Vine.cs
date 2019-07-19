using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    List<VineImpact> vineImpactQueue = new List<VineImpact>();

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRb = collision.collider.attachedRigidbody;
        Character character = null;
        if (otherRb != null)
        {
            character = otherRb.GetComponent<Character>();
        }
        if (otherRb != null && character != null)
        {
            vineImpactQueue.Add(new VineImpact 
            {
                character = character 
            });
        }
    }
}

public struct VineImpact
{
    public Character character;
}