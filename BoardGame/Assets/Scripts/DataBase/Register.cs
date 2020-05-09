using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
    private enum Categorias
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
    
    public TMP_InputField Pergunta;
    public TMP_InputField RespostaCerta;
    public TMP_InputField RespostaErrada1;
    public TMP_InputField RespostaErrada2;
    public TMP_InputField RespostaErrada3;

    public TMP_Dropdown Categoria;

    public Button submitButton;

    public void CallRegister()
    {
        StartCoroutine(RegisterFunc());
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
        return ((Categorias)Categoria.value).ToString(); 
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
