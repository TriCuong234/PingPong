using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public Transform ball; // Đối tượng bóng

    public BallController ballController; // Đối tượng BallController
    public float speed = 7f; // Tốc độ di chuyển của bot
    public float reactionTime = 0.5f; // Thời gian phản ứng của bot
    public float reactionDistance = 2f;
    private Rigidbody2D ballRb;
    private Vector3 targetPosition;

    private Rigidbody2D rb; // Đối tượng Rigidbody2D của bot
                            // Đối tượng bóng 
    public float ballSpeedX; // Vận tốc theo trục X 
    public float ballSpeedY; // Vận tốc theo trục Y 
    public float deltaTime = 1f / 60f; // Giả định tốc độ khung hình là 60 FPS 

    private bool isMoveAble = true; // Kiểm tra xem bot có thể di chuyển không

    private int pvp; // Kiểm tra xem có phải chế độ chơi 2 người không

    void Start()
    {
        pvp = PlayerPrefs.GetInt("pvp");

        ballRb = ball.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        if (pvp == 0)
        {
            this.gameObject.SetActive(true);
            return;
        }
        this.gameObject.SetActive(false);
    }

    // void Update()
    // {
    //     Vector2 nextPosition = PredictNextPosition(5); // Dự đoán sau 5 frame 
    //     //Debug.Log("Vị trí dự đoán của bóng sau 5 frame: " + nextPosition);
    // }

    void FixedUpdate()
    {
        if (isMoveAble && pvp == 0)
        {
            if (ballController.GetIsUp())
            {
                Vector2 direction = (ballController.GetNewPosition() - rb.position).normalized;
                Vector2 newVelocity = direction * speed;
                rb.velocity = newVelocity;

                if (rb.position.y < 0.3f)
                {
                    rb.position = new Vector2(rb.position.x, 0.3f);
                }
            }
            else
            {
                if (ballController.GetRelativeVelocity() != Vector2.zero)
                {
                    Vector2 newPosition = Vector2.MoveTowards(rb.position, (new Vector2(ballController.GetNewPosition().x, 4f)), speed * Time.fixedDeltaTime);
                    rb.MovePosition(newPosition);
                    rb.velocity = Vector2.zero;
                }

            }
        }

    }

    public void SetIsMoveAble(bool isMoveAble)
    {
        this.isMoveAble = isMoveAble;
    }

    public int GetPvp()
    {
        return pvp;
    }

    public void SetPosition(Vector2 position)
    {
        rb.position = position;
        rb.velocity = Vector2.zero;
    }

}
