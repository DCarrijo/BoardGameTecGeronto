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
    [SerializeField] private TextMeshProUGUI _categorie;

    private int _rightAnswerIndex = -1;

    public Action<bool> OnAnswer;

    public bool CurrentResult { get; private set; } = false;

    public void StartQuestion(Question question)
    {
        SetupQuestion(question);
        this.gameObject.SetActive(true);
    }
    
    private void SetupQuestion(Question question)
    {
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
        }
    }

    public void CheckAnswer(int index)
    {
        OnAnswer?.Invoke(index == _rightAnswerIndex);
        CurrentResult = (index == _rightAnswerIndex);
        this.gameObject.SetActive(false);
    }
}
