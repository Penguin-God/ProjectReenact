using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("�̵� �ӵ�")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private Vector2 lastMovement;
    bool isMove;
    [SerializeField] DialogueSystem dialogueSystem;

    // ������ �Է��� �������� �������� ���� (true: ����, false: ����)
    private bool lastInputHorizontal;
    private float prevHorizontal;
    private float prevVertical;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    [SerializeField] LayerMask layerMask;
    void Update()
    {
        // ���� �Է°� �б�
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // ���������� ���� �Է� �� ����
        if (horizontal != 0 && prevHorizontal == 0)
            lastInputHorizontal = true;
        else if (vertical != 0 && prevVertical == 0)
            lastInputHorizontal = false;

        // �밢�� �̵� ó��: �� �Է��� ��� ���� �� ������ �Է� �� �켱
        if (horizontal != 0 && vertical != 0)
        {
            if (lastInputHorizontal)
            {
                movement.x = horizontal;
                movement.y = 0;
            }
            else
            {
                movement.x = 0;
                movement.y = vertical;
            }
        }
        else
        {
            // �ϳ��� �ุ �ԷµǾ��ų� �Է� ����
            movement.x = horizontal;
            movement.y = vertical;
        }

        // ���� �Է� �� ������Ʈ
        prevHorizontal = horizontal;
        prevVertical = vertical;

        // ������ �̵� ���� ����
        if (movement != Vector2.zero)
        {
            lastMovement = movement;
            isMove = true;
        }
        else isMove = false;

        // �ִϸ����� �Ķ���� ������Ʈ
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetFloat("LastMoveX", lastMovement.x);
        animator.SetFloat("LastMoveY", lastMovement.y);
        animator.SetBool("IsMove", isMove);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 direction = lastMovement;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 2f, layerMask);
            if (hit.collider != null)
            {
                DialogueObject trigger = hit.collider.GetComponent<DialogueObject>();
                if (trigger != null)
                    dialogueSystem.StartDialogue(trigger);
            }
        }

    }

    void FixedUpdate()
    {
        // ���� ��� �̵�
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
