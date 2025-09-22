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

    public GameObject text; // Game Over UI (�ν����Ϳ� �Ҵ�)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isJumping = true;
        isGameOver = false;

        if (text != null) text.SetActive(false);
        Time.timeScale = 1f; // Ȥ�� ���� ���·� ���� ���۵Ǵ� �� ����
    }

    void Update()
    {
        if (isGameOver) return;

        // �¿� �̵� (A/D, ��/��)
        float move = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) move = -1f;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) move = 1f;

        // x �ӵ��� ���� (y�� ����)
        rb.linearVelocity = new Vector2(move * MoveSpeed, rb.linearVelocity.y);

        // ����
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpPower);
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴ� ����
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }

        // ������ũ(SpikeAction ���� ������Ʈ)�� �浹 �� ���ӿ���
        if (collision.gameObject.GetComponent<SpikeAction>() != null)
        {
            GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ʈ���� ����� ������ũ ó��
        if (other.GetComponent<SpikeAction>() != null)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (text != null) text.SetActive(true); // ��Game Over�� ���� �ȳ� UI ǥ��
        Time.timeScale = 0f;                    // ��� ����
    }
}
