using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class QuestionSaver
{
   public static bool HasLoaded { get; private set; } = false;
   private static string path = Application.persistentDataPath + "/questions.qst";
   private static string gitPath = Application.dataPath + "/Resources/GitQuestionFile/questions.qst";

   public static int CurrentQuestionId = 0;
   
   public static void SaveQuestion(Question[] question)
   {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(path, FileMode.Create);

      formatter.Serialize(stream, question);
      stream.Close();
      
      BinaryFormatter formatterGit = new BinaryFormatter();
      FileStream streamGit = new FileStream(gitPath, FileMode.Create);

      formatterGit.Serialize(streamGit, question);
      streamGit.Close();
   }

   public static Question[] LoadQuestion()
   {
      if (File.Exists(path))
      {
         BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(path, FileMode.Open);

         Question[] question = formatter.Deserialize(stream) as Question[];
         stream.Close();

         CurrentQuestionId = question.Length;

         HasLoaded = true;
         
         return question;
      }
      else if (File.Exists(gitPath))
      {
         BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(gitPath, FileMode.Open);

         Question[] question = formatter.Deserialize(stream) as Question[];
         stream.Close();

         CurrentQuestionId = question.Length;

         HasLoaded = true;
         
         return question;
      }
      else 
      {
         Debug.LogError("Save file do not exist at:  " + path);
         return null;
      }
   }

   public static void DeleteSaveFile()
   {
      if (File.Exists(path))
      {
         File.Delete(path);
      }
   }
}
