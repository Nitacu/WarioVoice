using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundToOtherObject : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'RotateAroundToOtherObject._object' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _object;
#pragma warning restore CS0649 // El campo 'RotateAroundToOtherObject._object' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'RotateAroundToOtherObject._speed' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private float _speed;
#pragma warning restore CS0649 // El campo 'RotateAroundToOtherObject._speed' nunca se asigna y siempre tendrá el valor predeterminado 0
#pragma warning disable CS0649 // El campo 'RotateAroundToOtherObject._axis' nunca se asigna y siempre tendrá el valor predeterminado 
    [SerializeField] private Vector3 _axis;
#pragma warning restore CS0649 // El campo 'RotateAroundToOtherObject._axis' nunca se asigna y siempre tendrá el valor predeterminado 
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(_object.transform.position, _axis, Time.deltaTime * _speed);
    }
}
