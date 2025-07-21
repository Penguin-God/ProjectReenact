using UnityEngine;

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
    [SerializeField] ClueManager clueManager;
    void Start()
    {
        foreach (var cb in clueManager.Clues)
            cb.gameObject.SetActive(false);

        // 2) ���� �ܼ� ���� & �ڽ� ����
        if (startClue != null)
            Reveal(startClue);
    }



    public void Reveal(ClueBehaviour clue)
    {
        float startAhapa = 0.3f;
        clue.Reveal(1f);

        // �� ã�Ƽ� �ڽĵ鸸 ���� ���� + �� �׸���
        foreach (var rule in rules)
        {
            if (rule.parentClueID != clue.ID) continue;
            foreach (var child in rule.children)
            {
                if (clueManager.ClueDict.TryGetValue(child.childClueID, out var cb))
                {
                    cb.Reveal(startAhapa);
                    DrawLine(clue.transform.position, cb.transform.position);
                }
                else
                    Debug.Log($"ClueExpansionRule���� ã�� �� ���� childID: {child.childClueID}");
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
