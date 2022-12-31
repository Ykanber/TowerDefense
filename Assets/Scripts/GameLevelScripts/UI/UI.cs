using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{

    [SerializeField] static TextMeshProUGUI scoreCountText;
    [SerializeField] static TextMeshProUGUI waveDisplayText;
    [SerializeField] static TextMeshProUGUI goldDisplayText;

    public static void SetWaveDisplay(int waveCount, int waveMax)
    {
        if(waveDisplayText == null)
        {
            waveDisplayText = FindObjectOfType<WaveText>().GetComponent<TextMeshProUGUI>();
        }
        waveDisplayText.text = string.Format("{0}/{1}", waveCount + 1, waveMax);
    }    
    
    public static void SetScoreCount(int score)
    {
        if (scoreCountText == null)
        {
            scoreCountText = FindObjectOfType<ScoreText>().GetComponent<TextMeshProUGUI>();
        }
        scoreCountText.text = score.ToString();
    }

    public static void setGold(int gold)
    {
        if (goldDisplayText == null)
        {
            goldDisplayText = FindObjectOfType<GoldText>().GetComponent<TextMeshProUGUI>();
        }
        goldDisplayText.text = string.Format("{0}", gold);
    }
}
