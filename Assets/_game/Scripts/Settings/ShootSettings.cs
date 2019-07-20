using UnityEngine;

[CreateAssetMenu]
public class ShootSettings : ScriptableObject
{
    public float shootInterval = 0.5f;

    public AudioClip[] playerShootSFX = { null, null, null, null };
    

}
