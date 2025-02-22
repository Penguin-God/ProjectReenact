using UnityEngine;

public class SimulationObject : MonoBehaviour
{
    [SerializeField] string _name;
    public string Name => _name;

    [SerializeField] int _id;
    public int Id => _id;
}
