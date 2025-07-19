using UnityEngine;

public class CombinationManager : MonoBehaviour
{
    [Tooltip("조합 가능한 모든 룰을 에셋으로 넣어두세요.")]
    public CombinationRuleSO[] allRules;

    public void Combine(ClueBehaviour c1, ClueBehaviour c2)
    {
        foreach (var rule in allRules)
        {
            bool matches = (rule.clueA == c1.type && rule.clueB == c2.type) || (rule.clueA == c2.type && rule.clueB == c1.type);

            if (!matches) continue;

            // 이벤트 순차 실행
            foreach (var ev in rule.events)
                ev.Execute();
            return;
        }

        print("조합 없고");
    }
}
