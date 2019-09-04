using UnityEngine;
using System.Collections;
//
using System.Xml;
using System.Collections.Generic;

using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;
/*

Import AIML files within the Resources

*/

public class BotInput : MonoBehaviour
{

    private TextAsset[] aimlFiles;
    private List<string> aimlXmlDocumentListFileName = new List<string>();
    private List<XmlDocument> aimlXmlDocumentList = new List<XmlDocument>();
    //
    private TextAsset GlobalSettings, GenderSubstitutions, Person2Substitutions, PersonSubstitutions, Substitutions, DefaultPredicates, Splitters;
    //
    private ChatbotMobileWeb bot;
    private CommandParser _commandParser;
    
    public Text robotOutput;

    private static BotInput instance;

    // Use this for initialization


    void Awake()
    {
         instance = this;
    }


    void Start()
    {
        if(FindObjectOfType<CommandParser>()){
            _commandParser= FindObjectOfType<CommandParser>();
        }
        else{
            Debug.Log("commandparser not found");
        }
         
        bot = new ChatbotMobileWeb();
        LoadFilesFromConfigFolder();
        bot.LoadSettings(GlobalSettings.text, GenderSubstitutions.text, Person2Substitutions.text, PersonSubstitutions.text, Substitutions.text, DefaultPredicates.text, Splitters.text);
        TextAssetToXmlDocumentAIMLFiles();
        bot.loadAIMLFromXML(aimlXmlDocumentList.ToArray(), aimlXmlDocumentListFileName.ToArray());
        bot.LoadBrain();

       
       
    }


    


    // Update is called once per frame
    void Update()
    {


    }


    /// <summary>
    /// Button to send the question to the robot
    /// </summary>
  

    public void SendQuestionToRobotFromSpeech(string speechText)
    {
        robotOutput.text = "";
        if (string.IsNullOrEmpty(speechText) == false)
        {
            // Response Bot AIML
            string text = RemoveDiacritics(speechText);
            var answer = bot.getOutput(text);
            Debug.Log("texto enviado" + text);

            answer = answer.Replace(".", "");

            // Response BotAIml in the Chat window
            if(_commandParser!=null){
                _commandParser.parseCommand(answer);
                Debug.Log("command send to parser");
            }
         
            robotOutput.text = answer;

            Debug.Log("texto recibido" + answer);
            //
            //inputField.text = string.Empty;
        }
    }


    public static string RemoveDiacritics(string input)
    {
        var stFormD = input.Normalize(NormalizationForm.FormD);
        var len = stFormD.Length;
        var sb = new StringBuilder();
        for (int i = 0; i < len; i++)
        {
            var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[i]);
            if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                sb.Append(stFormD[i]);
            }
        }
        return (sb.ToString().Normalize(NormalizationForm.FormC));
    }


    void LoadFilesFromConfigFolder()
    {
        GlobalSettings = Resources.Load<TextAsset>("config/Settings");
        GenderSubstitutions = Resources.Load<TextAsset>("config/GenderSubstitutions");
        Person2Substitutions = Resources.Load<TextAsset>("config/Person2Substitutions");
        PersonSubstitutions = Resources.Load<TextAsset>("config/PersonSubstitutions");
        Substitutions = Resources.Load<TextAsset>("config/Substitutions");
        DefaultPredicates = Resources.Load<TextAsset>("config/DefaultPredicates");
        Splitters = Resources.Load<TextAsset>("config/Splitters");
    }

    void TextAssetToXmlDocumentAIMLFiles()
    {
        aimlFiles = Resources.LoadAll<TextAsset>("aiml");
        foreach (TextAsset aimlFile in aimlFiles)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(aimlFile.text);
                aimlXmlDocumentListFileName.Add(aimlFile.name);
                aimlXmlDocumentList.Add(xmlDoc);
            }catch(System.Exception e)
            {
                Debug.LogWarning(e.ToString());
            }
        }
    }


    void OnDisable()
    {
        bot.SaveBrain();
    }


     private string DecodeFromUtf82(string texto)
    {

        byte[] bytes = Encoding.Default.GetBytes(texto);
        string cadena = Encoding.UTF8.GetString(bytes);
        cadena = getCode(cadena);
        return cadena;
    }

   

    public string getNumberinString(string inputText)
    {

        string mynumber = Regex.Replace(inputText, "[^0-9]", "");
        
        Debug.Log("numero encontrado en cadena de texto " + mynumber);
        return mynumber;

    }


    public string getCode(string inputString)
    {
        string result = "";
        string resultNumber = "";
        string resultAudioNumber;
        string resultFunctionNumber;

       
        foreach (Match match in Regex.Matches(inputString, @"(?<!\w)#\w+"))
        {
            Debug.Log("el codigo es: " + match.Value);
            //sitio donde se envia el codigo para procesarlo

            resultNumber = getNumberinString(match.Value);
            //_inputManager.initAct(resultNumber);
            result = inputString.Replace(match.Value, "");
          
        }

        if (result == "")
        {
            result = inputString;
        }

        foreach (Match match in Regex.Matches(result, @"(?<!\w)%\w+"))
        {
            Debug.Log("el codigo de audio es: " + match.Value);
            //sitio donde se envia el codigo para procesarlo

            resultAudioNumber = getNumberinString(match.Value);
            //_inputManager.getAudioManager().PlayAudio(resultAudioNumber);
            result = result.Replace(match.Value, "");
        }

        if (result == "")
        {
            result = inputString;
        }

        foreach (Match match in Regex.Matches(result, @"(?<!\w)?\w+"))
        {
            Debug.Log("el codigo de funcion es: " + match.Value);
            //sitio donde se envia el codigo para procesarlo

            resultFunctionNumber = getNumberinString(match.Value);
            //_inputManager._directActions.initAction(resultFunctionNumber);
            //_inputManager.getAudioManager().PlayAudio(resultAudioNumber);
            result = result.Replace(match.Value, "");
        }

        if (result == "")
        {
            return inputString;
        }

        //Debug.Log(" El codigo de acto es" + result );
        return result;
    }


    public static BotInput getInstance(){
        
    {
        if (instance==null)
    {
        instance = new BotInput();
    }
        return instance;
    }
    
    }
    




}
