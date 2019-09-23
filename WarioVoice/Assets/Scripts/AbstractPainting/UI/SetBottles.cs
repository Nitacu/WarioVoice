using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBottles : MonoBehaviour
{
    [SerializeField] private List<BottlePaintHelpButton> _bottles = new List<BottlePaintHelpButton>();

    public void setBottles()
    {
        foreach (var item in _bottles)
        {
            item.gameObject.SetActive(false);
        }

        AbstractPaintingManager _manager = FindObjectOfType<AbstractPaintingManager>();

        Debug.Log("Available paints: " + _manager.Levels[_manager.CurrentLevel].AvailableColors.Count);

        for (int i = 0; i < _manager.Levels[_manager.CurrentLevel].AvailableColors.Count; i++)
        {
            _bottles[i].gameObject.SetActive(true);
            _bottles[i].BottleColor = _manager.Levels[_manager.CurrentLevel].AvailableColors[i]._brushColor;
            _bottles[i].AudioClip = _manager.Levels[_manager.CurrentLevel].AvailableColors[i]._audioClip;
            _bottles[i].Text.text = _manager.Levels[_manager.CurrentLevel].AvailableColors[i]._colorName;
            _bottles[i].Text.color = _manager.Levels[_manager.CurrentLevel].AvailableColors[i]._brushColor;            
        }

       

    }
}
