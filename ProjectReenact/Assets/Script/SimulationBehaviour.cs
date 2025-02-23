using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimulationBehaviour : MonoBehaviour
{
    [SerializeField] SimulationConditionContainer simulationCondition;
    [SerializeField] int _currnetProgress;
    readonly HashSet<int> _executedEvents = new();

    public void TryReenact(IEnumerable<int> ids)
    {
        SimulationProgressData progressData = simulationCondition.GetProgressData(_currnetProgress);
        foreach (SimulationData data in simulationCondition.SimulationConditions)
        {
            // �ù� ���� ���� : ���� ���൵�� �°� ������Ʈ�� ���� ��
            if (progressData.AbleSimulations.Contains(data.SimulationId) && ids.Contains(data.Id1) && ids.Contains(data.Id2))
            {
                data.DoSimulation();

                // �̺�Ʈ ��������� ���൵ ����
                UpdateProgress(progressData, data);
            }
        }
    }

    void UpdateProgress(SimulationProgressData progressData, SimulationData data)
    {
        _executedEvents.Add(data.SimulationId);
        if (progressData.AdvancementSimulations.All(x => _executedEvents.Contains(x)))
            _currnetProgress++;
    }
}
