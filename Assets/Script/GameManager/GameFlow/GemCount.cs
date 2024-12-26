using UnityEngine;

public class GemCount : MonoBehaviour
{
    public static GemCount instance;
    private int gemCount;
    public bool lastBoss = false;
    private void Awake()
    {
        instance = this;
        gemCount = 2;
    }

    public void IncreaseGemCount()
    {
        gemCount++;
        if (gemCount == 3)
        {
            lastBoss = true;
        }
    }
}
