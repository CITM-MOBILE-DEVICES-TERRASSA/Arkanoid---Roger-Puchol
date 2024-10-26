using UnityEngine;

public class Ball : MonoBehaviour
{
    public static int ballCount = 0;
    
    public Rigidbody2D rb;
    private AudioSource audioSource;

    public AudioClip[] audioClips;
    public AudioClip deathClip;

    void Awake()
    {
        ballCount++;
        
    }
    void OnDestroy() => ballCount--;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
            LifeLost();         
    }
    
    private void LifeLost()
    {
        if (ballCount <= 1)
        {
            audioSource.PlayOneShot(deathClip);
            GameManager.GM.LifeLost();
        }
        else
            Destroy(gameObject);
    }
}
