using System.Linq;
using UnityEngine;

public class SimulationEvents : MonoBehaviour
{
    [SerializeField] TpPoint[] points;
    [SerializeField] SimulationObject[] objects;
    SimulationObject currentObject;

    public void SetObject(int id) => currentObject = objects.First(x => x.Id == id);
    public void TpToPoint(int id) => currentObject.transform.position = points.First(x => x.TpId == id).transform.position;
}
