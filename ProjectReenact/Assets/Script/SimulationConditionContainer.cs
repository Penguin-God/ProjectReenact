using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SimulationData
{
    [SerializeField] string _name;
    [SerializeField] int _id1;
    [SerializeField] int _id2;
    [SerializeField] int _simulationId;
    [SerializeField] int[] preconditionEvents;

    public string Name => _name;
    public int Id1 => _id1;
    public int Id2 => _id2;
    public int SimulationId => _simulationId;
    public IReadOnlyCollection<int> PreconditionEvents => preconditionEvents;

    [SerializeField] UnityEvent simulationEvent;
    public void DoSimulation() => simulationEvent?.Invoke();
}

public class SimulationConditionContainer : MonoBehaviour
{
    [SerializeField] SimulationData[] simulationDatas;
    public IReadOnlyCollection<SimulationData> SimulationConditions => simulationDatas;
}
