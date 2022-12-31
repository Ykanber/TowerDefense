using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage;

    GameObject goalObject;
    void Update()
    {
        if (goalObject != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, goalObject.transform.position, Time.deltaTime * 10); // move arrow
        }
        else 
        {
            Destroy(gameObject); // if enemy dies while arrow is moving
        }
    }


    public void SetGoal(GameObject enemy)
    {
        goalObject = enemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<AudioManager>().PlayEnemyHit();
        collision.GetComponent<Monster>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
