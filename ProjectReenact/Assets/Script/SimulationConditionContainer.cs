using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SimulationCondition
{
    public int id1;
    public int id2;
    [SerializeField] UnityEvent simulationEvent;
    public void DoSimulation() => simulationEvent?.Invoke();
}

public class SimulationConditionContainer : MonoBehaviour
{
    [SerializeField] SimulationCondition[] simulationConditions;
    public IReadOnlyCollection<SimulationCondition> SimulationConditions => simulationConditions;
}
