using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUI : MonoBehaviour
{

    [SerializeField] GameObject increaseDamageButton;
    [SerializeField] GameObject increaseRangeButton;

    [SerializeField] Scorpion scorpion;


    private void Update()
    {
        if(scorpion.Range < scorpion.rangeMax && GoldManager.GetGoldCount() >= 50) // if can upgrade active
        {
            increaseRangeButton.SetActive(true);
        }
        else
        {
            increaseRangeButton.SetActive(false);
        }
        if (scorpion.DamageLevel < scorpion.MaxDamageLv && GoldManager.GetGoldCount() >= 50) // if can upgrade active
        {
            increaseDamageButton.SetActive(true);
        }
        else
        {
            increaseDamageButton.SetActive(false);
        }
    }



}
