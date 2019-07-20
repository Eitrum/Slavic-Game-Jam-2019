using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VineSettings : ScriptableObject
{
    public float startScale = 0f;
    public float endScaleMultiplier = 2f;
    public float minEndScale = 3f;
    public float growDuration = 1f;
}
