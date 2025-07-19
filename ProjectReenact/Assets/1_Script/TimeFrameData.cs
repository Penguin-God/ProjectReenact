using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Proto/TimeFrame")]
public class TimeFrameData : ScriptableObject
{
    public string Id;                  // ����ũ ID
    public Sprite snapshotImage;       // �� ������ ������ �̹���
    public int order;
}
