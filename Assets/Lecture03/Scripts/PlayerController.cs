using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    bool isJumping = true;
    bool isGameOver = false;

    public float JumpPower = 10.0f;
    public float MoveSpeed = 5.0f;

    public GameObject text; // Game Over UI (인스펙터에 할당)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isJumping = true;
        isGameOver = false;

        if (text != null) text.SetActive(false);
        Time.timeScale = 1f; // 혹시 정지 상태로 씬이 시작되는 것 방지
    }

    void Update()
    {
        if (isGameOver) return;

        // 좌우 이동 (A/D, ←/→)
        float move = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) move = -1f;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) move = 1f;

        // x 속도만 갱신 (y는 유지)
        rb.linearVelocity = new Vector2(move * MoveSpeed, rb.linearVelocity.y);

        // 점프
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpPower);
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥 착지
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }

        // 스파이크(SpikeAction 붙은 오브젝트)와 충돌 시 게임오버
        if (collision.gameObject.GetComponent<SpikeAction>() != null)
        {
            GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 트리거 방식의 스파이크 처리
        if (other.GetComponent<SpikeAction>() != null)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (text != null) text.SetActive(true); // “Game Over” 같은 안내 UI 표시
        Time.timeScale = 0f;                    // 즉시 정지
    }
}
