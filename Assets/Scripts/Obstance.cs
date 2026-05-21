using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstance : MonoBehaviour
{
	private Rigidbody2D obs;
	public bool checkCeil;
	public int damage;
	public GameObject blood;//Tao mau
							// Start is called before the first frame update
	void Start()
	{
		obs = gameObject.GetComponent<Rigidbody2D>();//anhh x
	}

	// Update is called once per frame
	void Update()
	{
		float moveY = 0f;//di chuyen truc y
		if (checkCeil)
		{
			moveY = -5f;

		}
		else
		{
			moveY = 5f;
		}
		obs.linearVelocity = new Vector2(0, transform.localScale.y) * moveY;//di chuyen x du nguyen , y di chuyen

	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Cell_bottom")
		{
			checkCeil = false;
		}
		if (collision.gameObject.tag == "Cell_top")
		{

			checkCeil = true;
		}
	}
	//private void OnTriggerEnter2D(Collider2D collision)
	//{
	//	neu fireball cham vao player thi player se nhan sat thuong
	//	if (collision.tag == "Player")
	//	{
	//		collision.GetComponent<Player>().TakeDamage(damage);
	//		Instantiate(blood, transform.position, Quaternion.identity);
	//	}
	//	Destroy(gameObject);
	//}

}
