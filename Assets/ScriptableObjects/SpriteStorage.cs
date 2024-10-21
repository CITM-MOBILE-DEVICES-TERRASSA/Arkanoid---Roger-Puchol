using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteStorage", menuName = "ScriptableObjects/SpriteStorage")]
public class SpriteStorage : ScriptableObject
{
    public RuntimeAnimatorController[] brickAnimations;
}
