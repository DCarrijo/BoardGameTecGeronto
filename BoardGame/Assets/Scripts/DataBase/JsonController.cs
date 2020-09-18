using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.iOS;

public class JsonController : MonoBehaviour
{
    private readonly string _url = "http://jogo.tecgeronto.com.br/questoes";
    
    void Start()
    {
        StartCoroutine(GetQuestions());
    }

    private IEnumerator GetQuestions()
    {
        UnityWebRequest questionsRequest = UnityWebRequest.Get(_url);

        yield return questionsRequest.SendWebRequest();

        if (questionsRequest.isNetworkError || questionsRequest.isHttpError)
        {
            Debug.LogError(questionsRequest.error);
            yield break;
        }

        JSONNode questions = JSONNode.Parse(questionsRequest.downloadHandler.text);

        Debug.Log(questions);
        Debug.Log(questions.Count);

        foreach (var node in questions.AsArray)
        {
            var question = GetQuestionFromJson(node);
            Debug.Log(question);
        }
    }

    private Question GetQuestionFromJson(JSONNode node)
    {
        return new Question(node["pergunta"].ToString(), 
            node["respostaCerta"].ToString(), 
            new []{node["respostaErrada1"].ToString(),node["respostaErrada2"].ToString(),node["respostaErrada3"].ToString()}, 
            node["tema"].ToString(),
            node["id"].AsInt);
    }
}
