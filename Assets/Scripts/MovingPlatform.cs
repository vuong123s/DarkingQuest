using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] patrolPoints;// diem de monster di qua lai tuan tra
    public float speed;
    int currentPointIndex = 1;

    float waitTime;
    public float startWaitTime;//thoi gian doi khi den 1 patrol point

    // Start is called before the first frame update
    void Start()
    {
        //vi tri bat dau la diem tuan tra(patrol point) dau tien
        transform.position = patrolPoints[0].position;
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        // di chuyen tu vi tri hien tai den diem patrol point tiep theo
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

        // neu monster da di den vi tri cua diem patrol thi tang currentPointIndex
        if (transform.position == patrolPoints[currentPointIndex].position)
        {
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }

}
