using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionGame : MonoBehaviour
{
    [SerializeField] private TMP_Text _versionText;
    private void Start()
    {
         _versionText.text = "V " +  Application.version;
    }
}
