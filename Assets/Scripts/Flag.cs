using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Co chuyen level va hien You Win o man cuoi.
public class Flag : MonoBehaviour
{
    public string sceneName;
    public GameObject youWinPanel;
    public void Awake()
    {
        // Luu trang thai mo khoa level hien tai.
        //Dat gia tri trong PlayerPrefs voi khoa la ten cua canh hien tai đang chay va gia tri la 1.
        //danh dau 1 cap do da duoc mo khoa
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
    }
    private void OnTriggerEnter2D(Collider2D other)//kich hoat khi player cham vao la co chuyen level 2
    {
        // Neu la level cuoi thi hien You Win, nguoc lai chuyen scene.
        if (other.CompareTag("Player") && SceneManager.GetActiveScene().name == "Level3")
        {
            youWinPanel.SetActive(true);
        }
        else if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
        //PlayerPrefs.DeleteAll();//lam sach du lieu khi chuyen qua level moi

    }
}