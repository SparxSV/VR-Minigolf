using Managers;

using System;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI currentStats;

    private CourseManager courseManager;
    
    private DateTime time;

    private void Awake()
    {
        try
        {
            courseManager = FindObjectOfType<CourseManager>();
        }
        catch(MissingFieldException ex)
        {
            Debug.LogException(ex);
        }
    }

    private void Start()
    {
        gameObject.SetActive(gameObject.scene.name != "Main Lobby");
    }

    private void Update()
    {
        time = DateTime.Now;
        timer.text = $"{time}";

        currentStats.text = $"{courseManager.CurrentHole}/{courseManager.CurrentPar}";
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("Main Lobby");
    }
}
