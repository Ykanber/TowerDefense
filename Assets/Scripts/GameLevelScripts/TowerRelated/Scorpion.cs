using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scorpion : MonoBehaviour
{

    Animator animator;

    Rigidbody2D rb2d;

    public LayerMask enemy;

    GameObject enemyToHit;

    [SerializeField] GameObject shootingPoint;

    Quaternion rotation;

    //Range
    [SerializeField] int range;
    public int rangeMax = 4;
    [SerializeField] GameObject rangeImage;
    int increaseRangeLevel;
    [SerializeField] Sprite[] towerArray;
    [SerializeField] GameObject tower;

    // Damage
    [SerializeField] Color[] colorArr; 
    int damageLevel;
    [SerializeField] int maxDamageLv;
    [SerializeField] int damage;
    [SerializeField] GameObject [] arrows;

    // UI
    [SerializeField] GameObject TowerUI;

    public int Range { get => range; }
    public int Damage { get => damage; }
    public int DamageLevel { get => damageLevel;}
    public int MaxDamageLv { get => maxDamageLv;}

    AudioManager audioManager;
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.death) {
            RaycastHit2D []hits = Physics2D.CircleCastAll(transform.position, Range, Vector2.zero, Mathf.Infinity, enemy); // find enemies in range

            if (hits.Length != 0)
            {
                if (enemyToHit == null || !Array.Exists(hits,element => element.collider.gameObject == enemyToHit)) // if target is dead orr out of range
                {
                    animator.SetBool("isShooting", true);
                    float closestRange = float.MaxValue;
                    int closestRangeIndex = -1;
                    for (int i = 0; i < hits.Length; i++)
                    {
                        float distance = Vector2.Distance(transform.position, hits[i].collider.transform.position);
                        if (distance < closestRange)
                        {
                            closestRange = distance;
                            closestRangeIndex = i;
                        }
                    }
                    enemyToHit = hits[closestRangeIndex].collider.gameObject;
                }
                Vector3 direction = enemyToHit.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb2d.rotation = angle - 90;
                rotation = transform.rotation; // Scorpion rotation Handling
            }
            else
            {
                animator.SetBool("isShooting", false);
                enemyToHit = null;
            }
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
    }

    public void Shoot() // works  in animation
    {
        if (enemyToHit != null) {
            Arrow instantiatedArrow = Instantiate(arrows[DamageLevel], shootingPoint.transform.position, rotation).GetComponent<Arrow>();
            instantiatedArrow.SetGoal(enemyToHit);
            instantiatedArrow.damage = damage;
        } 
    }



    public void BuyDamage()
    {
        if (GoldManager.GetGoldCount() >= 50 && damageLevel != maxDamageLv)
        {
            audioManager.PlayAddDamage();
            GoldManager.DecreaseGold(50);
            IncreaseDamage();
        }
    }
    public void IncreaseDamage() // Changing animator to change scorpion sprite
    {
        damage += 20;
        damageLevel += 1;
        animator.SetInteger("lv", damageLevel);
        rangeImage.GetComponent<SpriteRenderer>().color = colorArr[damageLevel]; // change range display color
    }

    public void BuyRange()
    {
        if (GoldManager.GetGoldCount() >= 50 && range != rangeMax)
        {
            audioManager.PlayAddRange(); 
            GoldManager.DecreaseGold(50);
            IncreaseRange();
        }
    }

    public void IncreaseRange()
    {
        range += 1;
        increaseRangeLevel+=1;
        SetRangeImage();
    }

    void SetRangeImage() // increase range and change tower look
    {
        rangeImage.transform.localScale *= ((float)Range / ((float)Range - 1));
        tower.GetComponent<SpriteRenderer>().sprite = towerArray[increaseRangeLevel];
    }

    private void OnMouseDown()
    {
        TowerUI.SetActive(!TowerUI.activeSelf);
    }
}
