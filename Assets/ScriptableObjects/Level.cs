using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject
{
    // Array has to be multiple of 13 (screen width in bricks)
    public BrickColor[] layout;
}
