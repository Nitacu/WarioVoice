using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlayCat : MonoBehaviour
{
    [SerializeField] private GameObject _deathParticlesCat;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CAT"))
        {
            Instantiate(_deathParticlesCat, collision.gameObject.transform.position,Quaternion.identity);
            Destroy(collision.gameObject);
            Invoke("restarLevel", _deathParticlesCat.GetComponent<ParticleSystem>().duration + 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CAT"))
        {
            Instantiate(_deathParticlesCat, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Invoke("restarLevel", _deathParticlesCat.GetComponent<ParticleSystem>().duration+ 0.5f);
        }
    }

    private void restarLevel()
    {
        SceneManager.LoadScene(Application.loadedLevelName);
    }
}
