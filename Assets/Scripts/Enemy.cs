using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Enemy : MonoBehaviour
{
    public int damage;
    public int health;
	public GameObject blood;
	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
	}

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
		{
			Instantiate(blood, transform.position, Quaternion.identity);
			Destroy(gameObject);
        }
		Instantiate(blood, transform.position, Quaternion.identity);
	}
}
