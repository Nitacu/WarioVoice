using UnityEngine;
using System.Collections;

using UnityEngine.UI;

/*

    Import AIML files within the StreamingAssets

*/


public class ChatWindowExamplePC : MonoBehaviour
{
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'ChatbotPC' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
    private ChatbotPC bot;
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'ChatbotPC' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
    public InputField inputField;
    public Text robotOutput;

    // Use this for initialization
    void Start()
    {
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'ChatbotPC' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        bot = new ChatbotPC();
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'ChatbotPC' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        bot.LoadBrain();
    }


    void OnDisable()
    {
        bot.SaveBrain();
    }


    /// <summary>
    /// Button to send the question to the robot
    /// </summary>
    public void SendQuestionToRobot()
    {
        if (string.IsNullOrEmpty(inputField.text) == false)
        {
            // Response Bot AIML
            var answer = bot.getOutput(inputField.text);
            // Response BotAIml in the Chat window
            robotOutput.text = answer;
            //
            inputField.text = string.Empty;
        }
    }


}
