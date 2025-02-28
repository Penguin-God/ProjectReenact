using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class SimulationBehaviour : MonoBehaviour
{
    [SerializeField] SimulationConditionContainer simulationCondition;
    public event Action<int> OnExcuteEvent;
    readonly HashSet<int> _executedEvents = new();

    public void TryReenact(IEnumerable<int> ids)
    {
        foreach (SimulationData data in simulationCondition.SimulationConditions)
        {
            if (CheckIds(ids, data) && SeemPreconditionEvents(data))
            {
                data.DoSimulation();
                _executedEvents.Add(data.SimulationId);
                OnExcuteEvent?.Invoke(data.SimulationId);
            }
        }
    }

    bool CheckIds(IEnumerable<int> ids, SimulationData data) => ids.Contains(data.Id1) && ids.Contains(data.Id2);
    bool SeemPreconditionEvents(SimulationData data) => data.PreconditionEvents.All(x => _executedEvents.Contains(x));
}
