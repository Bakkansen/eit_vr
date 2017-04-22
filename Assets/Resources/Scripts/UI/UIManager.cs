using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRStandardAssets.Utils;


public class UIManager : MonoBehaviour {
    class QuizEntry {
        public string quizText { get; set; }
        public string[] answers { get; set; }
        public float[] scales { get; set; }
    }

    Dictionary<int, QuizEntry> m_quizQuestions = new Dictionary<int, QuizEntry>() {
        {1, new QuizEntry { quizText = "Hva blir volumet til steinblokken dersom vi gjør om fra kubikkmeter til kubikkdesimeter?", answers = new string[]{ "1000 dm^3", "10 000 dm^3", "10 dm^3", "100 dm^3" }, scales = new float[] { 1f, 10f, 0.01f, 0.1f }}},
        {2, new QuizEntry { quizText = "Hva blir volumet til steinblokken dersom vi skal gjøre om til kubikkcentimeter?", answers = new string[]{ "1 000 000 cm^3", "100  cm^3", "1000 cm^3", "100 000 000 cm^3" }, scales = new float[] { 1f, 0.0001f, 0.001f, 100f }}},
        {3, new QuizEntry { quizText = "Hvor mange liter støpemasse tror du egypterne måtte bruke for å støpe én steinblokk med et volum på én kubikkmeter?", answers = new string[]{ "1000 L", "100 L", "100 000 L", "10 L" }, scales = new float[] { 1f, 0.1f,  100f, 0.01f}}}
    };

    [SerializeField] private Reticle m_Reticle;                         // The scene only uses SelectionSliders so the reticle should be shown.
    [SerializeField] private SelectionRadial m_Radial;                  // Likewise, since only SelectionSliders are used, the radial should be hidden.
    [SerializeField] private SelectionSlider answer1, answer2, answer3, answer4, nextQ;
    [SerializeField] private UIFader fader;
    [SerializeField] private UIFader nextQFader;
    [SerializeField] private UIFader startFader;
    [SerializeField] private UIFader introFader;
    [SerializeField] private Text title;   
    [SerializeField] private AudioClip spm1;
    [SerializeField] private AudioClip spm2;
    [SerializeField] private AudioClip spm3;
    [SerializeField] private int m_currentQuestion = 1;
    [SerializeField] private GameObject m_cube;
    [SerializeField] private AudioSource m_Audio;
    [SerializeField] private AudioClip intro1;
    [SerializeField] private AudioClip intro2;
    [SerializeField] private bool skipIntro;

    private SelectionSlider selectedAnswer = null;
    List<SelectionSlider> m_sliders;
    List<int> counter = new List<int>() { 0, 1, 2, 3 };
    ResizeCube m_resize;


    // Use this for initialization
    private IEnumerator Start() {
        m_Reticle = Camera.main.GetComponent<Reticle>();
        m_Radial = Camera.main.GetComponent<SelectionRadial>();
        m_sliders = new List<SelectionSlider>() { answer1, answer2, answer3, answer4 };
        m_Reticle.Show();
        m_Radial.Hide();
        m_resize = m_cube.GetComponent<ResizeCube>();
        yield return StartCoroutine(fader.InteruptAndFadeOut());
        if (skipIntro)
        {
            StartCoroutine(fader.InteruptAndFadeIn());
            LoadNextQuestion();
        }
        else
        {
            yield return StartCoroutine(introFader.InteruptAndFadeIn());
        }
        // yield return StartCoroutine(fader.InteruptAndFadeIn());
    }    

    public void SetSelectedAnswer(SelectionSlider answer) {                
        selectedAnswer = answer;
        if (selectedAnswer.m_isIntroBar) {
            StartCoroutine(introFader.InteruptAndFadeOut());
            StartCoroutine(PlayIntro());
        } else if (selectedAnswer.m_isStartBar) {
            StartCoroutine(startFader.InteruptAndFadeOut());
            StartCoroutine(fader.InteruptAndFadeIn());
            LoadNextQuestion();
        } else {
            m_resize.SetScaleTo(selectedAnswer.GetAnswerScale());
        }
    }

    // The first entry in the answers-array is always the correct one.
    public void SetUpQuiz(string[] answers, float[] scales, string t) {
        title.text = t;
        counter.Shuffle();        
        for (int i = 0; i < m_sliders.Count; i++) {
            if (counter[i] == 0) {
                m_sliders[i].m_isCorrectAnswer = true;
            }
            m_sliders[i].SetText(answers[counter[i]]);
            m_sliders[i].SetAnswerScale(scales[counter[i]]);
        }
        nextQ.m_isNextQuestButton = true;
    }

    public void LoadNextQuestion() {
        if (m_currentQuestion > 3)
            return;
        if (m_currentQuestion == 1) {
            m_Audio.clip = spm1;
        } else if (m_currentQuestion == 2) {
            m_Audio.clip = spm2;
        } else {
            m_Audio.clip = spm3;
        }
        m_Audio.Play();
        foreach (SelectionSlider slider in m_sliders) {
            slider.resetFillColor();
            slider.m_isCorrectAnswer = false;
        }
        SetUpQuiz(m_quizQuestions[m_currentQuestion].answers, m_quizQuestions[m_currentQuestion].scales, m_quizQuestions[m_currentQuestion].quizText);
        m_currentQuestion++;
    }

    public void FadeInNextButton() {
        if (m_currentQuestion > 3) {
            Text t = nextQ.GetComponentInChildren<Text>();
            t.text = "Avslutt Quiz";
        }
        StartCoroutine(nextQFader.InteruptAndFadeIn());
    }

    public void FadeOutNextButton() {
        if (m_currentQuestion > 3) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        StartCoroutine(nextQFader.InteruptAndFadeOut());
    }

    IEnumerator PlayIntro() {
        
        m_Audio.clip = intro1;
        m_Audio.Play();
        yield return new WaitForSeconds(m_Audio.clip.length);
        
        m_Audio.clip = intro2;
        m_Audio.Play();
        yield return new WaitForSeconds(m_Audio.clip.length);

        StartCoroutine(startFader.InteruptAndFadeIn());
    }
}
