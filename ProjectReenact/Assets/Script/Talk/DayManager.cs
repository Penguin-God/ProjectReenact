using UnityEngine;
using TMPro;

public enum DayState
{
    AtHome,            // 집
    MovingToFacility,  // 수감소 이동 중
    SelectingNPC,      // 대화할 NPC 선택 대기
    InConversation,    // 대화 중
    OffWork,           // 퇴근
    Sleeping           // 수면 중 → 다음 날
}

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("UI 레퍼런스")]
    public TextMeshProUGUI dateText;             // “Day 1” 등 표시
    public GameObject homePanel;      // 집 화면
    public GameObject facilityPanel;  // 수감소 화면
    public GameObject npcSelectPanel; // NPC 선택 UI
    public GameObject convoPanel;     // 대화 UI
    public GameObject offWorkPanel;   // 퇴근 화면
    public GameObject sleepPanel;     // 수면 화면

    [Header("설정")]
    public int startDay = 1;

    [HideInInspector] public int currentDay;
    [HideInInspector] public DayState phase;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentDay = startDay;
        SetPhase(DayState.AtHome);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 대화 중일 땐 대화 UI 쪽에서 버튼으로 끝내도록 분기 가능
            if (phase != DayState.InConversation)
                AdvancePhase();
        }
    }

    public void AdvancePhase()
    {
        DayState next;
        switch (phase)
        {
            case DayState.AtHome: next = DayState.MovingToFacility; break;
            case DayState.MovingToFacility: next = DayState.SelectingNPC; break;
            case DayState.SelectingNPC: next = DayState.InConversation; break;
            case DayState.InConversation: next = DayState.OffWork; break;
            case DayState.OffWork: next = DayState.Sleeping; break;
            case DayState.Sleeping:
                currentDay++;
                dateText.text = $"Day {currentDay}";
                next = DayState.AtHome;
                break;
            default: next = DayState.AtHome; break;
        }
        SetPhase(next);
    }

    void SetPhase(DayState newPhase)
    {
        phase = newPhase;
        // 모든 패널 숨기고, 해당 페이즈만 켜기
        homePanel.SetActive(phase == DayState.AtHome);
        facilityPanel.SetActive(phase == DayState.MovingToFacility);
        npcSelectPanel.SetActive(phase == DayState.SelectingNPC);
        convoPanel.SetActive(phase == DayState.InConversation);
        offWorkPanel.SetActive(phase == DayState.OffWork);
        sleepPanel.SetActive(phase == DayState.Sleeping);
    }

    // NPC 선택 화면에서 버튼으로 대화 시작할 때 호출
    public void OnNPCSelected(int npcId)
    {
        SetPhase(DayState.InConversation);
    }

    // 대화 UI에서 ‘대화 끝’ 버튼에 연결
    public void OnConversationEnded()
    {
        AdvancePhase();  // OffWork 단계로
    }
}
