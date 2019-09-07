using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancerController : MonoBehaviour
{
   
    public void dancePlayer(Crystal crystal)
    {
        GetComponent<Animator>().Play(crystal.danceClip.name);
    }
}
