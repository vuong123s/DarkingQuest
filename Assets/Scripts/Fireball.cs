using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        // destroy fireball sau khoang thoi gian lifetime
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
		//transform.Translate(Vector2.right * speed * Time.deltaTime);
		transform.Translate(Vector2.left * speed * Time.deltaTime);

	}

    // khi cham vao collider2d nao do thi se thuc thi
	private void OnTriggerEnter2D(Collider2D collision)
	{
        // neu fireball cham vao player thi player se nhan sat thuong
		if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }

        Destroy(gameObject);
	}
}
