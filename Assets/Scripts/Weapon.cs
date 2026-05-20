using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Vat pham vu khi quay va trang bi khi cham Player.
public class Weapon : MonoBehaviour
{
	public int turnSpeed;
	public int damage;
	public float attackRange;
	public Sprite graphic; // giu vu khi tren tay

	// Update is called once per frame
	void Update()
	{
		// Quay vat pham de tao hieu ung nhat do.
		transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Khi Player cham vao thi trang bi vu khi.
		if (collision.tag == "Player")
		{
			collision.GetComponent<Player>().Equip(this);
		}
	}

	
}
