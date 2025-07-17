using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MemoryGraphManager : MonoBehaviour
{
    public MemoryNodeData[] allNodes;    // �������� ����� ��� ���
    public GameObject nodePrefab;        // NodeView ������
    public Transform nodeRoot;           // NodeView �� �θ�

    // ��Ÿ�ӿ� ������ �����ϱ� ���� ��ųʸ�
    Dictionary<string, MemoryNodeData> nodeDataMap;
    Dictionary<string, NodeView> nodeViews;

    void Awake()
    {
        nodeDataMap = allNodes.ToDictionary(n => n.nodeId, n => n);
        nodeViews = new Dictionary<string, NodeView>();
    }

    public void BuildGraph(string startNodeId)
    {
        // 1) ���� ������ ���/ť�� �ʿ��� ��常 �Ѹ���
        Queue<string> q = new Queue<string>();
        HashSet<string> visited = new HashSet<string>();
        q.Enqueue(startNodeId);
        visited.Add(startNodeId);

        while (q.Count > 0)
        {
            var id = q.Dequeue();
            CreateNodeView(id);

            foreach (var rel in nodeDataMap[id].relatedNodeIds)
            {
                if (visited.Add(rel))
                    q.Enqueue(rel);
            }
        }

        // 2) ��� ��� �Ѹ� �ڿ� ���ἱ �׸���
        foreach (var kv in nodeViews)
        {
            var src = kv.Value;
            foreach (var relId in src.data.relatedNodeIds)
            {
                if (nodeViews.TryGetValue(relId, out var dst))
                    DrawEdge(src, dst);
            }
        }

        ApplyLayout();
    }

    void CreateNodeView(string nodeId)
    {
        if (nodeViews.ContainsKey(nodeId)) return;

        var data = nodeDataMap[nodeId];
        var go = Instantiate(nodePrefab, nodeRoot);
        var view = go.GetComponent<NodeView>();
        view.Init(data);
        nodeViews[nodeId] = view;
    }

    void DrawEdge(NodeView a, NodeView b)
    {
        var lr = new GameObject($"Edge_{a.data.nodeId}_{b.data.nodeId}")
                    .AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPositions(new Vector3[] { a.transform.position, b.transform.position });
        // ���׸���, ���� ���� �ν����Ϳ��� ����
    }

    void ApplyLayout()
    {
        float radiusStep = 200f;
        
        void LayoutRec(string id, Vector2 pos, int depth)
        {
            var view = nodeViews[id];
            view.transform.localPosition = pos;
            int n = view.data.relatedNodeIds.Length;
            float angleStep = 360f / Mathf.Max(1, n);
            for (int i = 0; i < n; i++)
            {
                var childId = view.data.relatedNodeIds[i];
                if (!view.visited)
                {
                    view.visited = true;
                    float ang = i * angleStep * Mathf.Deg2Rad;
                    Vector2 childPos = pos + new Vector2(Mathf.Cos(ang), Mathf.Sin(ang)) * radiusStep * depth;
                    LayoutRec(childId, childPos, depth + 1);
                }
            }
        }

        LayoutRec("start", Vector2.zero, 1);
    }
}
