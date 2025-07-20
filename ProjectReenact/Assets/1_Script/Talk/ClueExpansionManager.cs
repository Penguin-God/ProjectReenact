using UnityEngine;
using System.Collections.Generic;

public class ClueExpansionManager : MonoBehaviour
{
    [Tooltip("씬에 배치된 시작 단서 (ID로도 일치시켜야 함)")]
    public ClueBehaviour startClue;

    [Tooltip("모든 Expansion Rule SO")]
    public ClueExpansionRuleSO[] rules;

    [Tooltip("기본 숨김 알파 (0 = 완전 안 보임)")]
    [Range(0, 1)] public float defaultHiddenAlpha = 0f;

    [SerializeField] GameObject line;

    // ID → ClueBehaviour 맵
    private Dictionary<string, ClueBehaviour> _clueDict;

    void Awake()
    {
        // 씬에 있는 모든 ClueBehaviour 수집
        _clueDict = new Dictionary<string, ClueBehaviour>();
        foreach (var cb in FindObjectsOfType<ClueBehaviour>())
        {
            if (!_clueDict.ContainsKey(cb.ID))
                _clueDict.Add(cb.ID, cb);
            else
                Debug.LogWarning($"중복된 ClueID: {cb.ID}");
        }
    }

    void Start()
    {
        // 1) 모든 단서 숨기기
        foreach (var cb in _clueDict.Values)
            cb.SetAlpha(defaultHiddenAlpha);

        // 2) 시작 단서 공개 & 자식 노출
        if (startClue != null)
            Reveal(startClue);
    }

    public void Reveal(ClueBehaviour clue)
    {
        float startAhapa = 0.3f;
        clue.Reveal();

        // 룰 찾아서 자식들만 알파 세팅 + 선 그리기
        foreach (var rule in rules)
        {
            if (rule.parentClueID != clue.ID) continue;

            foreach (var child in rule.children)
            {
                if (_clueDict.TryGetValue(child.childClueID, out var cb))
                {
                    cb.gameObject.SetActive(true);
                    // 자식 흐릿한 알파로 설정
                    cb.SetAlpha(startAhapa);
                    DrawLine(clue.transform.position, cb.transform.position);
                }
                else
                {
                    Debug.LogWarning($"ClueExpansionRule에서 찾을 수 없는 childID: {child.childClueID}");
                }
            }
            break;
        }
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        var lr = Instantiate(line).GetComponent<LineRenderer>();
        // 위치 지정
        lr.positionCount = 2;
        lr.useWorldSpace = true;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
