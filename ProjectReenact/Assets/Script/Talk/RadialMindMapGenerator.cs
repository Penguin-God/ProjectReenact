using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 중심 노드에서 바깥으로 방사형으로 뻗는 2-레벨 마인드맵 생성기
/// </summary>
public class RadialMindMapGenerator : MonoBehaviour
{
    [Header("노드 / 선 Prefab")]
    public GameObject nodePrefab;     // SpriteRenderer 달린 원 프리팹
    public LineRenderer linePrefab;   // 선 그릴 LineRenderer 프리팹

    [Header("1차 레이어(중심 바로 옆)")]
    [Range(1, 30)]
    public int firstLayerCount = 8;   // 1차 가지 개수
    public float firstLayerRadius = 4f;

    [Header("2차 레이어(선택)")]
    [Range(0, 10)]
    public int secondPerFirst = 2;    // 1차 노드당 2차 가지 개수
    public float secondLayerRadius = 1.8f;

    void Start() => Generate();

    /******************* 핵심 로직 *******************/
    public void Generate()
    {
        ClearOld();

        // 0) 중심 노드(원점)
        GameObject root = Instantiate(nodePrefab, Vector3.zero, Quaternion.identity, transform);
        root.name = "Root";

        List<GameObject> firstLayer = new();

        // 1) 1차 레이어 배치(원을 따라 균등 + 약간 랜덤)
        for (int i = 0; i < firstLayerCount; i++)
        {
            float baseAngle = 360f / firstLayerCount * i;
            float angle = baseAngle + Random.Range(-10f, 10f);            // ±10° 흔들어 줌
            Vector2 pos = Quaternion.Euler(0, 0, angle) * Vector2.right * firstLayerRadius;

            GameObject child = Instantiate(nodePrefab, pos, Quaternion.identity, transform);
            child.name = $"L1_{i}";
            DrawLine(root.transform.position, child.transform.position);

            firstLayer.Add(child);
        }

        // 2) 2차 레이어(각 1차 노드 주변에 무작위 배치)
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

    /******************* 유틸 *******************/
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
