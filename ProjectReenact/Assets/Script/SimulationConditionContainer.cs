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

    public string Name => _name;
    public int Id1 => _id1;
    public int Id2 => _id2;
    public int SimulationId => _simulationId;

    [SerializeField] UnityEvent simulationEvent;
    public void DoSimulation() => simulationEvent?.Invoke();
}

[System.Serializable]
public class SimulationProgressData
{
    [SerializeField] int _progress;
    [SerializeField] int[] ableSimulatoins;
    [SerializeField] int[] advancementSimulations; // 승?진 조건. 다음으로 넘어가기 위함

    public int Progress => _progress;
    public IReadOnlyCollection<int> AbleSimulations => ableSimulatoins;
    public IReadOnlyCollection<int> AdvancementSimulations => advancementSimulations;
}

public class SimulationConditionContainer : MonoBehaviour
{
    [SerializeField] SimulationData[] simulationDatas;
    public IReadOnlyCollection<SimulationData> SimulationConditions => simulationDatas;

    [SerializeField] SimulationProgressData[] simulationProgressDatas;
    public SimulationProgressData GetProgressData(int progress) => simulationProgressDatas[progress];
}
