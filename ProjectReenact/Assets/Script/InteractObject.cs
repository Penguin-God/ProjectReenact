using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class InteractObject : MonoBehaviour
{
    public string objName;
    public int locationId;
    public ActionType[] albeActions;
}
