using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public float timeLeft = 60.0f;
    public TMP_Text timerText;
    public TMP_Text failText;
    public GameObject failPanel;
    public GameObject pausePanel;
    public GameObject stopButton; 

    void Start()
    {
        if (failText != null)
        {
            failText.gameObject.SetActive(false);
        }
        if (failPanel != null)
        {
            failPanel.SetActive(false);
        }
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        // 确保游戏开始时 STOP 按钮显示
        if (stopButton != null)
        {
            stopButton.SetActive(true);
        }
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            
            float displayTime = Mathf.Max(timeLeft, 0);
            
            if (timerText != null)
            {
                timerText.text = "Remaining game time: " + Mathf.Ceil(displayTime).ToString();
            }
        }
        else
        {
            if (timerText != null)
            {
                timerText.text = "Remaining game time: 0";
            }
            
            if (failPanel != null)
            {
                failPanel.SetActive(true);
            }
            
            if (failText != null)
            {
                failText.gameObject.SetActive(true);
                failText.text = "You failed, try again.";
                failText.transform.SetAsLastSibling();
            }
            
            if (stopButton != null)
            {
                stopButton.SetActive(false);
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        Debug.Log("PauseGame called");
        Time.timeScale = 0;
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
            pausePanel.transform.SetAsLastSibling(); 
            Debug.Log("pausePanel activated and set as last sibling");
        }
        else
        {
            Debug.Log("pausePanel is null! Please assign pausePanel in the Inspector.");
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }
}