using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField]
    private Rigidbody2D player1;
    [SerializeField]
    private Rigidbody2D player2;

    [SerializeField]
    private BotController botController;
    [SerializeField]
    private Rigidbody2D bot;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private Vector2 moveInput2;
    private Vector2 moveVelocity2;
    [SerializeField]
    private BallController ballController;

    private bool moveAble;

    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI scoreText2;

    public TextMeshProUGUI timerText;

    public Button exit;
    void Start()
    {
        moveAble = true;

        if (PlayerPrefs.GetInt("pvp") == 0)
        {
            player2.gameObject.SetActive(false);
        }
        exit.onClick.AddListener(Exit);
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (moveAble)
        {
            moveInput2 = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));
            moveVelocity2 = moveInput2.normalized * speed;
            player2.MovePosition(player2.position + moveVelocity2 * Time.fixedDeltaTime);
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput.normalized * speed;
            player1.MovePosition(player1.position + moveVelocity * Time.fixedDeltaTime);
        }

        if (player1.position.y > -0.3f)
        {
            player1.position = new Vector2(player1.position.x, -0.3f);

        }


        if (player2.position.y < 0.3f)
        {
            player2.position = new Vector2(player2.position.x, 0.3f);
        }
    }

    public void StopMovement()
    {
        moveAble = false;
        player1.position = new Vector2(0, -4f);
        player1.velocity = Vector2.zero;
        if (botController.GetPvp() == 0)
        {
            botController.SetIsMoveAble(false);
            botController.SetPosition(new Vector2(0, 4f));
        }
        player2.position = new Vector2(0, 4f);
        player2.velocity = Vector2.zero;


        StopAllCoroutines();
        StartCoroutine(CountToStart(3));
    }

    IEnumerator CountToStart(int time)
    {
        int count = time;
        while (count > 0)
        {
            timerText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }

        timerText.text = null;
        if (botController.GetPvp() == 0)
        {
            botController.SetIsMoveAble(true);
        }
        StartMovement();
    }

    public void StartMovement()
    {
        moveAble = true;
        player1.position = new Vector2(0, -4f);
        player2.position = new Vector2(0, 4f);
        ballController.ResetRelativeVelocity();
        ballController.SetIsUp(false);
        if(botController.GetPvp() == 0 && ballController.GetBotLose())
        {
            ballController.AutoShootBall();
        }

    }

    public void UpdateScore(int player)
    {
        if (player == 1)
        {
            scoreText1.text = (int.Parse(scoreText1.text) + 1).ToString();
        }
        else
        {
            scoreText2.text = (int.Parse(scoreText2.text) + 1).ToString();
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
