using UnityEngine;
using UnityEngine.UI;

public class DetailPanel : MonoBehaviour
{
    public static DetailPanel Instance { get; private set; }
    [SerializeField] Image bigImage;
    [SerializeField] Text descriptionText;

    void Awake() => Instance = this;

    public void Show(MemoryNodeData data)
    {
        bigImage.sprite = data.thumbnail;
        descriptionText.text = data.description;
        gameObject.SetActive(true);
    }

    public void Hide() => gameObject.SetActive(false);
}
