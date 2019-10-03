using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatistics : MonoBehaviour
{
    [SerializeField] private GameObject _life;
    [SerializeField] private GameObject _heart;
    [SerializeField]private List<GameObject> _listHearts = new List<GameObject>();
    [SerializeField] private Image _icon;

    public Image Icon { get => _icon; set => _icon = value; }

    // recibe la vida actual si es menor borra corazones si es mayor los crea

    public void reloadStatistics(float life)
    {
        while (life != _listHearts.Count)
        {
            if (life < _listHearts.Count)
            {
                Destroy(_listHearts[0]);
                 _listHearts.RemoveAt(0);
                
            }
            else if (life > _listHearts.Count)
            {
                _listHearts.Add(Instantiate(_heart, _life.transform));
            }
        }

        if (_listHearts.Count == 0)
        {
            _icon.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            _icon.GetComponent<Image>().color = Color.white;
        }
    }
}
