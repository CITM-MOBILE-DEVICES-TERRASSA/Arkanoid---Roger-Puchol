using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject
{
    static int NextLevel = 1;
    static List<Level> Levels = new List<Level>();
    public int level = NextLevel;
    // Array has to be multiple of 13 (screen width in bricks)
    public BrickColor[] layout;

    public void Awake()
    {
        SortLevels();
    }


    [ContextMenu("Load Levels")]
    private void SortLevels()
    {
        Levels.Clear();
        var list = Resources.FindObjectsOfTypeAll<Level>();
        Levels.AddRange(list);

        Levels.Sort((l1, l2) => l1.level-l2.level);

        var strings = Levels.Select(l => l.level).ToList().ConvertAll(i => i.ToString());
        string message = "Levels list: " + string.Join(" ", strings);
            
        Debug.Log(message);
        NextLevel = Levels.Count;
    }
}
