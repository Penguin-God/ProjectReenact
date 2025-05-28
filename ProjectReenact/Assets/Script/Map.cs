using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField] Button[] btns;
    [SerializeField] Transform[] points;
    [SerializeField] Actor actor;
    [SerializeField] GameObject mapBtn;
    [SerializeField] Transform cameraTf;
    void Awake()
    {
        for (int i = 0; i < btns.Length; i++)
        {
            int num = i;
            btns[i].onClick.AddListener(() => MoveToPoint(points[num]));
        }
    }

    void MoveToPoint(Transform point)
    {
        cameraTf.position = new Vector3(point.position.x, point.position.y, -10);
        actor.transform.position = point.position;
        gameObject.SetActive(false);
        mapBtn.SetActive(true);
    }
}
