using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
	public GameObject panel;
	public GameObject settingCanvas;
	public void LoadScene(string sceneName)
    {
		StartCoroutine(FadeIn(sceneName));
    }

	public void Quit()
	{
		//thoat khoi game
		Application.Quit();
	}
	IEnumerator FadeIn(string sceneName)
	{
		panel.SetActive(true);
		yield return new WaitForSeconds(0.5f);//yied tra ve trong vai giay. xem canh Mantia Dot Lourdes truoc khi bat dau canh Game
		SceneManager.LoadScene(sceneName);//tai mot scene moi trong game
	}

	public void ShowPanel(GameObject panel)
	{
        settingCanvas.SetActive(true);
		panel.SetActive(true);
    }

	public void ClosePanel(GameObject panel)
	{
		settingCanvas.SetActive(false);
		panel.SetActive(false);
	}

	public void ResetLevel()
	{
		PlayerPrefs.DeleteAll();
	}

}
