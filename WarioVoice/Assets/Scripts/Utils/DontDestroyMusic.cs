using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyMusic : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'DontDestroyMusic._music' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] GameObject _music;
#pragma warning restore CS0649 // El campo 'DontDestroyMusic._music' nunca se asigna y siempre tendrá el valor predeterminado null

    void Awake()
    {
        DontDestroyOnLoad(_music);
        // add the callback method when scene loads
        SceneManager.sceneLoaded += OnSceneLoad;

        GameObject[] obs = GameObject.FindGameObjectsWithTag(Tags.Music);
        if (obs.Length > 1)
        {
            Destroy(_music);
        }

 
    }

    // called when scene loads
    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        
        if (scene.name != ChangeScene.SPIKINGLISHMENU && scene.name != ChangeScene.ESPIKINGLISHCREDITS)
        {            
            Destroy(_music);
            return;
        }
    }


}
