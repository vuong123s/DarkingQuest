using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

// Lop enemy co ban: sat thuong va mau.
public class Enemy : MonoBehaviour
{
    public int damage;
    public int health;
	public GameObject blood;
	private void OnTriggerEnter2D(Collider2D collision)
    {
		// Gay sat thuong khi cham Player.
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
	}

    public void TakeDamage(int damage)
    {
		// Tru mau va kiem tra chet.
        health -= damage;
        if (health < 0)
		{
			Instantiate(blood, transform.position, Quaternion.identity);
			Destroy(gameObject);
        }
		Instantiate(blood, transform.position, Quaternion.identity);
	}
}
