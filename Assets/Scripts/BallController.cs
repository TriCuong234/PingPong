using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float defaultSpeed = 8f;

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private BotController botController;

    Vector2 newPosition = new Vector2(0, 0);

    Vector2 relativeVelocity = new Vector2(0, 0);

    private Vector2 previousPosition;
    private Vector2 currentPosition; // Vị trí hiện tại

    private bool botLose;
    private bool isUp;
    void Start()
    {
        botLose = false;
        isUp = false;
        rb = GetComponent<Rigidbody2D>();
        previousPosition = rb.position;
        currentPosition = rb.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        newPosition = rb.position + relativeVelocity * Time.fixedDeltaTime;

        currentPosition = rb.position;
        if (currentPosition != previousPosition)
        {
            if (currentPosition.y - previousPosition.y> 0 ) {
                isUp = true;
            } else {
                isUp = false;
            }

            previousPosition = currentPosition;
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("End"))
        {
            rb.velocity = Vector2.zero;
            playerController.StopMovement();
            playerController.UpdateScore(1);
            ResetBallPosition(2);
            if(botController.GetPvp() == 0)
            {
                botLose = true;
            }
        }
        if (col.gameObject.CompareTag("End2"))
        {
            rb.velocity = Vector2.zero;
            playerController.StopMovement();
            playerController.UpdateScore(2);
            ResetBallPosition(1);
            if(botController.GetPvp() == 0)
            {
                botLose = false;
            }
        }

        Rigidbody2D otherRb = col.rigidbody;
        if (otherRb != null)
        {
            Vector2 collisionNormal = col.contacts[0].normal;
            relativeVelocity = col.relativeVelocity;
            //Vector2 newPosition = otherRb.position + otherRb.velocity * Time.fixedDeltaTime; Debug.Log("Vị trí mới của vật thể bị va chạm: " + newPosition); } }
        }

    }

    public void ResetBallPosition(int player)
    {
        if (player == 1)
        {
            rb.position = new Vector2(0, -3f);
            return;
        }
        rb.position = new Vector2(0, 3f);
    }

    public Vector2 GetNewPosition()
    {
        return newPosition;
    }

    public Vector2 GetRelativeVelocity()
    {
        return relativeVelocity;
    }

    public bool GetIsUp()
    {
        return this.isUp;
    }

    public void SetIsUp(bool value)
    {
        this.isUp = value;
    }

   
    public void ResetRelativeVelocity()
    {
        this.relativeVelocity = Vector2.zero;
        previousPosition = rb.position;
        currentPosition = rb.position;
        
    }
    public bool GetBotLose()
    {
        return botLose;
    }

    public void AutoShootBall(){
        rb.velocity = new Vector2(Random.Range(-10,10), -defaultSpeed);
    }
}
