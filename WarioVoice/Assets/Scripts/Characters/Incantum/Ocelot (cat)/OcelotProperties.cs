using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcelotProperties : MonoBehaviour
{
    [SerializeField]private GameObject _mouth;

    public bool _hasObj = false;

    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.enabled = false;
#pragma warning disable CS0618 // 'ParticleSystem.duration' está obsoleto: 'duration property is deprecated. Use main.duration instead.'
        Invoke("activeCat",GetComponent<ParticleSystem>().duration);
#pragma warning restore CS0618 // 'ParticleSystem.duration' está obsoleto: 'duration property is deprecated. Use main.duration instead.'
    }

    private void activeCat()
    {
        _anim.enabled = true;
    }

    public GameObject Mouth { get => _mouth; set => _mouth = value; }
}
