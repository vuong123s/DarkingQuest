using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    float speed;
    public float minSpeed;
    public float maxSpeed;
    public float minX;
    public float maxX;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // cloud di chuyen sang phai
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // neu x cua cloud > maxX thi cho cloud ve vi tri minX 
        if(transform.position.x > maxX) {
            Vector2 newPosition = new Vector2(minX, transform.position.y);
            transform.position = newPosition;
        }
    }
}
