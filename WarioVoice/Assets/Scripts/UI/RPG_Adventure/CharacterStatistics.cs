using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatistics : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'CharacterStatistics._life' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _life;
#pragma warning restore CS0649 // El campo 'CharacterStatistics._life' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'CharacterStatistics._heart' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _heart;
#pragma warning restore CS0649 // El campo 'CharacterStatistics._heart' nunca se asigna y siempre tendrá el valor predeterminado null
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

// cuando muere colocar el rostro de color gris
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
