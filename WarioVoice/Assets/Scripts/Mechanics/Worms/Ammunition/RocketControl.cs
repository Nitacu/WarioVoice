using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControl : MonoBehaviour
{
    [SerializeField] GameObject _smallExplotion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        Instantiate(_smallExplotion,transform.position,Quaternion.identity);

        
        if (FindObjectOfType<Ammunition>().Amnunition == 0)
        {
            FindObjectOfType<ConfigurationWorms>().lostGame();
        }
        else
        {
            if (FindObjectOfType<ConvertAngles>().TutorialMode)
            {
                FindObjectOfType<ConvertAngles>().TutorialMode = false;
                FindObjectOfType<GuideControlWorm>().desactiveAction();
            }
            else
            {
                FindObjectOfType<GuideControlWorm>().activeKeepAction();
            }

        }

    }

}
