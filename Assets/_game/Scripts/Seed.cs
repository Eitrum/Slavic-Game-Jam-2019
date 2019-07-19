using UnityEngine;

public sealed class Seed : MonoBehaviour
{
    public Rigidbody rb;

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
