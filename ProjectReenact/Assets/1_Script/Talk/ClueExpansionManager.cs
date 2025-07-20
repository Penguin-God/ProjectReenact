using UnityEngine;
using System.Collections.Generic;

public class ClueExpansionManager : MonoBehaviour
{
    [Tooltip("���� ��ġ�� ���� �ܼ� (ID�ε� ��ġ���Ѿ� ��)")]
    public ClueBehaviour startClue;

    [Tooltip("��� Expansion Rule SO")]
    public ClueExpansionRuleSO[] rules;

    [Tooltip("�⺻ ���� ���� (0 = ���� �� ����)")]
    [Range(0, 1)] public float defaultHiddenAlpha = 0f;

    [SerializeField] GameObject line;

    // ID �� ClueBehaviour ��
    private Dictionary<string, ClueBehaviour> _clueDict;

    void Awake()
    {
        // ���� �ִ� ��� ClueBehaviour ����
        _clueDict = new Dictionary<string, ClueBehaviour>();
        foreach (var cb in FindObjectsOfType<ClueBehaviour>())
        {
            if (!_clueDict.ContainsKey(cb.ID))
                _clueDict.Add(cb.ID, cb);
            else
                Debug.LogWarning($"�ߺ��� ClueID: {cb.ID}");
        }
    }

    void Start()
    {
        // 1) ��� �ܼ� �����
        foreach (var cb in _clueDict.Values)
            cb.SetAlpha(defaultHiddenAlpha);

        // 2) ���� �ܼ� ���� & �ڽ� ����
        if (startClue != null)
            Reveal(startClue);
    }

    public void Reveal(ClueBehaviour clue)
    {
        float startAhapa = 0.3f;
        clue.Reveal();

        // �� ã�Ƽ� �ڽĵ鸸 ���� ���� + �� �׸���
        foreach (var rule in rules)
        {
            if (rule.parentClueID != clue.ID) continue;

            foreach (var child in rule.children)
            {
                if (_clueDict.TryGetValue(child.childClueID, out var cb))
                {
                    cb.gameObject.SetActive(true);
                    // �ڽ� �帴�� ���ķ� ����
                    cb.SetAlpha(startAhapa);
                    DrawLine(clue.transform.position, cb.transform.position);
                }
                else
                {
                    Debug.LogWarning($"ClueExpansionRule���� ã�� �� ���� childID: {child.childClueID}");
                }
            }
            break;
        }
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        var lr = Instantiate(line).GetComponent<LineRenderer>();
        // ��ġ ����
        lr.positionCount = 2;
        lr.useWorldSpace = true;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
