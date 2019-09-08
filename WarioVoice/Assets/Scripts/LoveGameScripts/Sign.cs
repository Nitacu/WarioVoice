using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sign", menuName = "Sign")]
public class Sign : ScriptableObject
{ 
    public Sprite signSprite;
    public WordList.itemNames item;
    public List<WordList.itemNames> possibleAnswers = new List<WordList.itemNames>();
    //public bool hasSprite;
}
