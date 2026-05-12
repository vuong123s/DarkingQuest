using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Enemy
{
    public Transform[] patrolPoints;// diem de monster di qua lai tuan tra
    public float speed;
    int currentPointIndex = 1;

    float waitTime;
    public float startWaitTime;//thoi gian doi khi den 1 patrol point

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //vi tri bat dau cua monster la diem tuan tra(patrol point) dau tien
        transform.position = patrolPoints[0].position;
        transform.rotation = patrolPoints[0].rotation;
        waitTime = startWaitTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // di chuyen tu vi tri hien tai den diem patrol point tiep theo
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

        // neu monster da di den vi tri cua diem patrol thi tang currentPointIndex
        if (transform.position == patrolPoints[currentPointIndex].position)
        {
            transform.rotation = patrolPoints[currentPointIndex].rotation;// rotation cua monster la rotation cua patrol point

            // khi den patrol point thi doi nen idle
            animator.SetBool("isRunning", false);

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
            animator.SetBool("isRunning", true); // tren duong di den patrol point thi running
        }
    }

}
