using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Button reenactBtn;
    [SerializeField] string[] conditionIds; // string id·Î º¯°æ
    [SerializeField] DialogueSystem dialogueSystem;

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
    }
}
