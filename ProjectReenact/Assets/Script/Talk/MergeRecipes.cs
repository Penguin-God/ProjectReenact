using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MindMap/MergeRecipes", fileName = "MergeRecipes_")]
public class MergeRecipes : ScriptableObject
{
    [System.Serializable]
    public class Recipe
    {
        public MindMapNode nodeA;      // 합성 재료1
        public MindMapNode nodeB;      // 합성 재료2
        public MindMapNode resultNode; // 생성될 노드
    }

    [Tooltip("허용된 머지 조합 목록")]
    public List<Recipe> recipes = new();
}
