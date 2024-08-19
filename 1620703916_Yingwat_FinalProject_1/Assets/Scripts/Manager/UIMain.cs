using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    public static UIMain Instance { get; private set; }

    private void Awake()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        quitButton.onClick.AddListener(OnQuitButton);
    }

    private void OnQuitButton()
    {
        Application.Quit();
    }

    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Game");
    }
}
