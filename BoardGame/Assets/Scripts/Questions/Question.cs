using System;
using UnityEngine;

[System.Serializable]
public enum Categories
{
    NULL = -1,
    MOB = 0,    //mobilidade
    FIN,        //saude financeira
    SEG,        //seguranca
    SAU,        //auto cuidado/saude
    CON,        //convivio interpessoal
    EMO,        //equilibrio emocional
    SEX,        //sexualidade
    ALI,        //alimentacao saudavel
    EDU,        //educacao ao longo da vida
    CMV,        //corpo em movimento
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

    public Question(string question, string rightAnswer, string[] wrongAnswer, string questionCategoryString,
        int questionId)
    {
        Categories cat = GetCategorie(questionCategoryString);
        this.QuestionText = question;
        this.RightAnswer = rightAnswer;
        this.WrongAnswer = wrongAnswer;
        this.QuestionId = questionId;
        this.QuestionCategory = cat;
    }

    public static Categories GetCategorie(string categorie)
    {
        categorie.ToUpper();
        Categories aux = Categories.NULL;
        
        foreach (var cat in Enum.GetNames(typeof(Categories)))
        {
            if (cat == categorie)
            {
                aux = (Categories)Enum.Parse(typeof(Categories), cat);
            }
        }
        return aux;
    }

    public override string ToString()
    {
        return "ID: " + QuestionId + "\n" +
               QuestionText + "\n" + RightAnswer + "\n" + WrongAnswer[0] + "\n" + WrongAnswer[1] + "\n" +
               WrongAnswer[2] + "\n" + QuestionCategory;
    }
}
