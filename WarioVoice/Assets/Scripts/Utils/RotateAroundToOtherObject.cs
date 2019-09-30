using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundToOtherObject : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _axis;
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(_object.transform.position, _axis, Time.deltaTime * _speed);
    }
}
