using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsLoader : MonoBehaviour
{
    private void Awake()
    {
        LoadQuestions();
    }
    
    public void LoadQuestions()
    {
        QuestionHash.CreateHash();
        
        Question[] question = QuestionSaver.LoadQuestion();

        if (question != null)
        {
            QuestionHash.Add(question);
        }
    }
}