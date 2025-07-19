using System.Collections;
using System.Linq;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public static MergeManager Instance { get; private set; }

    [Header("머지 데이터")]
    public MergeRecipes mergeRecipes;
    [Header("생성할 노드 Prefab")]
    public GameObject nodePrefab;

    NodeBehaviour firstSelected;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SelectNode(NodeBehaviour nb)
    {
        // 1) 첫 선택
        if (firstSelected == null)
        {
            firstSelected = nb;
            Highlight(nb.gameObject, true);
            return;
        }

        // 2) 같은 노드 다시 클릭 → 취소
        if (nb == firstSelected)
        {
            Highlight(firstSelected.gameObject, false);
            firstSelected = null;
            return;
        }

        // 3) 두 번째 선택 → 머지 시도
        TryMerge(firstSelected, nb);
        Highlight(firstSelected.gameObject, false);
        firstSelected = null;
    }

    void TryMerge(NodeBehaviour a, NodeBehaviour b)
    {
        // recipes에서 (A,B) 또는 (B,A) 조합 검색
        var recipe = mergeRecipes.recipes
            .FirstOrDefault(r =>
                (r.nodeA == a.nodeData && r.nodeB == b.nodeData) ||
                (r.nodeA == b.nodeData && r.nodeB == a.nodeData));

        if (recipe == null)
        {
            // 실패 피드백
            StartCoroutine(FlashRed(a.gameObject));
            StartCoroutine(FlashRed(b.gameObject));
            return;
        }

        // 성공: 새 노드 만들고, 기존 2개 파괴
        Vector3 spawnPos = (a.transform.position + b.transform.position) * 0.5f;
        Destroy(a.gameObject);
        Destroy(b.gameObject);

        GameObject go = Instantiate(nodePrefab, spawnPos, Quaternion.identity);
        var nb = go.GetComponent<NodeBehaviour>();
        nb.nodeData = recipe.resultNode;
        go.name = $"Node_{recipe.resultNode.id}";
    }

    IEnumerator FlashRed(GameObject go)
    {
        var sr = go.GetComponent<SpriteRenderer>();
        var orig = sr.color;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sr.color = orig;
    }

    void Highlight(GameObject go, bool on)
    {
        var sr = go.GetComponent<SpriteRenderer>();
        sr.color = on ? Color.yellow : Color.white;
    }
}
