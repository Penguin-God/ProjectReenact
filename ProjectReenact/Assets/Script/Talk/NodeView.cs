using UnityEngine;
using UnityEngine.UI;

public class NodeView : MonoBehaviour
{
    public MemoryNodeData data { get; private set; }
    public bool visited;    // ���̾ƿ� ��

    [SerializeField] Image thumbnailImage;
    [SerializeField] Button selectButton;

    public void Init(MemoryNodeData d)
    {
        data = d;
        thumbnailImage.sprite = d.thumbnail;
        selectButton.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        // Ŭ�� �� �� �� ����
        // DetailPanel.Instance.Show(data);
    }
}
