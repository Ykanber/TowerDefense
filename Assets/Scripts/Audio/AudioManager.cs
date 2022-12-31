using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip generateTower;
    [SerializeField] AudioClip addRange;
    [SerializeField] AudioClip addDamage;
    [SerializeField] AudioClip nextWave;
    [SerializeField] AudioClip levelFailed;
    [SerializeField] AudioClip levelPassed;
    [SerializeField] AudioClip enemyHit;

    public void PlayGenerateTower() 
    {
        AudioSource.PlayClipAtPoint(generateTower, Camera.main.transform.position,0.7f);
    }
    public void PlayAddRange()
    {
        AudioSource.PlayClipAtPoint(addRange, Camera.main.transform.position);
    }
    public void PlayAddDamage()
    {
        AudioSource.PlayClipAtPoint(addDamage, Camera.main.transform.position);
    }
    public void PlayNextWave()
    {
        AudioSource.PlayClipAtPoint(nextWave, Camera.main.transform.position);
    }
    public void PlayLevelFailed()
    {
        AudioSource.PlayClipAtPoint(levelFailed, Camera.main.transform.position);
    }
    public void PlayLevelPassed()
    {
        AudioSource.PlayClipAtPoint(levelPassed, Camera.main.transform.position);
    }
    public void PlayEnemyHit()
    {
        AudioSource.PlayClipAtPoint(enemyHit, Camera.main.transform.position,0.1f);
    }
}
