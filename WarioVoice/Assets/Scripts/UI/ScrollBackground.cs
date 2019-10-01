using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    Material material;

    [SerializeField] float lerpVelocity;


    void Start()
    {
        material = GetComponent<RawImage>().material;
    }

    // Update is called once per frame
    void Update()
    {
       Vector2 offset = material.mainTextureOffset;
        offset.x += Time.deltaTime / lerpVelocity;
        offset.y += Time.deltaTime / lerpVelocity;

        material.mainTextureOffset = offset;
    }
}
