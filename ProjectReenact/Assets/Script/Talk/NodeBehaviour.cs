using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NodeBehaviour : MonoBehaviour
{
    public MindMapNode nodeData;   // ▶︎ SO 레퍼런스 그대로 저장

    public string id;
    public string nodeType;

    void OnMouseDown() => MergeManager.Instance.SelectNode(this);
}
