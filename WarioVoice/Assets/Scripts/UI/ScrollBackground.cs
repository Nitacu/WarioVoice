using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    RawImage rawIm;

    [SerializeField] float lerpVelocity;

    enum horizontalDirection
    {
        LEFT,
        RIGHT,
        STATIC
    }

    enum verticalDirection
    {
        DOWN,
        UP,
        STATIC
    }

    [SerializeField] private horizontalDirection _horizontalDirection;
    [SerializeField] private verticalDirection _verticalDirection;

    private float _xDirection;
    private float _yDirection;

    void Start()
    {
        rawIm = GetComponent<RawImage>();

        int randomHorizontal = Random.Range(0, 2);
        _xDirection = (randomHorizontal == 0) ? -1 : 1;

        int randomVertical = Random.Range(0, 2);
        _yDirection = (randomVertical == 0) ? -1 : 1;


        /*
        switch (_horizontalDirection)
        {
            case horizontalDirection.LEFT:
                _xDirection = 1;
                break;
            case horizontalDirection.RIGHT:
                _xDirection = -1;
                break;
            case horizontalDirection.STATIC:
                _xDirection = 0;
                break;
        }

        switch (_verticalDirection)
        {
            case verticalDirection.DOWN:
                _yDirection = 1;
                break;
            case verticalDirection.UP:
                _yDirection = -1;
                break;
            case verticalDirection.STATIC:
                _yDirection = 0;

                break;
        }      */
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset;
        offset.x = rawIm.uvRect.x;
        offset.y = rawIm.uvRect.y;
        offset.x += (Time.deltaTime * _xDirection)/ lerpVelocity;
        offset.y += (Time.deltaTime * _yDirection)/ lerpVelocity;

        rawIm.uvRect = new Rect(offset.x, offset.y, rawIm.uvRect.width, rawIm.uvRect.height);

        //material.mainTextureOffset = offset;
    }
}
