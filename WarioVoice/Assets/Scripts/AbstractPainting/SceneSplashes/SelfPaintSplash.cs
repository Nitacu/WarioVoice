using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfPaintSplash : PaintSplash
{
    private Vector3 _originalScale;
    public Vector3 OriginalScale { get => _originalScale; set => _originalScale = value; }

#pragma warning disable CS0649 // El campo 'SelfPaintSplash._speedScale' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private float _speedScale;
#pragma warning restore CS0649 // El campo 'SelfPaintSplash._speedScale' nunca se asigna y siempre tendrá el valor predeterminado 0

    private bool _matched;
    public bool Matched { get => _matched; set => _matched = value; }

    // Start is called before the first frame update
#pragma warning disable CS0114 // 'SelfPaintSplash.Start()' oculta el miembro heredado 'PaintSplash.Start()'. Para hacer que el miembro actual invalide esa implementación, agregue la palabra clave override. Si no, agregue la palabra clave new.
    void Start()
#pragma warning restore CS0114 // 'SelfPaintSplash.Start()' oculta el miembro heredado 'PaintSplash.Start()'. Para hacer que el miembro actual invalide esa implementación, agregue la palabra clave override. Si no, agregue la palabra clave new.
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
