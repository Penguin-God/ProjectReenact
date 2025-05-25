using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;

    private Queue<string> sentences;
    bool isDialoging;

    void Awake()
    {
        sentences = new Queue<string>();
        dialogueUI.SetActive(false);
    }

    public void StartDialogue(DialogueObject dialogueObj)
    {
        if (isDialoging) return;

        isDialoging = true;
        dialogueUI.SetActive(true);
        sentences.Clear();
        foreach (string sentence in dialogueObj.dialogue)
            sentences.Enqueue(sentence);
        DisplayNextSentence();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        isDialoging = false;
        dialogueUI.SetActive(false);
    }
}
