using UnityEngine;

[CreateAssetMenu(menuName = "MapEvent/Create Event")]
public class CreateEventSO : MapEventSO
{
    public GameObject prefab;
    public Vector3 spawnPosition;

    public override void Execute()
    {
        Vector3 worldPos = spawnPosition;
        GameObject.Instantiate(prefab, worldPos, Quaternion.identity);
    }
}
