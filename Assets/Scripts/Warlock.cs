using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy ban dan theo chu ky.
public class Warlock : Enemy
{
    public GameObject fireBall;
    public float timeBetweenShots;
    float nextShotTime;
    public Transform shotPoint; // xac dinh vi tri ban fireball

    // Update is called once per frame
    void Update()
    {
        // khi da toi thoi gian ban fireball
        if(Time.time>nextShotTime)
        {
            // Tao dan tai shotPoint, huong theo rotation cua warlock.
            Instantiate(fireBall,shotPoint.position,transform.rotation); // tao ban sao cua fireball o vi tri shotpoint va huong cua warlock
            nextShotTime = Time.time + timeBetweenShots; // tinh thoi gian ban tiep theo
        }
    }
}
