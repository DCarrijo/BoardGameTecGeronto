using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public static class QuestionHash
{
    private static Dictionary<Categories, List<Question>> Hash;

    public static Dictionary<Categories, Queue<Question>> GameQuestions { get; private set; }

    public static void CreateHash()
    {
        Hash = new Dictionary<Categories, List<Question>>();
        foreach (var cat in Enum.GetValues(typeof(Categories)).Cast<Categories>())
        {
            Hash.Add(cat, new List<Question>());
        }
    }

    public static void Add(Question[] questions)
    {
        if (Hash == null)
            CreateHash();

        foreach (var question in questions)
        {
            Hash[question.QuestionCategory].Add(question);
        }
    }

    public static void Add(Question question)
    {
        if (Hash == null)
            CreateHash();

        Hash[question.QuestionCategory].Add(question);
    }

    public static Question GetGameQuestion(Categories categorie)
    {
        if (GameQuestions == null)
            return null;

        Question aux = GameQuestions[categorie].Dequeue();
        GameQuestions[categorie].Enqueue(aux);
        
        return aux;
    }

    public static void GenerateGameQuestions()
    {
        GameQuestions = new Dictionary<Categories, Queue<Question>>();
        foreach (var cat in Enum.GetValues(typeof(Categories)).Cast<Categories>())
        {
            GameQuestions.Add(cat, new Queue<Question>());
            GameQuestions[cat] = GetRandomQuestionsQueue(Hash[cat]);
        }
    }

    private static Queue<Question> GetRandomQuestionsQueue(List<Question> questions)
    {
        Queue<Question> questionsQueue = new Queue<Question>();
        int index;
        while (questions.Count > 0)
        {
            index = Random.Range(0, questions.Count);
            questionsQueue.Enqueue(questions[index]);
            questions.RemoveAt(index);
        }

        return questionsQueue;
    }

    public static Question[] GetArray()
    {
        List<Question> questions = new List<Question>();

        foreach (var key in Hash.Keys)
        {
            foreach (var question in Hash[key])
            {
                questions.Add(question);
            }
        }

        return questions.ToArray();
    }

    public static string[] Log()
    {
        List<string> aux = new List<string>();
        foreach (var key in Hash.Keys)
        {
            foreach (var question in Hash[key])
            {
                aux.Add(question.ToString());
            }
        }

        return aux.ToArray();
    }
}
