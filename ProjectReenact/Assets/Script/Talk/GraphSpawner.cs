using System.Collections.Generic;
using UnityEngine;

public class HierarchicalGraphSpawner : MonoBehaviour
{
    [Header("필수")]
    public MindMapNode root;
    public GameObject nodePrefab;    // NodeBehaviour 달린 프리팹
    public LineRenderer linePrefab;

    [Header("자동 레이아웃 옵션")]
    public float layerGap = 3f;       // 레벨 간 거리
    public bool autoLayout = true;   // false면 manualPosition 사용

    readonly Dictionary<MindMapNode, GameObject> spawned = new();

    void Start() => Spawn();

    [ContextMenu("Respawn")]
    public void Spawn()
    {
        ClearOld();
        if (root == null) { Debug.LogError("Root SO가 비어 있습니다"); return; }

        if (autoLayout)
            LayoutRadial(root, 0, 0, 360);   // 위치 계산 먼저
        Traverse(root, null);
    }

    /************** 계층 레이아웃 (방사형) **************/
    void LayoutRadial(MindMapNode n, int depth, float startA, float endA)
    {
        float angle = (startA + endA) * 0.5f;
        float r = depth * layerGap;
        n.manualPosition = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad),
                                       Mathf.Sin(angle * Mathf.Deg2Rad)) * r;

        int c = n.children.Count;
        if (c == 0) return;

        float slice = (endA - startA) / c;
        for (int i = 0; i < c; i++)
            LayoutRadial(n.children[i], depth + 1,
                         startA + slice * i,
                         startA + slice * (i + 1));
    }

    /************** 인스턴스 & 선 그리기 **************/
    void Traverse(MindMapNode so, MindMapNode parent)
    {
        // 이미 생성됐으면 재귀 중복 방지
        if (spawned.TryGetValue(so, out var go) == false)
        {
            go = Instantiate(nodePrefab, so.manualPosition, Quaternion.identity, transform);
            go.name = $"Node_{so.id}";
            go.GetComponent<NodeBehaviour>().nodeData = so;
            spawned[so] = go;
        }

        // 부모가 있으면 선 하나만
        if (parent != null)
            DrawLine(spawned[parent].transform.position, go.transform.position);

        // 자식 재귀
        foreach (var child in so.children)
            if (child != null)
                Traverse(child, so);
    }

    void DrawLine(Vector3 a, Vector3 b)
    {
        var lr = Instantiate(linePrefab, transform);
        lr.positionCount = 2;
        lr.SetPosition(0, a);
        lr.SetPosition(1, b);
    }

    void ClearOld()
    {
        foreach (Transform t in transform) Destroy(t.gameObject);
        spawned.Clear();
    }
}
