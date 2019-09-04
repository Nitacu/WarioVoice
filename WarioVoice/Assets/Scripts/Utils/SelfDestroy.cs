using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private float _time;

    void Update()
    {
        Time -= UnityEngine.Time.deltaTime;

        if (Time <= 0)
        {
            Destroy(gameObject);
        }    
    }

    public float Time { get => _time; set => _time = value; }
}
