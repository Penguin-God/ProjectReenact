using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum ActionType
{
    None,
    Move,
    Get,
    Combine,
    Break,
}

public enum UI_Type
{
    Image,
    Button,
}

[Serializable]
public struct ActionData
{
    public ActionType actionType;
    public UI_Type ui_type;
    public string actName;
    public Sprite sprite;
}

public class ReenactSystem : MonoBehaviour
{
    public LayerMask clickableLayer;
    Actor actor;
    [SerializeField] int currentActionPhase;
    [SerializeField] Transform actoinBthParent;
    [SerializeField] GameObject actionBtn;
    [SerializeField] GameObject actionImage;
    [SerializeField] List<InteractObject> holdObjects;
    [SerializeField] DialogueSystem dialogueSystem;

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
                foreach (var data in interactObj.albeActionDatas)
                {
                    if(data.ui_type == UI_Type.Button)
                    {
                        var btn = Instantiate(actionBtn, actoinBthParent).GetComponent<Button>();
                        btn.GetComponentInChildren<TextMeshProUGUI>().text = data.actName;
                        btn.onClick.AddListener(() => OnclickActoinButton(interactObj, data.actionType));
                    }
                    else
                    {
                        var btn = Instantiate(actionImage, actoinBthParent).GetComponent<Button>();
                        btn.onClick.AddListener(() => OnclickActoinButton(interactObj, data.actionType));
                        btn.GetComponent<Image>().sprite = data.sprite;
                    }
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
                actor.currentLocation = interactObject.locationId;
                break;
            case ActionType.Get:
                interactObject.gameObject.SetActive(false);
                holdObjects.Add(interactObject);
                break;
            case ActionType.Combine:
                string[] allPots = new string[] { "유물1", "유물2", "유물3" };
                if (allPots.All(x => holdObjects.Select(obj => obj.objName).Contains(x)))
                    dialogueSystem.StartDialogue("재연성공");
                break;
            default:
                break;
        }
    }
}
