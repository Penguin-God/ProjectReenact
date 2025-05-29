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
        dialogueSystem.StartDialogue("재연시작");

        Color c = fadeImage.color;
        fadeImage.color = new Color(c.r, c.g, c.b, 0f);
        fadeImage.gameObject.SetActive(true);
    }

    void Update()
    {
        if(dialogueSystem.IsSeen("재연성공") && isFade == false)
        {
            isFade = true;
            StartCoroutine(FadeSequence());
        }

        if (dialogueSystem.IsSeen("마을없어짐"))
        {
            SceneManager.LoadScene(2);
        }
    }

    private IEnumerator FadeSequence()
    {
        // 1. 투명 → 흰색
        yield return StartCoroutine(Fade(0f, 1f));
        // 2. 흰색 → 투명
        origin.gameObject.SetActive(false);
        doom.gameObject.SetActive(true);
        actor.gameObject.SetActive(false);
        yield return StartCoroutine(Fade(1f, 0f));
        dialogueSystem.StartDialogue("마을없어짐");
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

        // 최종 알파 값 보정
        fadeImage.color = new Color(baseColor.r, baseColor.g, baseColor.b, endAlpha);
    }
}
