using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BossDefeatedFeedBack : MonoBehaviour
{
    const string APPEAR = "Appear";
    const string DISAPPEAR = "Disappear";
    const string SHAKE = "Shake";


    private int currentBossesDefeated = 1;


    [SerializeField] private TextMeshProUGUI _bossesText;
    [SerializeField] private TextMeshProUGUI _playername;
    [SerializeField] private float timeTochangeScene = 3;

    [SerializeField] private List<GameObject> _bossIcons = new List<GameObject>();
    [SerializeField] private Sprite _defeatedIcon;
    [SerializeField] private Sprite _unDefeatedIcon;
    [SerializeField] private AnimationClip _animationClip;
    private float _animationTime;

    private void Awake()
    {
        
        currentBossesDefeated = SaveSystem.getPlayerInstace().bossesDefeated;
        PlayerInformation playerInf = SaveSystem.getPlayerInstace();
        _playername.text = playerInf.playerName;
        _bossesText.text = ShowSlotData.DEFEATED_BOSSES + "\n" + (playerInf.bossesDefeated - 1).ToString() + "/" + GameManager.maxBosses.ToString();
        
        _animationTime = _animationClip.length;

    }


    private void Start()
    {

        foreach (var item in _bossIcons)
        {
            item.GetComponent<Image>().sprite = _unDefeatedIcon;
        }

        for (int i = 0; i < currentBossesDefeated -1; i++)
        {
            _bossIcons[i].GetComponent<Image>().sprite = _defeatedIcon;
        }

        StartCoroutine(defeatAnimation(currentBossesDefeated - 1, 1.5f));

    }

    IEnumerator defeatAnimation(int indexIcon, float timeToStartAnimation)
    {
        Animator iconAnim = _bossIcons[indexIcon].GetComponent<Animator>();
        iconAnim.Play(Animator.StringToHash(SHAKE));


        yield return new WaitForSeconds(timeToStartAnimation);

        iconAnim.Play(Animator.StringToHash(DISAPPEAR));

        StartCoroutine(changeSprite(indexIcon));
    }

    IEnumerator changeSprite(int indexIcon)
    {

        yield return new WaitForSeconds(_animationTime);
        _bossIcons[indexIcon].GetComponent<Image>().sprite = _defeatedIcon;
        _bossIcons[indexIcon].GetComponent<Animator>().Play(Animator.StringToHash(APPEAR));

        _bossesText.text = ShowSlotData.DEFEATED_BOSSES + "\n" + (SaveSystem.getPlayerInstace().bossesDefeated ).ToString() + "/" + GameManager.maxBosses.ToString();

        StartCoroutine(changeScene());
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(timeTochangeScene);

        GameManager.GetInstance().StartGame();
    }
}
