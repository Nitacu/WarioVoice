using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



class AndroidPluginCallback : AndroidJavaProxy
{
    public AndroidPluginCallback() : base("com.artesanal.unityplugin.PluginCallback") { }

    public void onSuccess(string textSpeech)
    {
        //speechContoller control = GameObject.FindObjectOfType<speechContoller>();
        speechContoller control = speechContoller.getInstance();
        Debug.Log("ENTER callback onSuccess: " + textSpeech);
        control.settext(textSpeech);
       
        //BotInput chat = GameObject.FindObjectOfType<BotInput>();
        BotInput chat = BotInput.getInstance();
        string texto = textSpeech;
        chat.SendQuestionToRobotFromSpeech(texto);

    }

    public void onStart(string startmessage)
    {
        //speechContoller control = GameObject.FindObjectOfType<speechContoller>();
        speechContoller control = speechContoller.getInstance();
        Debug.Log("ENTER callback onError: " + startmessage);
        control.speechReturned.text = startmessage;
    }


     public void onEnd(string endmessage)
    {
        //speechContoller control = GameObject.FindObjectOfType<speechContoller>();
        speechContoller control = speechContoller.getInstance();
        Debug.Log("ENTER callback onError: " + endmessage);
        control.speechReturned.text = endmessage;
    }

    public void onError(string errorMessage)
    {
        //speechContoller control = GameObject.FindObjectOfType<speechContoller>();
        speechContoller control = speechContoller.getInstance();
        Debug.Log("ENTER callback onError: " + errorMessage);
        control.speechReturned.text = errorMessage;
    }
}


public class speechContoller : MonoBehaviour {

   
    public TMP_Text speechReturned;
    private Microphone mic;
     private static speechContoller instance;


    // Use this for initialization

   
    void Awake()
    {
        initSpech();
        //AndroidPermissionsUsageExample permision = GameObject.FindObjectOfType<AndroidPermissionsUsageExample>();
        //permision.OnBrowseGalleryButtonPress();
        instance = this;
    }

    
    void Start () {
        //initSpech();
        //AndroidPermissionsUsageExample permision = GameObject.FindObjectOfType<AndroidPermissionsUsageExample>();
        //permision.OnBrowseGalleryButtonPress();
        //instance = this;
    }


    public void settext(string speechText){
        speechReturned.text= speechText;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void openexplorer()
    {
        var androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
        // Accessing the class to call a static method on it
        var jc = new AndroidJavaClass("com.artesanal.unityplugin.plugin");
        // Calling a Call method to which the current activity is passed
        jc.Call("getfile", jo);
    }

    public void openactivity()
    {
        var androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
        // Accessing the class to call a static method on it
        var jc = new AndroidJavaClass("com.artesanal.unityplugin.plugin");
        // Calling a Call method to which the current activity is passed
        //jc.CallStatic("openactivity", jo);
        jo.Call("getfile", jo);
        
    }

    public void initSpech()
    {
        var androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
        // Accessing the class to call a static method on it
        var jc = new AndroidJavaClass("com.artesanal.unityplugin.plugin");
        // Calling a Call method to which the current activity is passed
        //jc.CallStatic("openactivity", jo);
        jo.Call("runOnUiThread", new AndroidJavaRunnable(() => { jc.CallStatic("initializeSpeech", jo, new AndroidPluginCallback()); }));

        //jc.CallStatic("initializeSpeech",jo, new AndroidPluginCallback());
    }

    public void startSpeech()
    {
        var androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
        // Accessing the class to call a static method on it
        var jc = new AndroidJavaClass("com.artesanal.unityplugin.plugin");
        // Calling a Call method to which the current activity is passed
        //jc.CallStatic("openactivity", jo);
        jo.Call("runOnUiThread", new AndroidJavaRunnable(() => { jc.CallStatic("startSpeech", jo); }));
        //jc.CallStatic("startSpeech", jo);
    }


     public static speechContoller getInstance(){
        
    {
        if (instance==null)
    {
        instance = new speechContoller();
    }
        return instance;
    }
    
    }

 
}
