using UnityEngine;

public class ClueHover : MonoBehaviour
{
    Camera cam;
    ClueBehaviour lastClue;  // �������� ȣ�� ���̴� �ܼ� ����

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector2 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        // ���̾� ����ȭ �ϰ� ������ LayerMask �Ἥ Clue ���̾ �ɷ�������
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null)
        {
            var clue = hit.collider.GetComponent<ClueBehaviour>();
            if (clue != null)
            {
                if (clue != lastClue)
                {
                    // ���ο� Ŭ��� ȣ�� ����
                    TooltipManager.Instance.Show(clue.type.type);
                    lastClue = clue;
                }
                return;
            }
        }

        // Clue ���� ������ ���� ����
        if (lastClue != null)
        {
            TooltipManager.Instance.Hide();
            lastClue = null;
        }
    }
}
