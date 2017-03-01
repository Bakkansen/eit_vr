using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;


public class UIManager : MonoBehaviour {
    [SerializeField] private Reticle m_Reticle;                         // The scene only uses SelectionSliders so the reticle should be shown.
    [SerializeField] private SelectionRadial m_Radial;                  // Likewise, since only SelectionSliders are used, the radial should be hidden.
    [SerializeField] private SelectionSlider answer1, answer2, answer3, answer4;
    [SerializeField] private UIFader fader;
    [SerializeField] private Text title;
    [SerializeField] private Text quizText;    

    private SelectionSlider selectedAnswer = null;
    private string titleText = "Hvor mange kubikkdesimeter tilsvarer 1 kubikkmeter?";
    private string question = "";
    private string[] answers = { "1000 dm^3", "10000 dm^3", "10 dm^3", "100 dm3^" };

    // Use this for initialization
    private IEnumerator Start() {
        m_Reticle.Show();
        m_Radial.Hide();
        SetUpQuiz(answers, question, titleText);
        yield return StartCoroutine(fader.InteruptAndFadeIn());
    }    

    public void SetSelectedAnswer(SelectionSlider answer) {        
        selectedAnswer = answer;        
    }

    // The first entry in the answers-array is always the correct one.
    private void SetUpQuiz(string[] answers, string q, string t) {
        title.text = t;
        quizText.text = q;
        answer1.isCorrectAnswer = true;
        answer1.SetText(answers[0]);
        answer2.SetText(answers[1]);
        answer3.SetText(answers[2]);
        answer4.SetText(answers[3]);
    }
}
