using UnityEngine;

public class ClueHover : MonoBehaviour
{
    //Camera cam;
    //NodeBehaviour lastNode;  // �������� ȣ�� ���̴� �ܼ� ����

    //void Awake()
    //{
    //    cam = Camera.main;
    //}

    //void Update()
    //{
    //    Vector2 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
    //    // ���̾� ����ȭ �ϰ� ������ LayerMask �Ἥ Clue ���̾ �ɷ�������
    //    RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

    //    if (hit.collider != null)
    //    {
    //        var node = hit.collider.GetComponent<NodeBehaviour>();
    //        if (node != null)
    //        {
    //            if (node != lastNode)
    //            {
    //                // ���ο� Ŭ��� ȣ�� ����
    //                TooltipManager.Instance.Show(node.data.name);
    //                lastNode = node;
    //            }
    //            return;
    //        }
    //    }

    //    // Clue ���� ������ ���� ����
    //    if (lastNode != null)
    //    {
    //        TooltipManager.Instance.Hide();
    //        lastNode = null;
    //    }
    //}
}
