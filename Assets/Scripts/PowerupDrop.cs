using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType
{
    None,
    Catch
}

public class PowerupDrop : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float speed;
    public PowerupType type;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0, -speed, 0);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
            Destroy(gameObject);
    }
}
