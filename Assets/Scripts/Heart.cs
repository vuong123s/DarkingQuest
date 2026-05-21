using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Cap nhat UI trai tim theo mau player.
public class Heart : MonoBehaviour
{
    public int numberOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite brokenHeart;
    Player player;

    private void Start()
    {
		// Tim Player de doc mau.
        player = FindObjectOfType<Player>();    
    }

    private void Update()
    {
		// Gioi han mau toi da theo numberOfHearts.
        if(player.health > numberOfHearts)
        {
            player.health = numberOfHearts;
        }
        for(int i = 0; i < hearts.Length; i++)
        {
            // Bat/tat icon neu vuot so luong toi da.
            if(i < numberOfHearts)
            {
                hearts[i].enabled = true;
            } else
            {
                hearts[i].enabled = false;
            }

            // Doi sprite theo mau con lai.
            if(i<player.health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = brokenHeart;
            }
        }
    }

}
