using UnityEngine;

public class ClueBehaviour : MonoBehaviour
{
    public ClueTypeSO type;

    [Tooltip("씬에서 이 단서를 식별할 고유 ID")]
    public string clueID;

    // 2D 스프라이트용
    [SerializeField] SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public string ID => clueID;

    public void SetAlpha(float a)
    {
        var c = spriteRenderer.color;
        c.a = a;
        spriteRenderer.color = c;
    }

    public void Reveal() => SetAlpha(1f);
}
