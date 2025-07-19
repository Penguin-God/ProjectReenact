// ClueExpansionRuleSO.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Clue/Expansion Rule")]
public class ClueExpansionRuleSO : ScriptableObject
{
    [Tooltip("�� ���� ����� �θ� Clue�� ID")]
    public string parentClueID;

    [System.Serializable]
    public class ChildEntry
    {
        [Tooltip("���� �̸� ��ġ�� �ڽ� Clue�� ID")]
        public string childClueID;
    }

    [Tooltip("�θ� ���� �� �巯�� �ڽ� ���")]
    public ChildEntry[] children;
}
