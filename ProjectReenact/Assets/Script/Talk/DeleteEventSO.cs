using UnityEngine;

[CreateAssetMenu(menuName = "MapEvent/Delete Event")]
public class DeleteEventSO : MapEventSO
{
    public string targetTag;

    public override void Execute()
    {
        var gos = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (var go in gos)
            Destroy(go);
    }
}
