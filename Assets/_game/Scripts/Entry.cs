using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    List<Character> characters;

    CharacterMovementSystem characterMovementSystem;
    // Start is called before the first frame update
    void Start()
    {
        characters = new List<Character>(32);
        characterMovementSystem = new CharacterMovementSystem(characters);
    }

    private void FixedUpdate()
    {
        characterMovementSystem.FixedTick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
