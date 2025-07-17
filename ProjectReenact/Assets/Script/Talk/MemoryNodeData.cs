using UnityEngine;

[CreateAssetMenu(menuName = "Proto/Memory Node")]
public class MemoryNodeData : ScriptableObject
{
    public string nodeId;                // 유니크 ID
    public Sprite thumbnail;             // 메인 뷰 축소판 이미지
    [TextArea] public string description;// 클릭 시 보여줄 설명
    public string[] relatedNodeIds;      // 연결된 노드 ID 목록
}
