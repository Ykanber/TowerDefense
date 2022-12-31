using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamageButton : MonoBehaviour
{
    [SerializeField] Scorpion scorpion;


    private void OnMouseDown()
    {
        scorpion.BuyDamage();
    }
}
