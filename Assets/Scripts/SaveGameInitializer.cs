using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameInitializer : MonoBehaviour
{
    
    public SaveGame saveGame;
    
    void Awake()
    {
        if (!SaveGame.SG)
            SaveGame.SG = saveGame;
        Destroy(gameObject);
    }
}
