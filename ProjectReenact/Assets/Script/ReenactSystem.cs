using UnityEngine;
using UnityEngine.UI;

public class ReenactSystem : MonoBehaviour
{
    public LayerMask clickableLayer;
    Actor actor;
    InteractObject interactObj;
    [SerializeField] int currentActionPhase;
    [SerializeField] Button moveBtn;

    void Awake()
    {
        moveBtn.onClick.AddListener(OnclickActoinButton);
    }

    // ���� ���� Ŭ���ϰ� �����ϰ� ������Ʈ Ŭ���ϰ� �����ϰ� �ൿ �����ؼ� �̵��ϱ�
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPoint = new Vector2(worldPoint.x, worldPoint.y);

            RaycastHit2D hit;
            hit = Physics2D.Raycast(clickPoint, Vector2.zero, Mathf.Infinity, clickableLayer);
            if (hit.collider == null) return;

            if(currentActionPhase == 0 && hit.collider.TryGetComponent(out actor))
                currentActionPhase = 1;
            else if(currentActionPhase == 1 && hit.collider.TryGetComponent(out interactObj))
                currentActionPhase = 2;
        }
    }

    void OnclickActoinButton()
    {
        if (currentActionPhase != 2) return;
        
        actor.transform.position = interactObj.transform.position;
        actor = null;
        interactObj = null;
        currentActionPhase = 0;
    }
}
