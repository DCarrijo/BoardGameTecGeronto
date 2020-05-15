using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;

[System.Serializable]
public enum Categories
{
    MOB = 0,    //mobilidade
    FIN,        //saude financeira
    SEG,        //seguranca
    SAU,        //auto cuidado/saude
    CON,        //convivio interpessoal
    EMO,        //equilibrio emocional
    SEX,        //sexualidade
    ALI,        //alimentacao saudavel
    EDU,        //educacao ao longo da vida
    CMV         //corpo em movimento
}

[System.Serializable]
public class Question
{
    public string QuestionText;
    public string RightAnswer;
    public string[] WrongAnswer;
    public int QuestionId;
    public Categories QuestionCategory;

    public Question(string question, string rightAnswer, string[] wrongAnswer, Categories questionCategory, int questionId)
    {
        this.QuestionText = question;
        this.RightAnswer = rightAnswer;
        this.WrongAnswer = wrongAnswer;
        this.QuestionCategory = questionCategory;
        this.QuestionId = questionId;
    }

    public override string ToString()
    {
        return "ID: " + QuestionId + "\n" +
               QuestionText + "\n" + RightAnswer + "\n" + WrongAnswer[0] + "\n" + WrongAnswer[1] + "\n" +
               WrongAnswer[2] + "\n" + QuestionCategory;
    }
}
