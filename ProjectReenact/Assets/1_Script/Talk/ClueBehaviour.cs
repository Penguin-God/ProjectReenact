using UnityEngine;

public class ClueBehaviour : MonoBehaviour
{
    public ClueTypeSO type;

    [Tooltip("������ �� �ܼ��� �ĺ��� ���� ID")]
    public string clueID;

    // 2D ��������Ʈ��
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
