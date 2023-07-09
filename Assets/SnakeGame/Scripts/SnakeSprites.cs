using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSprites : ScriptableObject
{
    public Sprite head;
    public float headRotationOffset;

    public Sprite body;
    public float bodyRotationOffset;

    public Sprite tail;
    public float tailRotationOffset;
}
