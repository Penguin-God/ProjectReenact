using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class ReenactEventSystem : MonoBehaviour
{
    [SerializeField] DialogueSystem dialogueSystem;
    public float fadeDuration = 1f;
    public Image fadeImage;
    bool isFade;
    [SerializeField] GameObject origin;
    [SerializeField] GameObject doom;
    [SerializeField] GameObject actor;
    void Start()
    {
        dialogueSystem.StartDialogue("�翬����");

        Color c = fadeImage.color;
        fadeImage.color = new Color(c.r, c.g, c.b, 0f);
        fadeImage.gameObject.SetActive(true);
    }

    void Update()
    {
        if(dialogueSystem.IsSeen("�翬����") && isFade == false)
        {
            isFade = true;
            StartCoroutine(FadeSequence());
        }

        if (dialogueSystem.IsSeen("����������"))
        {
            SceneManager.LoadScene(2);
        }
    }

    private IEnumerator FadeSequence()
    {
        // 1. ���� �� ���
        yield return StartCoroutine(Fade(0f, 1f));
        // 2. ��� �� ����
        origin.gameObject.SetActive(false);
        doom.gameObject.SetActive(true);
        actor.gameObject.SetActive(false);
        yield return StartCoroutine(Fade(1f, 0f));
        dialogueSystem.StartDialogue("����������");
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color baseColor = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
            yield return null;
        }

        // ���� ���� �� ����
        fadeImage.color = new Color(baseColor.r, baseColor.g, baseColor.b, endAlpha);
    }
}
