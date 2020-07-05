using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodActionsController : MonoBehaviour
{
    [SerializeField] private float _playPoints = 15;
    [SerializeField] private float _foodPoints = 50;
    [SerializeField] private float _showerPoints = 5;
    [SerializeField] private TitoMoodController _titoMood;

    public void playWithTito()
    {
        _titoMood.addMoodPoints(_playPoints);
    }

    public void eatWithTito()
    {
        _titoMood.addMoodPoints(_foodPoints);
    }

    public void showerWithTito()
    {
        _titoMood.addMoodPoints(_showerPoints);
    }
}
