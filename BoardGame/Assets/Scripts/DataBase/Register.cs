using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
    public TMP_InputField Pergunta;
    public TMP_InputField RespostaCerta;
    public TMP_InputField RespostaErrada1;
    public TMP_InputField RespostaErrada2;
    public TMP_InputField RespostaErrada3;

    public TMP_Dropdown Categoria;

    public TextMeshProUGUI CurrentIdText;

    public Button submitButton;

    private void Update()
    {
        CurrentIdText.text = QuestionSaver.CurrentQuestionId.ToString();
    }

    public void CallRegister()
    {
       //StartCoroutine(RegisterFunc());
       SaveQuestion();
    }

    public void SaveQuestion()
    {
        Question question = new Question(Pergunta.text, 
                                RespostaCerta.text, 
                                new []{RespostaErrada1.text,RespostaErrada2.text,RespostaErrada3.text},
                                (Categories)Categoria.value, ++QuestionSaver.CurrentQuestionId);
        
        QuestionHash.Add(question);
        
        foreach (var log in QuestionHash.Log())
        {
            Debug.Log(log);
        }
        
        //QuestionSaver.SaveQuestion(question);
    }

    public void SaveBuffer()
    {
        QuestionSaver.SaveQuestion(QuestionHash.GetArray());
    }

    public void DeleteFile()
    {
        QuestionSaver.DeleteSaveFile();
    }

    private IEnumerator RegisterFunc()
    {
        // List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        // formData.Add(new MultipartFormDataSection("categoria", GetCategorie()));
        // formData.Add(new MultipartFormDataSection(name: "pergunta", data: Pergunta.text));
        // formData.Add(new MultipartFormDataSection("respostaCerta", RespostaCerta.text));
        // formData.Add(new MultipartFormDataSection("respostaErrada1", RespostaErrada1.text));
        // formData.Add(new MultipartFormDataSection("respostaErrada2", RespostaErrada2.text));
        // formData.Add(new MultipartFormDataSection("respostaErrada3", RespostaErrada3.text));
    
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("foo", "bar"));
        
        UnityWebRequest wwwR = UnityWebRequest.Post("https://www.capudino.com/sqlconnect/result", formData);

        yield return wwwR.SendWebRequest();

        if (wwwR.isNetworkError || wwwR.isHttpError)
        {
            Debug.Log(wwwR.error);
        }
        else
        {
            Debug.Log("Pergunta Registrada");
            Debug.Log(wwwR.responseCode);
            string responseText = wwwR.downloadHandler.text;
            Debug.Log("Response Text:" + responseText);
        }
    }

    private IEnumerator GetFunc()
    {
        UnityWebRequest wwwR = UnityWebRequest.Get("https://www.capudino.com/sqlconnect/getrequest.py");

        yield return wwwR.SendWebRequest();

        if (wwwR.isNetworkError || wwwR.isHttpError)
        {
            Debug.Log(wwwR.error);
        }
        else
        {
            Debug.Log("Pergunta Registrada");
            Debug.Log(wwwR.responseCode);
            string responseText = wwwR.downloadHandler.text;
            Debug.Log("Response Text:" + responseText);
        }
    }
    
    public string GetCategorie()
    {
        return ((Categories)Categoria.value).ToString(); 
    }
    
    public void VerifyInputs()
    {
        submitButton.interactable = (Pergunta.text.Length >= 1 &&
                                        RespostaCerta.text.Length >= 1 &&
                                        RespostaErrada1.text.Length >= 1 &&
                                        RespostaErrada2.text.Length >= 1 &&
                                        RespostaErrada3.text.Length >= 1 );
    }
}
