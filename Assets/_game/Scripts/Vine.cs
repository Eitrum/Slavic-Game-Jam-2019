using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    internal float spawnTimeStamp;
    internal float endScale;

    public ParticleSystem[] particleSystemsToPause;
    private float timer = 0f;

    private void Update()
    {
        if (timer < 1f)
        {
            timer += Time.deltaTime;

        }
        else
        {
            foreach (var ps in particleSystemsToPause)
            {
                ps.Pause();
            }
        }
    }
}
