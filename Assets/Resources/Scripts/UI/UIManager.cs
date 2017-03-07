using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;


public class UIManager : MonoBehaviour {
    class QuizEntry {
        public string quizText { get; set; }
        public string[] answers { get; set; }
    }

    Dictionary<int, QuizEntry> m_quizQuestions = new Dictionary<int, QuizEntry>() {
        {1, new QuizEntry { quizText = "Hvor mange kubikkdesimeter tilsvarer 1 kubikkmeter?", answers = new string[]{ "1000 dm^3", "10 000 dm^3", "10 dm^3", "100 dm^3" }}},
        {2, new QuizEntry { quizText = "Hvor mange kubikkcentimeter tilsvarer 1 kubikkmeter?", answers = new string[]{ "1 000 000 cm^3", "100  cm^3", "1000 cm^3", "100 000 000 cm^3" }}},
        {3, new QuizEntry { quizText = "Hvor mange liter tilsvarer 1 kubikkmeter?", answers = new string[]{ "1000 L", "100 L", "1 000 000 L", "10 L" }}}
    };

    [SerializeField] private Reticle m_Reticle;                         // The scene only uses SelectionSliders so the reticle should be shown.
    [SerializeField] private SelectionRadial m_Radial;                  // Likewise, since only SelectionSliders are used, the radial should be hidden.
    [SerializeField] private SelectionSlider answer1, answer2, answer3, answer4, nextQ;
    [SerializeField] private UIFader fader;
    [SerializeField] private UIFader nextQFader;
    [SerializeField] private Text title;   
    [SerializeField] private int m_currentQuestion = 1;

    private SelectionSlider selectedAnswer = null;
    List<SelectionSlider> m_sliders;
    List<int> counter = new List<int>() { 0, 1, 2, 3 };


    // Use this for initialization
    private IEnumerator Start() {
        m_Reticle = Camera.main.GetComponent<Reticle>();
        m_Radial = Camera.main.GetComponent<SelectionRadial>();
        m_sliders = new List<SelectionSlider>() { answer1, answer2, answer3, answer4 };
        m_Reticle.Show();
        m_Radial.Hide();
        LoadNextQuestion();
        yield return StartCoroutine(fader.InteruptAndFadeIn());
    }    

    public void SetSelectedAnswer(SelectionSlider answer) {        
        selectedAnswer = answer;        
    }

    // The first entry in the answers-array is always the correct one.
    public void SetUpQuiz(string[] answers, string t) {
        title.text = t;
        counter.Shuffle();        
        for (int i = 0; i < m_sliders.Count; i++) {
            Debug.Log(counter[i]);
            if (counter[i] == 0) {
                m_sliders[i].m_isCorrectAnswer = true;
            }
            m_sliders[i].SetText(answers[counter[i]]);
        }
        nextQ.m_isNextQuestButton = true;
    }

    public void LoadNextQuestion() {
        if (m_currentQuestion > 3)
            return;
        foreach (SelectionSlider slider in m_sliders) {
            slider.resetFillColor();
            slider.m_isCorrectAnswer = false;
        }
        SetUpQuiz(m_quizQuestions[m_currentQuestion].answers, m_quizQuestions[m_currentQuestion].quizText);
        m_currentQuestion++;
    }

    public void FadeInNextButton() {
        StartCoroutine(nextQFader.InteruptAndFadeIn());
    }

    public void FadeOutNextButton() {
        StartCoroutine(nextQFader.InteruptAndFadeOut());
    }
}
