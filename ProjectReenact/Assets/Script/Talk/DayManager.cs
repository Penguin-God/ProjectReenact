using UnityEngine;
using TMPro;

public enum DayState
{
    AtHome,            // ��
    MovingToFacility,  // ������ �̵� ��
    SelectingNPC,      // ��ȭ�� NPC ���� ���
    InConversation,    // ��ȭ ��
    OffWork,           // ���
    Sleeping           // ���� �� �� ���� ��
}

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("UI ���۷���")]
    public TextMeshProUGUI dateText;             // ��Day 1�� �� ǥ��
    public GameObject homePanel;      // �� ȭ��
    public GameObject facilityPanel;  // ������ ȭ��
    public GameObject npcSelectPanel; // NPC ���� UI
    public GameObject convoPanel;     // ��ȭ UI
    public GameObject offWorkPanel;   // ��� ȭ��
    public GameObject sleepPanel;     // ���� ȭ��

    [Header("����")]
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
            // ��ȭ ���� �� ��ȭ UI �ʿ��� ��ư���� �������� �б� ����
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
        // ��� �г� �����, �ش� ����� �ѱ�
        homePanel.SetActive(phase == DayState.AtHome);
        facilityPanel.SetActive(phase == DayState.MovingToFacility);
        npcSelectPanel.SetActive(phase == DayState.SelectingNPC);
        convoPanel.SetActive(phase == DayState.InConversation);
        offWorkPanel.SetActive(phase == DayState.OffWork);
        sleepPanel.SetActive(phase == DayState.Sleeping);
    }

    // NPC ���� ȭ�鿡�� ��ư���� ��ȭ ������ �� ȣ��
    public void OnNPCSelected(int npcId)
    {
        SetPhase(DayState.InConversation);
    }

    // ��ȭ UI���� ����ȭ ���� ��ư�� ����
    public void OnConversationEnded()
    {
        AdvancePhase();  // OffWork �ܰ��
    }
}
