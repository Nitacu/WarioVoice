using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{

    public List<Transform> positionList = new List<Transform>();
    private int random = 0;

    public void setPosition()
    {
        random = Random.Range(0, positionList.Count);
        transform.position = positionList[random].position;
    }

}
