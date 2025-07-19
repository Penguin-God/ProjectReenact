using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance { get; private set; }

    [Header("���� UI")]
    public RectTransform tooltipPanel;   // TooltipPanel �Ҵ�
    public TextMeshProUGUI tooltipText;             // TooltipText �Ҵ�
    public Vector2 offset = new Vector2(10, -10);  // ���콺 Ŀ�� ���� ������

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        tooltipPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        // ������ ���� ���� ����, �г��� ���콺 ��ġ�� �ű�
        if (tooltipPanel.gameObject.activeSelf)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                tooltipPanel.parent as RectTransform,
                Input.mousePosition,
                null,    // Screen Space - Overlay �� null
                out pos
            );
            tooltipPanel.anchoredPosition = pos + offset;
        }
    }

    public void Show(string message)
    {
        tooltipText.text = message;
        tooltipPanel.gameObject.SetActive(true);
    }

    public void Hide()
    {
        tooltipPanel.gameObject.SetActive(false);
    }
}
