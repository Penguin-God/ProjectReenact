using UnityEngine;


public class ArrowMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ� ���� ����

    private Rigidbody2D rb;
    private Vector2 movement;

    // ���� �� Rigidbody2D ������Ʈ�� �����ɴϴ�.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // WASD Ȥ�� ����Ű �Է��� �޽��ϴ�.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // ������ �ð� ���ݸ��� ���� ������ �����մϴ�.
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
