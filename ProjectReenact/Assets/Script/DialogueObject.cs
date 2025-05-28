using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DialogueObject : MonoBehaviour
{
    public string dialogueId;
    DialogueSystem dialogueSystem;

    void Awake()
    {
        dialogueSystem = FindAnyObjectByType<DialogueSystem>();
    }

    void OnMouseDown()
    {
        dialogueSystem.StartDialogue(dialogueId);
    }
}
