using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public int turnSpeed;
	public int damage;
	public float attackRange;
	public Sprite graphic; // giu vu khi tren tay

	// Update is called once per frame
	void Update()
	{
		transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			collision.GetComponent<Player>().Equip(this);
		}
	}

	
}
