using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : Enemy
{
    public Transform attackPoint;
    GameObject player;
    Animator anim;
    public float timeBetweenAttack;
    float nextAttackTime;
    public float distanceToAttack;
    public float attackRange;

    public Transform[] patrolPoints;// diem de monster di qua lai tuan tra
    public float speed;
    int currentPointIndex = 1;
    float waitTime;
    public float startWaitTime;//thoi gian doi khi den 1 patrol point

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        //vi tri bat dau cua monster la diem tuan tra(patrol point) dau tien
        transform.position = patrolPoints[0].position;
        transform.rotation = patrolPoints[0].rotation;
    }


    // Update is called once per frame
    private void Update()
    {
        //if (player != null)
        //{
        //    float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        //    if (distanceToPlayer < distanceToAttack)
        //    {
        //        if (Time.time > nextAttackTime)
        //        {
        //            anim.SetTrigger("attack");
        //            if (distanceToPlayer < attackRange)
        //            {
        //                player.GetComponent<Player>().TakeDamage(damage);
        //            }
        //            nextAttackTime = Time.time + timeBetweenAttack;
        //        }
        //    }
        //}

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
            float angleToPlayer = Vector2.Dot(transform.right, directionToPlayer);

            if (distanceToPlayer < distanceToAttack && angleToPlayer < 0) // Player is within attack distance and in front of the monster
            {
                if (Time.time > nextAttackTime)
                {
                    anim.SetTrigger("attack");
                    if (distanceToPlayer < attackRange)
                    {
                        player.GetComponent<Player>().TakeDamage(damage);
                    }
                    nextAttackTime = Time.time + timeBetweenAttack;
                }
            }
        }

            // di chuyen tu vi tri hien tai den diem patrol point tiep theo
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

        // neu monster da di den vi tri cua diem patrol thi tang currentPointIndex
        if (transform.position == patrolPoints[currentPointIndex].position)
        {
            transform.rotation = patrolPoints[currentPointIndex].rotation;// rotation cua monster la rotation cua patrol point

            // khi den patrol point thi doi nen idle
            anim.SetBool("isRunning", false);

            // monster khi den patrol point thi phai cho 1 tg waitTime, neu waitTime >0 thi chowf, <0 thi tang currentPointIndex va reset waitTime
            if (waitTime <= 0)
            {
                if (currentPointIndex + 1 < patrolPoints.Length)
                {
                    currentPointIndex++;
                }
                else
                {
                    currentPointIndex = 0;
                }
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        else
        {
            anim.SetBool("isRunning", true); // tren duong di den patrol point thi running
        }
    }
}
