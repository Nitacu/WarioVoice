using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfPaintSplash : PaintSplash
{
    private Vector3 _originalScale;
    public Vector3 OriginalScale { get => _originalScale; set => _originalScale = value; }

    [SerializeField] private float _speedScale;

    private bool _matched;
    public bool Matched { get => _matched; set => _matched = value; }

    // Start is called before the first frame update
    void Start()
    {
        OriginalScale = transform.localScale;
        transform.localScale = Vector2.zero; 
    }

    // Update is called once per frame
    void Update()
    {
        if (!Mathf.Approximately(_originalScale.x, transform.localScale.x))
        {
            Vector2 _targetScale = Vector2.Lerp(transform.localScale, _originalScale, _speedScale);
            transform.localScale = _targetScale;
        }
    }
}
