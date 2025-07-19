using UnityEngine;

public class CombinationManager : MonoBehaviour
{
    [Tooltip("���� ������ ��� ���� �������� �־�μ���.")]
    public CombinationRuleSO[] allRules;

    public void Combine(ClueBehaviour c1, ClueBehaviour c2)
    {
        foreach (var rule in allRules)
        {
            bool matches = (rule.clueA == c1.type && rule.clueB == c2.type) || (rule.clueA == c2.type && rule.clueB == c1.type);

            if (!matches) continue;

            // �̺�Ʈ ���� ����
            foreach (var ev in rule.events)
                ev.Execute();
            return;
        }

        print("���� ����");
    }
}
