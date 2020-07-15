using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlayCat : MonoBehaviour
{
#pragma warning disable CS0649 // El campo 'SlayCat._deathParticlesCat' nunca se asigna y siempre tendrá el valor predeterminado null
    [SerializeField] private GameObject _deathParticlesCat;
#pragma warning restore CS0649 // El campo 'SlayCat._deathParticlesCat' nunca se asigna y siempre tendrá el valor predeterminado null
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CAT"))
        {
            Instantiate(_deathParticlesCat, collision.gameObject.transform.position,Quaternion.identity);
            Destroy(collision.gameObject);
#pragma warning disable CS0618 // 'ParticleSystem.duration' está obsoleto: 'duration property is deprecated. Use main.duration instead.'
            Invoke("restarLevel", _deathParticlesCat.GetComponent<ParticleSystem>().duration + 0.5f);
#pragma warning restore CS0618 // 'ParticleSystem.duration' está obsoleto: 'duration property is deprecated. Use main.duration instead.'
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CAT"))
        {
            Instantiate(_deathParticlesCat, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
#pragma warning disable CS0618 // 'ParticleSystem.duration' está obsoleto: 'duration property is deprecated. Use main.duration instead.'
            Invoke("restarLevel", _deathParticlesCat.GetComponent<ParticleSystem>().duration+ 0.5f);
#pragma warning restore CS0618 // 'ParticleSystem.duration' está obsoleto: 'duration property is deprecated. Use main.duration instead.'
        }
    }

    private void restarLevel()
    {
#pragma warning disable CS0618 // 'Application.loadedLevelName' está obsoleto: 'Use SceneManager to determine what scenes have been loaded'
        SceneManager.LoadScene(Application.loadedLevelName);
#pragma warning restore CS0618 // 'Application.loadedLevelName' está obsoleto: 'Use SceneManager to determine what scenes have been loaded'
    }
}
