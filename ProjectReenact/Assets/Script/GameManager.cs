using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Button reenactBtn;
    [SerializeField] GameObject mapBtn;
    [SerializeField] string[] conditionIds; // string id로 변경
    [SerializeField] DialogueSystem dialogueSystem;
    [SerializeField] SpriteRenderer mural1;
    [SerializeField] Sprite mural1Origin;

    [SerializeField] SpriteRenderer mural2;
    [SerializeField] Sprite mural2Origin;

    void Start()
    {
        reenactBtn.onClick.AddListener(SceneToReenact);
        dialogueSystem.StartDialogue("시작");
    }

    void SceneToReenact()
    {
        SceneManager.LoadScene(1);
    }

    bool start;
    void Update()
    {
        if (dialogueSystem.IsSeen("시작") && start == false)
        {
            start = true;
            mapBtn.SetActive(true);
        }

        if (conditionIds.All(x => dialogueSystem.IsSeen(x)))
            reenactBtn.gameObject.SetActive(true);

        if(dialogueSystem.IsSeen("유물재연1"))
            mural1.sprite = mural1Origin;

        if (dialogueSystem.IsSeen("유물재연2"))
            mural2.sprite = mural2Origin;
    }
}
