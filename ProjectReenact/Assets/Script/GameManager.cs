using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Button reenactBtn;
    [SerializeField] string[] conditionIds; // string id로 변경
    [SerializeField] DialogueSystem dialogueSystem;
    [SerializeField] SpriteRenderer mural1;
    [SerializeField] Sprite mural1Origin;

    [SerializeField] SpriteRenderer mural2;
    [SerializeField] Sprite mural2Origin;

    void Awake()
    {
        reenactBtn.onClick.AddListener(SceneToReenact);
    }

    void SceneToReenact()
    {
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        if (conditionIds.All(x => dialogueSystem.IsSeen(x)))
            reenactBtn.gameObject.SetActive(true);

        if(dialogueSystem.IsSeen("유물재연1"))
            mural1.sprite = mural1Origin;

        //if (dialogueSystem.IsSeen("유물2"))
        //    mural2.sprite = mural2Origin;
    }
}
