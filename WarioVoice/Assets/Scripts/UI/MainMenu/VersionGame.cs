using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionGame : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'VersionGame._versionText' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private TMP_Text _versionText;
#pragma warning restore CS0649 // El campo 'VersionGame._versionText' nunca se asigna y siempre tendrá el valor predeterminado null
    private void Start()
    {
         _versionText.text = "V " +  Application.version;
    }
}
