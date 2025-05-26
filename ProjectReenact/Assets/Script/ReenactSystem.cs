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
                actor.currentLocation = interactObject.objName;
                break;
            case ActionType.Get:
                if(actor.currentLocation == interactObject.objName)
                    interactObject.gameObject.SetActive(false);
                break;
            case ActionType.PutDown:
                break;
            default:
                break;
        }
    }
}
