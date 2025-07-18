using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MindMap/Node", fileName = "Node_")]
public class MindMapNode : ScriptableObject
{
    public string id;
    public string type;

    [Tooltip("이 노드의 자식들 (계층)")]
    public List<MindMapNode> children = new();

    // 위치를 직접 찍고 싶다면 여기에 추가
    public Vector2 manualPosition = Vector2.zero;
}
