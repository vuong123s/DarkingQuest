using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Dieu khien menu, fade va reset tien trinh.
public class Menu : MonoBehaviour
{
	public GameObject panel;
	public GameObject settingCanvas;
	public void LoadScene(string sceneName)
    {
		// Chuyen scene co hieu ung fade.
		StartCoroutine(FadeIn(sceneName));
    }

	public void Quit()
	{
		//thoat khoi game
		Application.Quit();
	}
	IEnumerator FadeIn(string sceneName)
	{
		// Bat panel va doi mot chut truoc khi load.
		panel.SetActive(true);
		yield return new WaitForSeconds(0.5f);//yied tra ve trong vai giay. xem canh Mantia Dot Lourdes truoc khi bat dau canh Game
		SceneManager.LoadScene(sceneName);//tai mot scene moi trong game
	}

	public void ShowPanel(GameObject panel)
	{
		// Mo canvas setting va panel con.
        settingCanvas.SetActive(true);
		panel.SetActive(true);
    }

	public void ClosePanel(GameObject panel)
	{
		// Dong canvas setting va panel con.
		settingCanvas.SetActive(false);
		panel.SetActive(false);
	}

	public void ResetLevel()
	{
		// Xoa tien trinh luu.
		PlayerPrefs.DeleteAll();
	}

}
