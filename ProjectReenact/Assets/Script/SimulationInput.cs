using UnityEngine;
using System.Collections.Generic;

public class SimulationInput : MonoBehaviour
{
    [SerializeField] SimulationBehaviour simulationBehaviour;
    List<int> clickedIDs = new List<int>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치를 월드 좌표로 변환
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // 해당 위치에서 2D 레이캐스트 수행
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider == null) return;
            SimulationObject clickable = hit.collider.GetComponent<SimulationObject>();
            if(clickable == null) return;
            clickedIDs.Add(clickable.Id);
            Debug.Log("클릭한 오브젝트 ID: " + clickable.Id);

            if (clickedIDs.Count == 2)
            {
                simulationBehaviour.TryReenact(clickedIDs);
                clickedIDs.Clear();
            }
        }
    }
}
