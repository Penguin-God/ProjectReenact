using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    // --- �̱��� ���ǿ� ---
    public static MergeManager Instance { get; private set; }
    void Awake() => Instance = this;

    [Header("������ ����� ������")]
    public GameObject nodePrefab;       // �� ��� ���� �� ���
    public LineRenderer linePrefab;     // �� �ٽ� �׸� �� ���

    [Header("���� ����: (A,B) �� C")]
    public List<Recipe> recipes;        // �ν����Ϳ��� ����

    NodeBehaviour waiting;              // ù ��°�� Ŭ���� ���

    // ----------------------------------------------------------------------------------

    /// <summary>��� Ŭ�� �� ȣ��</summary>
    public void SelectNode(NodeBehaviour node)
    {
        // 1) ù ���� ���ٸ� ����
        if (waiting == null)
        {
            waiting = node;
            Highlight(waiting, true);
            return;
        }

        // 2) ���� ��� �ٽ� ������ ���
        if (waiting == node)
        {
            Highlight(waiting, false);
            waiting = null;
            return;
        }

        // 3) �� ��° ��� �� ���� �˻�
        string resultType = GetResult(waiting.nodeType, node.nodeType);

        if (resultType == null)                    // �Ұ��� ����
        {
            // �ǵ�� (�� ��¦�̱� ��) �ְ� �ʱ�ȭ
            FlashRed(node);
            Highlight(waiting, false);
            waiting = null;
            return;
        }

        // 4) ��ġ��
        Merge(waiting, node, resultType);
        waiting = null;
    }

    /******************* ���� ���� *******************/
    void Merge(NodeBehaviour a, NodeBehaviour b, string resultType)
    {
        // �� ��ġ = �� ��� �߰�
        Vector3 pos = (a.transform.position + b.transform.position) / 2f;

        // �� ��� ����
        GameObject newObj = Instantiate(nodePrefab, pos, Quaternion.identity);
        var nb = newObj.GetComponent<NodeBehaviour>();
        nb.nodeType = resultType;
        newObj.name = $"Node_{resultType}";

        // ���� ���� �ٽ� �׸���
        //   �� a,b �� ����� �ִ� LineRenderer ���� �ı�
        foreach (Transform child in a.transform.parent)  // ������ �� �θ� �ؿ� �ִٰ� ����
        {
            var lr = child.GetComponent<LineRenderer>();
            if (lr && (lr.GetPosition(0) == a.transform.position ||
                       lr.GetPosition(1) == a.transform.position ||
                       lr.GetPosition(0) == b.transform.position ||
                       lr.GetPosition(1) == b.transform.position))
                Destroy(child.gameObject);
        }

        //   �� �� ��尡 ���� �ִ� �̿� ã�� �� ��忡 ����
        var neighbours = GatherNeighbours(a);
        neighbours.UnionWith(GatherNeighbours(b));
        neighbours.Remove(a); neighbours.Remove(b);

        foreach (var n in neighbours)
        {
            DrawLine(newObj.transform.position, n.transform.position);
        }

        // �� ��� ����
        Destroy(a.gameObject);
        Destroy(b.gameObject);
    }

    HashSet<NodeBehaviour> GatherNeighbours(NodeBehaviour node)
    {
        HashSet<NodeBehaviour> set = new();
        foreach (Transform child in node.transform.parent)
        {
            var lr = child.GetComponent<LineRenderer>();
            if (lr == null) continue;

            if (lr.GetPosition(0) == node.transform.position)
            {
                NodeBehaviour other = FindNodeAt(lr.GetPosition(1));
                if (other) set.Add(other);
            }
            else if (lr.GetPosition(1) == node.transform.position)
            {
                NodeBehaviour other = FindNodeAt(lr.GetPosition(0));
                if (other) set.Add(other);
            }
        }
        return set;
    }

    NodeBehaviour FindNodeAt(Vector3 pos)
    {
        // ������ �Ÿ��� �ſ� ���� ��� Ž��
        foreach (var nb in FindObjectsOfType<NodeBehaviour>())
            if (Vector2.Distance(nb.transform.position, pos) < 0.01f)
                return nb;
        return null;
    }

    /******************* ��ƿ & ������ ���� *******************/
    [System.Serializable]
    public class Recipe
    {
        public string typeA;
        public string typeB;
        public string resultType;
    }

    string GetResult(string a, string b)
    {
        foreach (var r in recipes)
        {
            bool match = (r.typeA == a && r.typeB == b) ||
                         (r.typeA == b && r.typeB == a);
            if (match) return r.resultType;
        }
        return null;    // ���� �Ұ�
    }

    void DrawLine(Vector3 a, Vector3 b)
    {
        LineRenderer lr = Instantiate(linePrefab, transform);
        lr.positionCount = 2;
        lr.SetPosition(0, a);
        lr.SetPosition(1, b);
    }

    // �ð��� ���� �� ���ϴ� ��� ��ü
    void Highlight(NodeBehaviour n, bool on) =>
        n.GetComponent<SpriteRenderer>().color = on ? Color.yellow : Color.white;

    void FlashRed(NodeBehaviour n)
    {
        var sr = n.GetComponent<SpriteRenderer>();
        sr.color = Color.red;
        Invoke(nameof(ResetColor), 0.2f);
        void ResetColor() => sr.color = Color.white;
    }
}
