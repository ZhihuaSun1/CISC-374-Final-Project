using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizController : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;         
        public string[] options = new string[4];  
        public int correctOptionIndex;        
    }

    [Header("Quiz Data")]
    public Question[] questions;       

    [Header("UI References")]
    public TMP_Text questionTextUI;             
    public Button[] optionButtons;          
    public GameObject quizPanel;            
    public GameObject failPanel;      
    public GameObject completePanel; 
    public GameObject stopButton;

    private int currentQuestionIndex = 0;

    void Start()
    {
        
        if (failPanel != null)
        {
            failPanel.SetActive(false);
        }
        if (completePanel != null)
        {
            completePanel.SetActive(false);
        }
        if (stopButton != null)
        {
            stopButton.SetActive(true);
        }
        if (questions.Length > 0)
        {
            ShowQuestion();
        }
    }
    
    void ShowQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            Question q = questions[currentQuestionIndex];
            questionTextUI.text = q.questionText;
            for (int i = 0; i < optionButtons.Length; i++)
            {
                TMP_Text btnText = optionButtons[i].GetComponentInChildren<TMP_Text>();
                if (btnText != null && i < q.options.Length)
                {
                    btnText.text = q.options[i];
                }
            }
        }
        else
        {
            Debug.Log("Quiz completed successfully!");
            Time.timeScale = 0; 
            if (quizPanel != null)
            {
                quizPanel.SetActive(false);
            }
            if (completePanel != null)
            {
                completePanel.SetActive(true);
            }
            if (stopButton != null)
            {
                stopButton.SetActive(false);
            }
        }
    }

    public void OnOptionSelected(int optionIndex)
    {
        foreach (var btn in optionButtons)
        {
            btn.interactable = false;
        }

        if (currentQuestionIndex < questions.Length)
        {
            Question q = questions[currentQuestionIndex];
            if (optionIndex == q.correctOptionIndex)
            {
                Debug.Log("Correct answer!");
                currentQuestionIndex++;

                if (currentQuestionIndex >= questions.Length)
                {
                    Debug.Log("Quiz completed successfully!");
                    Time.timeScale = 0; 
                    if (quizPanel != null)
                    {
                        quizPanel.SetActive(false);
                    }
                    if (completePanel != null)
                    {
                        completePanel.SetActive(true);
                    }
                    if (stopButton != null)
                    {
                        stopButton.SetActive(false);
                    }
                }
                else
                {
                    ShowQuestion();
                    foreach (var btn in optionButtons)
                    {
                        btn.interactable = true;
                    }
                }
            }
            else
            {
                Debug.Log("Wrong answer!");
                Time.timeScale = 0; 
                if (failPanel != null)
                {
                    failPanel.SetActive(true);
                    TMP_Text[] texts = failPanel.GetComponentsInChildren<TMP_Text>(true);
                    foreach(var t in texts)
                    {
                        t.gameObject.SetActive(true);
                    }
                }
                if (stopButton != null)
                {
                    stopButton.SetActive(false);
                }
            }
        }
    }
}