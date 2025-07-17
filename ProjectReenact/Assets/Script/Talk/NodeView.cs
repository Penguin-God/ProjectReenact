using UnityEngine;
using UnityEngine.UI;

public class NodeView : MonoBehaviour
{
    public MemoryNodeData data { get; private set; }
    public bool visited;    // ·¹ÀÌ¾Æ¿ô ¿ë

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
        // Å¬¸¯ ½Ã »ó¼¼ ºä ¶ç¿ì±â
        // DetailPanel.Instance.Show(data);
    }
}
