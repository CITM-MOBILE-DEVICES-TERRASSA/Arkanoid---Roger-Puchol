using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BrickColor
{
    None = -1,
    Orange,
    White,
    LightBlue,
    Green,
    Red,
    Blue,
    Magenta,
    Yellow,
    Gray,
    Gold,
    COLOR_COUNT
};

public class BrickScript : MonoBehaviour
{
    [SerializeField] private GameObject[] powerupPrefabs;
    [SerializeField] [Range(0,100)] private int powerupChance;
    
    private static readonly int Hit = Animator.StringToHash("Hit");
    public BrickColor brickColor;

    // < 0 hp = indestructible
    [SerializeField] private int hp;

    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        switch (brickColor)
        {
            case BrickColor.None:
            {
                Destroy(gameObject);
                return;
            }
            case BrickColor.Gray:
            {
                hp = 5;
                break;
            }
            case BrickColor.Gold:
            {
                hp = -1;
                break;
            }
            default:
            {
                hp = 1;
                break;
            }
        }
        anim = GetComponent<Animator>();
        TriggerAnimation();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.GetComponent<Ball>()) return;

        if (--hp == 0)
        {
            GameManager.GM.AddScore(brickColor == BrickColor.Gray? 50:10);
            
            if (powerupPrefabs.Length > 0 && Random.Range(0,100) < powerupChance)
                Instantiate(powerupPrefabs[Random.Range(0,powerupPrefabs.Length)], transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
        else
        {
            CancelInvoke();
            anim?.SetTrigger(Hit);
            TriggerAnimation();
        }

        if (hp < 0) hp = -1; // Prevent integer underflow on unbreakable blocks (still unlikely to happen)

    }
    
    void TriggerAnimation()
    {
        anim?.SetTrigger(Hit);
        Invoke(nameof(TriggerAnimation), Random.Range(1.5f, 5.0f));
    }

    void OnDestroy()
    {
        if (GameManager.GM)
            GameManager.GM.DestroyBrick(this);
    }
}
