using UnityEngine;
using UnityEngine.UI;

public class BuyTowerButtonScript : MonoBehaviour
{
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();    
    }

    void Update()
    {
        if (GoldManager.GetGoldCount() >= 50)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
