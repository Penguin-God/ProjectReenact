using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class CombineClickHandler : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;                              // Ŭ�� ����ĳ��Ʈ�� ����� ī�޶�
    public CombinationManager combinationManager;          // ���� ������ �����ϴ� �Ŵ���

    [Header("Highlight Settings")]
    public Color highlightColor = Color.yellow;            // ���� �� ���̶���Ʈ ����

    [Header("Feedback Settings")]
    public LineRenderer lineRenderer;                      // ���õ� �� �ܼ��� �����ϴ� ���� ������
    public float feedbackDelay = 0.5f;                     // ���� �� �ǵ�� ��� �ð�

    private ClueBehaviour firstClue;                       // ù ��° �ܼ�
    private ClueBehaviour secondClue;                      // �� ��° �ܼ�

    private Renderer firstRenderer;
    private Color firstOriginalColor;

    private Renderer secondRenderer;
    private Color secondOriginalColor;

    void Reset()
    {
        mainCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }
    }

    [SerializeField] bool isCombine = false;
    CombinationRuleSO currentRule = null;
    public void StartCombination(CombinationRuleSO rule)
    {
        currentRule = rule;
        isCombine = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isCombine)
        {
            TrySelectClue();
        }
    }

    private void TrySelectClue()
    {
        Vector2 worldPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null)
        {
            var clue = hit.collider.GetComponent<ClueBehaviour>();
            if (clue != null)
            {
                HandleClueClick(clue);
            }
        }
    }

    private void HandleClueClick(ClueBehaviour clue)
    {
        if (firstClue == null)
        {
            // ù ��° �ܼ� ���� �� ���̶���Ʈ
            firstClue = clue;
            ApplyHighlight(ref firstRenderer, ref firstOriginalColor, clue);
        }
        else if (firstClue == clue)
        {
            // ���� �ܼ� �ٽ� Ŭ�� �� ���� ����
            ClearFirstSelection();
        }
        else
        {
            // �� ��° �ܼ� ����
            secondClue = clue;
            ApplyHighlight(ref secondRenderer, ref secondOriginalColor, clue);

            // �ǵ��: ���� �׸��� �� ���� ����
            if (lineRenderer != null)
                DrawConnectionLine();

            StartCoroutine(CombineWithFeedback());
        }
    }

    private IEnumerator CombineWithFeedback()
    {
        // ��� �ǵ�� ���
        yield return new WaitForSeconds(feedbackDelay);

        // ���� ����
        ExecuteCombine();
    }

    private void ExecuteCombine()
    {
        // combinationManager.Combine(firstClue, secondClue);
        combinationManager.Combine(currentRule, firstClue, secondClue);
        // ���̶���Ʈ �� ���� ����
        ClearLine();
        ClearFirstSelection();
        ClearSecondSelection();

        // ���� �ʱ�ȭ
        firstClue = null;
        secondClue = null;
        currentRule = null;
        isCombine = false;
    }

    private void DrawConnectionLine()
    {
        if (firstClue != null && secondClue != null)
        {
            lineRenderer.SetPosition(0, firstClue.transform.position);
            lineRenderer.SetPosition(1, secondClue.transform.position);
            lineRenderer.enabled = true;
        }
    }

    private void ClearLine()
    {
        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }

    private void ApplyHighlight(ref Renderer rend, ref Color originalColor, ClueBehaviour clue)
    {
        rend = clue.GetComponent<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color;
            rend.material.color = highlightColor;
        }
    }

    private void ClearFirstSelection()
    {
        if (firstRenderer != null)
        {
            firstRenderer.material.color = firstOriginalColor;
            firstRenderer = null;
        }
        firstClue = null;
    }

    private void ClearSecondSelection()
    {
        if (secondRenderer != null)
        {
            secondRenderer.material.color = secondOriginalColor;
            secondRenderer = null;
        }
        secondClue = null;
    }
}
