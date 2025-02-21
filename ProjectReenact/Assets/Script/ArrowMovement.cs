using UnityEngine;


public class ArrowMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도 조절 변수

    private Rigidbody2D rb;
    private Vector2 movement;

    // 시작 시 Rigidbody2D 컴포넌트를 가져옵니다.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // WASD 혹은 방향키 입력을 받습니다.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // 고정된 시간 간격마다 물리 연산을 적용합니다.
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
