using System.Collections;
using System.Linq;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public static MergeManager Instance { get; private set; }

    [Header("���� ������")]
    public MergeRecipes mergeRecipes;
    [Header("������ ��� Prefab")]
    public GameObject nodePrefab;

    NodeBehaviour firstSelected;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SelectNode(NodeBehaviour nb)
    {
        // 1) ù ����
        if (firstSelected == null)
        {
            firstSelected = nb;
            Highlight(nb.gameObject, true);
            return;
        }

        // 2) ���� ��� �ٽ� Ŭ�� �� ���
        if (nb == firstSelected)
        {
            Highlight(firstSelected.gameObject, false);
            firstSelected = null;
            return;
        }

        // 3) �� ��° ���� �� ���� �õ�
        TryMerge(firstSelected, nb);
        Highlight(firstSelected.gameObject, false);
        firstSelected = null;
    }

    void TryMerge(NodeBehaviour a, NodeBehaviour b)
    {
        // recipes���� (A,B) �Ǵ� (B,A) ���� �˻�
        var recipe = mergeRecipes.recipes
            .FirstOrDefault(r =>
                (r.nodeA == a.nodeData && r.nodeB == b.nodeData) ||
                (r.nodeA == b.nodeData && r.nodeB == a.nodeData));

        if (recipe == null)
        {
            // ���� �ǵ��
            StartCoroutine(FlashRed(a.gameObject));
            StartCoroutine(FlashRed(b.gameObject));
            return;
        }

        // ����: �� ��� �����, ���� 2�� �ı�
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
