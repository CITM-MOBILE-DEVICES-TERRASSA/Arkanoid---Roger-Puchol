using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupState
{
    Normal = 0,
    Wide,
    Magnetic,
    Small
}

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    
    public SpriteStorage sprites;
    
    public GameObject brickPrefab;
    
    public Level level;

    public GameObject gridStart;

    [Min(0.1f)]
    public Vector2 brickSize;
    
    // Start is called before the first frame update
    void Start()
    {
        //Singleton
        if (GM != null)
        {
            Destroy(this);
            return;
        }

        if (Camera.main != null) 
            Camera.main.aspect = 200.0f / 171.0f;

        int columns = 13;
        int rows = level.layout.Length / columns;
        if (rows < 1) rows = 1;
        
        // Spawn the block grid
        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < columns; i++)
            {
                BrickColor color = level.layout[j * columns + i];
                if (color == BrickColor.None) continue;
                
                var pos = gridStart.transform.position;
                pos.x += i * brickSize.x;
                pos.y -= j * brickSize.y;
                var instance = Instantiate(brickPrefab, pos, Quaternion.identity)?.GetComponent<BrickScript>();
                if (instance)
                {
                    instance.brickColor = color;
                    var anim = instance.GetComponent<Animator>();
                    if (anim)
                    {
                        anim.runtimeAnimatorController = sprites.brickAnimations[(int)color];
                    }
                }
                
            }
        }

        GM = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
