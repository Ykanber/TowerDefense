using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    // Health Bar
    [SerializeField]
    Image healthBar;


    // Health fields
    [SerializeField]
    int health;
    int currentHealth;

    // walk related fields
    [SerializeField]
    int walkSpeed;

    // Follow along the path
    int walkIndex = 0;

    // Gold Value
    [SerializeField] int goldValue;

    //Path
    Vector2[] pathArray;

    // Isdeath
    bool death;

    // Move Handling
    bool move = true;

    Vector3 oldPos;

    //move Animation
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = health;
        GetPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (move) {
            Vector3 targetPos = pathArray[walkIndex] + new Vector2(0.5f, 0.5f); // pivots are left bottom so add 0.5 on both axis
            oldPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, walkSpeed * Time.deltaTime);
            Vector3 change = transform.position - oldPos;

            animator.SetFloat("moveX", change.x);  
            animator.SetFloat("moveY", change.y);
            if (Vector2.Distance(transform.position, targetPos) < 0.01)
            {
                walkIndex += 1;
                if (walkIndex >= pathArray.Length)
                {
                    move = false;
                    walkIndex = 0;
                }
            } 
        }
    }

    void GetPath()
    {
        pathArray = SpawnManager.ReturnPath();
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        float fillAmount = ((float)currentHealth / (float)health >= 0) ? (float)currentHealth / (float)health : 0;
        healthBar.fillAmount = fillAmount;
        if(gameObject != null && currentHealth <= 0 && !death)
        {
            death = true;
            GoldManager.IncreaseGold(goldValue);
            SpawnManager.IncreaseDeadMonsterCount();
            Destroy(gameObject);
        }
    }
}
