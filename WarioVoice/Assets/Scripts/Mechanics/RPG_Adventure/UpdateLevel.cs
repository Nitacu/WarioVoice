using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateLevel : MonoBehaviour
{

    public void reload()
    {
        if (AttackGlossary.GetInstance()._difficultyLevel != 10)
        {
            AttackGlossary.GetInstance()._difficultyLevel++;
        }

        SceneManager.LoadScene("RPg_adventure");
    }

}
