using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void Play(string _scene)
	{
		SceneManager.LoadSceneAsync(_scene);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
