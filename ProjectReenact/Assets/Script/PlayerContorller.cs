using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("이동 속도")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private Vector2 lastMovement;
    bool isMove;
    [SerializeField] DialogueSystem dialogueSystem;

    // 마지막 입력이 수평인지 수직인지 저장 (true: 수평, false: 수직)
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
        // 원시 입력값 읽기
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 마지막으로 눌린 입력 축 결정
        if (horizontal != 0 && prevHorizontal == 0)
            lastInputHorizontal = true;
        else if (vertical != 0 && prevVertical == 0)
            lastInputHorizontal = false;

        // 대각선 이동 처리: 두 입력이 모두 있을 때 마지막 입력 축 우선
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
            // 하나의 축만 입력되었거나 입력 없음
            movement.x = horizontal;
            movement.y = vertical;
        }

        // 이전 입력 값 업데이트
        prevHorizontal = horizontal;
        prevVertical = vertical;

        // 마지막 이동 방향 저장
        if (movement != Vector2.zero)
        {
            lastMovement = movement;
            isMove = true;
        }
        else isMove = false;

        // 애니메이터 파라미터 업데이트
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
        // 물리 기반 이동
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
