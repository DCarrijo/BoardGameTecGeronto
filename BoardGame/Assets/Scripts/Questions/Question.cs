using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;

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

    public Question(string question, string rightAnswer, string[] wrongAnswer, int questionId, Categories questionCategory)
    {
        this.QuestionText = question;
        this.RightAnswer = rightAnswer;
        this.WrongAnswer = wrongAnswer;
        this.QuestionId = questionId;
        this.QuestionCategory = questionCategory;
    }
}
