using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rb2d;


    public ParticleSystem collisionParticle;

    public float maxInitialAngle = 0.67f;

    public float moveSpeed = 1f;

    private float startX = 0f;

    public float maxStartY = 4f;

    public float speedMultiplier = 1.5f;

    private void Start()
    {   
       GameManager.instance.onReset += ResetBall;
       GameManager.instance.gameUI.onStartGame += ResetBall;
    }

    private void InitialPush()
    {
        Vector2 dir = Random.value < 0.5f ? Vector2.left : Vector2.right;
        
        dir.y = Random.Range(-maxInitialAngle,maxInitialAngle);
        rb2d.velocity = dir * moveSpeed;
        EmitParticle(32);
    }

    private void ResetBall()
    {
        ResetBallPosition();
        InitialPush();
    }

    private void ResetBallPosition()
    {
       float posY = Random.Range(-maxStartY, maxStartY);
       Vector2 position = new Vector2(startX, posY);
       transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
       if(scoreZone)
       {
          GameManager.instance.OnScoreZoneReached(scoreZone.id);
          
       }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Paddle paddle = collision.collider.GetComponent<Paddle>();

        if(paddle)
        {
            GameManager.instance.gameAudio.PlayPaddleSound();
            rb2d.velocity *= speedMultiplier; 
            EmitParticle(16);
        }

        Wall wall = collision.collider.GetComponent<Wall>();

        if(wall)
        {
            GameManager.instance.gameAudio.PlayWallSound();
            EmitParticle(8);
             
        }


    }

    private void EmitParticle(int amount)
    {
        collisionParticle.Emit(amount);
    }
}
