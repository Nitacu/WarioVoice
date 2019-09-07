using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClampText : MonoBehaviour
{
    public TextMeshProUGUI nameLable;   
    // Update is called once per frame
    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        nameLable.transform.position = namePos;
    }
}

