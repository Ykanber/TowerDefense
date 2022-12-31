using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    static int goldCount;

    public static void IncreaseGold(int gold)
    {
        goldCount += gold;
        UI.setGold(goldCount);
    }
    public static void DecreaseGold(int gold)
    {
        goldCount -= gold;
        UI.setGold(goldCount);
    }

    public static void SetGoldCount(int gold)
    {
        goldCount = gold;
        UI.setGold(goldCount);
    }

    public static int GetGoldCount()
    {
        return goldCount;
    }
}
