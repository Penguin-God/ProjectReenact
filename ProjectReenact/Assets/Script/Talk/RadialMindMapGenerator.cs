using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �߽� ��忡�� �ٱ����� ��������� ���� 2-���� ���ε�� ������
/// </summary>
public class RadialMindMapGenerator : MonoBehaviour
{
    [Header("��� / �� Prefab")]
    public GameObject nodePrefab;     // SpriteRenderer �޸� �� ������
    public LineRenderer linePrefab;   // �� �׸� LineRenderer ������

    [Header("1�� ���̾�(�߽� �ٷ� ��)")]
    [Range(1, 30)]
    public int firstLayerCount = 8;   // 1�� ���� ����
    public float firstLayerRadius = 4f;

    [Header("2�� ���̾�(����)")]
    [Range(0, 10)]
    public int secondPerFirst = 2;    // 1�� ���� 2�� ���� ����
    public float secondLayerRadius = 1.8f;

    void Start() => Generate();

    /******************* �ٽ� ���� *******************/
    public void Generate()
    {
        ClearOld();

        // 0) �߽� ���(����)
        GameObject root = Instantiate(nodePrefab, Vector3.zero, Quaternion.identity, transform);
        root.name = "Root";

        List<GameObject> firstLayer = new();

        // 1) 1�� ���̾� ��ġ(���� ���� �յ� + �ణ ����)
        for (int i = 0; i < firstLayerCount; i++)
        {
            float baseAngle = 360f / firstLayerCount * i;
            float angle = baseAngle + Random.Range(-10f, 10f);            // ��10�� ���� ��
            Vector2 pos = Quaternion.Euler(0, 0, angle) * Vector2.right * firstLayerRadius;

            GameObject child = Instantiate(nodePrefab, pos, Quaternion.identity, transform);
            child.name = $"L1_{i}";
            DrawLine(root.transform.position, child.transform.position);

            firstLayer.Add(child);
        }

        // 2) 2�� ���̾�(�� 1�� ��� �ֺ��� ������ ��ġ)
        foreach (var parent in firstLayer)
        {
            for (int j = 0; j < secondPerFirst; j++)
            {
                float angle = Random.Range(0f, 360f);
                Vector2 pos = parent.transform.position + (Quaternion.Euler(0, 0, angle) * Vector2.right * secondLayerRadius);

                GameObject sub = Instantiate(nodePrefab, pos, Quaternion.identity, transform);
                sub.name = $"L2_{parent.name}_{j}";
                DrawLine(parent.transform.position, sub.transform.position);
            }
        }
    }

    /******************* ��ƿ *******************/
    void DrawLine(Vector3 a, Vector3 b)
    {
        LineRenderer lr = Instantiate(linePrefab, transform);
        lr.positionCount = 2;
        lr.SetPosition(0, a);
        lr.SetPosition(1, b);
    }

    void ClearOld()
    {
        foreach (Transform child in transform) { Destroy(child.gameObject); }
    }
}
