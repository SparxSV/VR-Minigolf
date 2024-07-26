using UnityEngine;

namespace UI
{
	public class SettingsUI : MonoBehaviour
	{
		public void BackToMenu(GameObject _menu)
		{
			gameObject.SetActive(false);
			_menu.SetActive(true);
		}
	}
}