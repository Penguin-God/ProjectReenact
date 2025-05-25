using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DialogueObject : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogue;
}
