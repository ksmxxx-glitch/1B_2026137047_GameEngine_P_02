using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.Rendering.DebugUI;


public class PlayerController : MonoBehaviour

{

    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;

    float score;

    private bool isGiant = false;
    private bool isMove = false;
    private bool isJump = false;

    private float moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
        score = 0f;
    }

    private void Update()
    {

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (isGiant)
        {
            if (moveInput < 0)
                transform.localScale = new Vector3(2, 2, 2);
            else if (moveInput > 0)
                transform.localScale = new Vector3(-2, 2, 2);
        }
        else
        { 
            if (moveInput < 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (moveInput > 0)
                transform.localScale = new Vector3(-1, 1, 1);

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 2f, groundLayer);
        }


    }
    public void  OnMove(InputValue value)
    { 
        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }

        { 
            if (isJump)
                rb.AddForce(Vector2.up * (jumpForce + 3f), ForceMode2D.Impulse);
            else
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            pAni.SetTrigger("Jump");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.CompareTag("Trap"))
        {
            if (isGiant)
                Destroy(collision.gameObject);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.CompareTag("Finish"))
        {
            //  HighScore.TrySet(SceneManager.GetActiveScene().buildIndex, (int)score);
            StageResultSaver.SaveStage(SceneManager.GetActiveScene().buildIndex, (int)score);

            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }

        if (collision.CompareTag("Enemy"))
        {
            if (isGiant)
                Destroy(collision.gameObject);
            else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.CompareTag("Enemy2"))
        {
            if (isGiant)
                Destroy(collision.gameObject);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.CompareTag("Item"))
        {
            isGiant = true;
            Invoke(nameof(ResetGiant), 3f);
            score += collision.GetComponent<ItemObject>().GetPoint();
            Destroy(collision.gameObject);
            
            // 만약 아이템을 먹었으면 점수를 10점 올려준다.
            //  score += 10f;
        }

        if (collision.CompareTag("SpeedItem"))
        {
            isMove = true;
            Invoke(nameof(ResetMove), 7f);
            Destroy(collision.gameObject);
            score += collision.GetComponent<ItemObject>().GetPoint();
            //  score += 10f;
        }


        if (collision.CompareTag("JumpItem"))
        {
            isJump = true;
            Invoke(nameof(ResetJump), 3f);
            Destroy(collision.gameObject);
            score += collision.GetComponent<ItemObject>().GetPoint();
            //  score += 10f;
        }

        if (isMove)
            rb.linearVelocity = new Vector2(moveInput * (moveSpeed + 3f), rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

    }




    void ResetGiant()
        {
            isGiant = false;
        }

        void ResetMove()
        {
            isMove = false;
        }

        void ResetJump()
        {
            isJump = false;
        }

}








