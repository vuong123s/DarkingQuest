using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnlockLevel : MonoBehaviour
{
    private Button button;
    public GameObject chains;
    public string sceneName;
    void Start()
    {
        button = GetComponent<Button>();
        if(PlayerPrefs.GetInt(sceneName,0)==1)//neu k co gia tri mac dinh bang va neu bang 1 thi mo khoa level
        {
            button.interactable = true;//tuong tac duoc voi button
            chains.SetActive(false);// an day xich
        }    
    }
}
