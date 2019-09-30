using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLoveGame : MonoBehaviour
{
    public WordController _controller;

    public void startGameOfLove()
    {
        gameObject.SetActive(false);
        _controller.startGame();
    }
}
