using UnityEngine;
using System.Collections.Generic;

public class SimulationInput : MonoBehaviour
{
    [SerializeField] SimulationConditionContainer simulationConditions;
    List<int> clickedIDs = new List<int>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // �ش� ��ġ���� 2D ����ĳ��Ʈ ����
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider == null) return;
            SimulationObject clickable = hit.collider.GetComponent<SimulationObject>();
            if(clickable == null) return;
            clickedIDs.Add(clickable.Id);
            Debug.Log("Ŭ���� ������Ʈ ID: " + clickable.Id);

            if (clickedIDs.Count == 2)
            {
                CheckCombination(clickedIDs);
                clickedIDs.Clear();
            }
        }
    }

    // �� ID�� ���տ� ���� �̺�Ʈ�� �����ϴ� �Լ�
    void CheckCombination(List<int> ids)
    {
        foreach(SimulationCondition condition in simulationConditions.SimulationConditions)
        {
            if (ids.Contains(condition.id1) && ids.Contains(condition.id2))
                condition.DoSimulation();
        }
    }
}
