using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
            [SerializeField] private Button startButton;
            [SerializeField] private RectTransform startDialog;
            [SerializeField] private TextMeshProUGUI scoreText;
            [SerializeField] private TextMeshProUGUI finalScoreText;
            [SerializeField] private TextMeshProUGUI lastScoreText;
            [SerializeField] private Button restartButton;
            [SerializeField] private RectTransform nextDialog;
            [SerializeField] private RectTransform endDialog;
            [SerializeField] private Button quitButton;
            [SerializeField] private Button nextButton;
            
            public static UIManager Instance { get; private set; }
            
            private void Awake()
            {
                /*Debug.Assert(startButton != null, "startButton cannot be null");
                Debug.Assert(startDialog != null, "startDialog cannot be null");        
                Debug.Assert(scoreText != null, "scoreText cannot null");
                Debug.Assert(finalScoreText != null, "finalScoreText cannot null");
                Debug.Assert(restartButton != null, "restartButton cannot be null");
                Debug.Assert(endDialog != null, "endDialog cannot be null");  
*/                
                /*if (Instance == null)
                {
                    Instance = this;
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Destroy(gameObject);                
                }

                ShowStartDialog(false);
                ShowScore(true);
                GameManager.Instance.OnRestarted += RestartUI;
                ScoreManager.Instance.OnScoreUpdated += UpdateScoreUI;
                UpdateScoreUI();
                GameManager.Instance.StartGame();*/
            
                //startButton.onClick.AddListener(OnStartButtonClicked);
                //quitButton.onClick.AddListener(OnQuitButton);
                /*restartButton.onClick.AddListener(OnRestartButtonClicked);
                nextButton.onClick.AddListener(OnNextlevelButtonClicked);*/
            }
            
            //private void OnQuitButton()
            //{
                //Application.Quit();
            //}
    
            private void Start()
            {
                /*ShowStartDialog(false);
                ShowScore(true);
                GameManager.Instance.OnRestarted += RestartUI;
                ScoreManager.Instance.OnScoreUpdated += UpdateScoreUI;
                UpdateScoreUI();
                GameManager.Instance.StartGame();*/

                if (Instance == null)
                {
                    Instance = this;
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Destroy(gameObject);                
                }

                ShowStartDialog(false);
                ShowScore(true);
                GameManager.Instance.OnRestarted += RestartUI;
                ScoreManager.Instance.OnScoreUpdated += UpdateScoreUI;
                UpdateScoreUI();
                GameManager.Instance.StartGame();

                restartButton.onClick.AddListener(OnRestartButtonClicked);
                nextButton.onClick.AddListener(OnNextlevelButtonClicked);

                ShowEndDialog(false);
                ShowScore(false);
            }
    
            /*private void OnStartButtonClicked()
            {
                ShowStartDialog(false);
                ShowScore(true);
                GameManager.Instance.OnRestarted += RestartUI;
                ScoreManager.Instance.OnScoreUpdated += UpdateScoreUI;
                UpdateScoreUI();
                GameManager.Instance.StartGame();
            }
    */
            public void OnEndDialog()
            {
               endDialog.gameObject.SetActive(true);
            }
            
            public void OnNextlevel()
            {
                nextDialog.gameObject.SetActive(true);
            }
            
            public void OnNextlevelButtonClicked()
            {
                nextDialog.gameObject.SetActive(false);
                GameManager.Instance.NextLevelGame();
            }
        
            private void OnRestartButtonClicked()
            {
            /*ShowEndDialog(false);
            UpdateScoreUI();
            ShowScore(true);
            GameManager.Instance.StartGame();*/
            //startDialog.gameObject.SetActive(true);
            endDialog.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            OnDestroy();

            Start();

            //SceneManager.LoadScene("UI_main");

            } 
        
            private void UpdateScoreUI()
            {
                scoreText.text = $"Score : {ScoreManager.Instance.GetScore()}";
                lastScoreText.text = $"Player Score : {ScoreManager.Instance.GetScore()}";
                finalScoreText.text = $"Player Score : {ScoreManager.Instance.GetScore()}";
            }
    
            private void RestartUI()
            {
                ShowScore(false);
                ShowEndDialog(true);
            }
        
            public void ShowScore(bool showScore)
            {
                scoreText.gameObject.SetActive(showScore);
            }
    
            private void ShowStartDialog(bool showDialog)
            {
                startDialog.gameObject.SetActive(showDialog);
            }
    
            private void ShowEndDialog(bool showDialog)
            {
                endDialog.gameObject.SetActive(showDialog);
            }
    
            private void OnDestroy()
            {
                GameManager.Instance.OnRestarted -= RestartUI;
                ScoreManager.Instance.ResetScore();
                ScoreManager.Instance.OnScoreUpdated -= UpdateScoreUI;
            }
    }

}
