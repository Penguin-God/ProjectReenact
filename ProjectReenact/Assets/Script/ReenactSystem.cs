using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ActionType
{
    Move,
    Get,
    PutDown,
}

public class ReenactSystem : MonoBehaviour
{
    public LayerMask clickableLayer;
    Actor actor;
    [SerializeField] int currentActionPhase;
    [SerializeField] Transform actoinBthParent;
    [SerializeField] GameObject actionBtn;

    // 액터 먼저 클릭하고 저장하고 오브젝트 클릭하고 저장하고 행동 선택해서 이동하기
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
            else if(currentActionPhase == 1 && hit.collider.TryGetComponent(out InteractObject interactObj))
            {
                foreach (var action in interactObj.albeActions)
                {
                    var btn = Instantiate(actionBtn, actoinBthParent).GetComponent<Button>();
                    btn.GetComponentInChildren<TextMeshProUGUI>().text = action.ToString();
                    btn.onClick.AddListener(() => OnclickActoinButton(interactObj, action));
                }
                currentActionPhase = 2;
            }
        }
    }

    // 이런식으로 오브젝트 없어지기, 조건에 따라 행동에 대한 이벤트 실행하기. (유물 3개 모으고 삼각형 밖에서 올바른 상형문자 선택 시 마을 없어지기)
    // 연출은 하얀색 화면으로 바뀐 후 텅 비기
    // 오브젝트 상호작용으로 변경하기. 이동도 액터, 위치 선택 후 이동으로 고르면 됨
    void OnclickActoinButton(InteractObject interactObject, ActionType actionType)
    {
        if (currentActionPhase != 2) return;

        Action(actor, interactObject, actionType);
        actor = null;
        currentActionPhase = 0;
        foreach (Transform child in actoinBthParent)
            Destroy(child.gameObject);
    }

    void Action(Actor actor, InteractObject interactObject, ActionType actionType)
    {
        switch (actionType)
        {
            case ActionType.Move:
                actor.transform.position = interactObject.transform.position;
                actor.transform.position = new Vector3(actor.transform.position.x, actor.transform.position.y, -1);
                break;
            case ActionType.Get:
                interactObject.gameObject.SetActive(false);
                break;
            case ActionType.PutDown:
                break;
            default:
                break;
        }
    }
}
