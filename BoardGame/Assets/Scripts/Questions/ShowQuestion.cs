using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShowQuestion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _question;
    [SerializeField] private TextMeshProUGUI[] _answers;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private TextMeshProUGUI _categorie;

    private int _rightAnswerIndex = -1;

    public Action<bool> OnAnswer;

    public bool CurrentResult { get; private set; } = false;

    public void StartQuestion(Question question, bool hasPowerUp = false)
    {
        SetupQuestion(question, hasPowerUp);
        this.gameObject.SetActive(true);
    }
    
    private void SetupQuestion(Question question, bool hasPowerUp)
    {
        bool hasUsedPowerUp = false;

        foreach (var button in _buttons)
        {
            button.interactable = true;
        }
        
        _question.text = question.QuestionText;
        _categorie.text = question.QuestionCategory.ToString();

        _rightAnswerIndex = Random.Range(0, 4);
        _answers[_rightAnswerIndex].text = question.RightAnswer;
        
        List<String> wrongAnswers = new List<string>(question.WrongAnswer);

        for (int i = 0; i < 4; i++)
        {
            if (i == _rightAnswerIndex) continue;

            int aux = Random.Range(0, wrongAnswers.Count);
            _answers[i].text = wrongAnswers[aux];
            wrongAnswers.RemoveAt(aux);
            
            if (hasPowerUp && !hasUsedPowerUp)
            {
                if (Random.Range(0.0f, 1.0f) > 0.3f)
                {
                    hasUsedPowerUp = true;
                    _buttons[aux].interactable = false;
                }
            }
        }
        
        
    }

    public void CheckAnswer(int index)
    {
        OnAnswer?.Invoke(index == _rightAnswerIndex);
        CurrentResult = (index == _rightAnswerIndex);
        this.gameObject.SetActive(false);
    }
}
