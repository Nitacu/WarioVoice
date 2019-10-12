using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClampText : MonoBehaviour
{
    public TextMeshProUGUI nameLable;
    [Header("Para cualquier objeto")]
    [SerializeField] private GameObject _UIGameObject;

    public float offsetY = 0;

    void Update()
    {
        if (nameLable)
        {
            Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
            nameLable.transform.position = new Vector3(namePos.x,namePos.y + offsetY, namePos.z);
        }
        else if (_UIGameObject)
        {
            Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
            _UIGameObject.transform.position = namePos;
        }

    }
}

