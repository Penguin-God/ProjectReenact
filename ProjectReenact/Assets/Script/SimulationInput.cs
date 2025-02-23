using UnityEngine;
using System.Collections.Generic;

public class SimulationInput : MonoBehaviour
{
    [SerializeField] SimulationUI_Controller uI_Controller;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 클릭한 시뮬 오브젝트의 ID를 넘김
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider == null) return;
            SimulationObject clickable = hit.collider.GetComponent<SimulationObject>();
            if(clickable == null) return;

            uI_Controller.UpdateSelectObjText(clickable.Id);
        }
    }
}
