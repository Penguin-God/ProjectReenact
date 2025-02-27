using System.Collections;
using System.Linq;
using UnityEngine;


public class SimulationEvents : MonoBehaviour
{
    [SerializeField] TpPoint[] points;
    [SerializeField] SimulationObject[] objects;
    SimulationObject currentObject;

    public void SetObject(int id) => currentObject = objects.First(x => x.Id == id);
    public void TpToPoint(int id) => currentObject.transform.position = points.First(x => x.TpId == id).transform.position;
    public void MoveToPoint(int id) => StartCoroutine(Co_MoveToPoint(points.First(x => x.TpId == id).transform.position));
    
    IEnumerator Co_MoveToPoint(Vector3 destionation)
    {
        while (true)
        {
            if (Vector2.Distance(currentObject.transform.position, destionation) < 0.5f)
            {
                currentObject.transform.position = destionation;
                break;
            }
            currentObject.transform.position = Vector2.MoveTowards(currentObject.transform.position, destionation, 0.02f);
            yield return null;
        }
    }
}