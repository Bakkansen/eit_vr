using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;


public class UIManager : MonoBehaviour {
    [SerializeField] private Reticle m_Reticle;                         // The scene only uses SelectionSliders so the reticle should be shown.
    [SerializeField] private SelectionRadial m_Radial;                  // Likewise, since only SelectionSliders are used, the radial should be hidden.
    [SerializeField] private SelectionSlider answer1;
    [SerializeField] private SelectionSlider answer2;
    [SerializeField] private UIFader fader;
    [SerializeField] private Text title;
    [SerializeField] private Text quizText;    

    private SelectionSlider selectedAnswer = null;
    private string titleText = "Måleenheter for volum";
    private string question = "Hva tror du volumet til en steinblokk blir dersom vi konverterer fra kubikkmeter til kubikkdesimeter?";
    private string[] answers = { "1000", "1" };

    // Use this for initialization
    private IEnumerator Start() {
        m_Reticle.Show();
        m_Radial.Hide();
        SetUpQuiz(answers, question, titleText);
        yield return StartCoroutine(fader.InteruptAndFadeIn());
    }    

    public void SetSelectedAnswer(SelectionSlider answer) {        
        selectedAnswer = answer;
        Debug.Log("Selected: " + selectedAnswer.name);
        StartCoroutine(fader.InteruptAndFadeOut());
    }

    // The first entry in the answers-array is always the correct one.
    private void SetUpQuiz(string[] answers, string q, string t) {
        title.text = t;
        quizText.text = q;
        answer1.isCorrectAnswer = true;
        answer1.SetText(answers[0]);
        answer2.SetText(answers[1]);
    }
}
