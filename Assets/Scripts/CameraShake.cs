using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Goi trigger rung camera.
public class CameraShake : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
		// Luu animator de goi trigger.
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shake()
    {
        // Goi trigger rung camera.
        anim.SetTrigger("shake");
    }
}
