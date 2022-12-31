using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseRangeButton : MonoBehaviour
{

    [SerializeField] Scorpion scorpion;

    private void OnMouseDown()
    {
        scorpion.BuyRange();
    }
}
