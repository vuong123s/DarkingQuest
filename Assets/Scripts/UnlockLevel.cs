using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class UnlockLevel : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject chains;
    [SerializeField] private string sceneName;
    void Start()
    {
        button = GetComponent<Button>();
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError($"{name}: sceneName is empty in UnlockLevel.");
            return;
        }

        if (PlayerPrefs.GetInt(sceneName,0)==1)//neu k co gia tri mac dinh bang va neu bang 1 thi mo khoa level
        {
            button.interactable = true;//tuong tac duoc voi button
            if (chains != null)
            {
                chains.SetActive(false);// an day xich
            }
            else
            {
                Debug.LogWarning($"{name}: chains is not assigned in UnlockLevel.");
            }
        }    
    }
}
