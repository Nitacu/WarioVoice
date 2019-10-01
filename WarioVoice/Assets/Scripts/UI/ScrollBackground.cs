using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    RawImage rawIm;

    [SerializeField] float lerpVelocity;


    void Start()
    {
        rawIm = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset;
        offset.x = rawIm.uvRect.x;
        offset.y = rawIm.uvRect.y;
        offset.x += Time.deltaTime / lerpVelocity;
        offset.y += Time.deltaTime / lerpVelocity;

        rawIm.uvRect = new Rect(offset.x, offset.y, rawIm.uvRect.width, rawIm.uvRect.height);

        //material.mainTextureOffset = offset;
    }
}
