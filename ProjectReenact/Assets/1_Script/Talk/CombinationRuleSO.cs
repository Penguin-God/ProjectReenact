using UnityEngine;

[CreateAssetMenu(menuName = "Clue/Combination Rule")]
public class CombinationRuleSO : ScriptableObject
{
    public ClueTypeSO clueA;
    public ClueTypeSO clueB;
    public MapEventSO[] events;  // ���� �̺�Ʈ�� ������� �迭 ����
}
