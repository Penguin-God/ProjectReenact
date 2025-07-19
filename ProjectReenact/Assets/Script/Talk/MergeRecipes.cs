using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MindMap/MergeRecipes", fileName = "MergeRecipes_")]
public class MergeRecipes : ScriptableObject
{
    [System.Serializable]
    public class Recipe
    {
        public MindMapNode nodeA;      // �ռ� ���1
        public MindMapNode nodeB;      // �ռ� ���2
        public MindMapNode resultNode; // ������ ���
    }

    [Tooltip("���� ���� ���� ���")]
    public List<Recipe> recipes = new();
}
