using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController PC;
    
    [SerializeField] private Transform ballOrigin;
    private Ball _ball = null;
    
    public PowerupType currentPowerup = PowerupType.None;
    public float powerupTimer = 0.0f;

    [SerializeField] private float initialSpeed = 0.1f;
    [SerializeField] private float ballSpeed = 0.1f;
    [Min(0.0f)]public float speedIncrease;
    
    // Start is called before the first frame update
    void Start()
    {
        GrabBall(FindObjectOfType<Ball>());
        ballSpeed = initialSpeed;
        PC = this;
    }

    private void OnDestroy() => PC = null;

    // Update is called once per frame
    void Update()
    {
        //Debug skip level
        if (Input.GetKeyDown(KeyCode.PageDown))
            GameManager.GM.NextLevel();
        
        if (Input.GetButtonUp("Action") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            LaunchBall();
        }
        if (currentPowerup != PowerupType.None)
        {
            powerupTimer -= Time.deltaTime;
            if (powerupTimer <= 0)
                currentPowerup = PowerupType.None;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball)
        {
            BounceBall(ball);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PowerupDrop powerupDrop = collision.gameObject.GetComponent<PowerupDrop>();
        if (powerupDrop)
        {
            currentPowerup = powerupDrop.type;
            powerupTimer = 10.0f;
            Destroy(powerupDrop.gameObject);
        }
    }

    public void LaunchBall()
    {
        if (!_ball) return;
        
        _ball.transform.parent = null;
        _ball.gameObject.SetActive(true);
        _ball.rb.velocity = (_ball.transform.position - transform.position).normalized * ballSpeed;
        _ball = null;
    }

    public void BounceBall(Ball ball)
    {
        if (currentPowerup == PowerupType.Catch)
        {
            GrabBall(ball);
        }
        else
        {
            ball.rb.velocity = (ball.transform.position - transform.position).normalized * ballSpeed;
            ballSpeed += speedIncrease;
        }
    }

    public void GrabBall(Ball ball)
    {
        if (!ball) return;
        
        _ball = ball;
        _ball.transform.parent = ballOrigin.transform;
        _ball.transform.localPosition = Vector3.zero;
        if (_ball.rb)
            _ball.rb.velocity = Vector2.zero;
    }
    
}
