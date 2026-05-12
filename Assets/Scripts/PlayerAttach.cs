using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttach : MonoBehaviour
{
    public float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public Animator camAnim;
    public Animator playerAnim;
    public float attackRangeX;
    public float attackRangeY;
    public int damage;
    void Update()
    {
        if(timeBtwAttack<=0)
        {
            if(Input.GetKey(KeyCode.K))
            {
                camAnim.SetTrigger("shake");
                playerAnim.SetTrigger("attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY),0, whatIsEnemies);
                for(int i=0; i<enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }   
        
    }
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY,1));
	}
}
