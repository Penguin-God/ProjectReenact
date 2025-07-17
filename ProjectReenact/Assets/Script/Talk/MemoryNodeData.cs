using UnityEngine;

[CreateAssetMenu(menuName = "Proto/Memory Node")]
public class MemoryNodeData : ScriptableObject
{
    public string nodeId;                // ����ũ ID
    public Sprite thumbnail;             // ���� �� ����� �̹���
    [TextArea] public string description;// Ŭ�� �� ������ ����
    public string[] relatedNodeIds;      // ����� ��� ID ���
}
