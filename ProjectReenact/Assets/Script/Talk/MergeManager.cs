using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    // --- 싱글턴 편의용 ---
    public static MergeManager Instance { get; private set; }
    void Awake() => Instance = this;

    [Header("생성에 사용할 프리팹")]
    public GameObject nodePrefab;       // 새 노드 만들 때 사용
    public LineRenderer linePrefab;     // 선 다시 그릴 때 사용

    [Header("허용된 조합: (A,B) → C")]
    public List<Recipe> recipes;        // 인스펙터에서 세팅

    NodeBehaviour waiting;              // 첫 번째로 클릭된 노드

    // ----------------------------------------------------------------------------------

    /// <summary>노드 클릭 시 호출</summary>
    public void SelectNode(NodeBehaviour node)
    {
        // 1) 첫 선택 없다면 보류
        if (waiting == null)
        {
            waiting = node;
            Highlight(waiting, true);
            return;
        }

        // 2) 같은 노드 다시 누르면 취소
        if (waiting == node)
        {
            Highlight(waiting, false);
            waiting = null;
            return;
        }

        // 3) 두 번째 노드 → 조합 검사
        string resultType = GetResult(waiting.nodeType, node.nodeType);

        if (resultType == null)                    // 불가한 조합
        {
            // 피드백 (색 반짝이기 등) 넣고 초기화
            FlashRed(node);
            Highlight(waiting, false);
            waiting = null;
            return;
        }

        // 4) 합치기
        Merge(waiting, node, resultType);
        waiting = null;
    }

    /******************* 내부 로직 *******************/
    void Merge(NodeBehaviour a, NodeBehaviour b, string resultType)
    {
        // 새 위치 = 두 노드 중간
        Vector3 pos = (a.transform.position + b.transform.position) / 2f;

        // 새 노드 생성
        GameObject newObj = Instantiate(nodePrefab, pos, Quaternion.identity);
        var nb = newObj.GetComponent<NodeBehaviour>();
        nb.nodeType = resultType;
        newObj.name = $"Node_{resultType}";

        // 연결 라인 다시 그리기
        //   ① a,b 와 연결돼 있던 LineRenderer 전부 파괴
        foreach (Transform child in a.transform.parent)  // 노드들이 한 부모 밑에 있다고 가정
        {
            var lr = child.GetComponent<LineRenderer>();
            if (lr && (lr.GetPosition(0) == a.transform.position ||
                       lr.GetPosition(1) == a.transform.position ||
                       lr.GetPosition(0) == b.transform.position ||
                       lr.GetPosition(1) == b.transform.position))
                Destroy(child.gameObject);
        }

        //   ② 두 노드가 갖고 있던 이웃 찾아 새 노드에 연결
        var neighbours = GatherNeighbours(a);
        neighbours.UnionWith(GatherNeighbours(b));
        neighbours.Remove(a); neighbours.Remove(b);

        foreach (var n in neighbours)
        {
            DrawLine(newObj.transform.position, n.transform.position);
        }

        // 옛 노드 삭제
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
        // 간단히 거리가 매우 작은 노드 탐색
        foreach (var nb in FindObjectsOfType<NodeBehaviour>())
            if (Vector2.Distance(nb.transform.position, pos) < 0.01f)
                return nb;
        return null;
    }

    /******************* 유틸 & 데이터 구조 *******************/
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
        return null;    // 조합 불가
    }

    void DrawLine(Vector3 a, Vector3 b)
    {
        LineRenderer lr = Instantiate(linePrefab, transform);
        lr.positionCount = 2;
        lr.SetPosition(0, a);
        lr.SetPosition(1, b);
    }

    // 시각적 보조 ─ 원하는 대로 교체
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
