using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferencePaintSplash : PaintSplash
{
    private bool _evaluatorSplash;
    public bool EvaluatorSplash { get => _evaluatorSplash; set => _evaluatorSplash = value; }

#pragma warning disable CS0649 // El campo 'ReferencePaintSplash._evaluateRadius' nunca se asigna y siempre tendrá el valor predeterminado 0
    [SerializeField] private float _evaluateRadius;
#pragma warning restore CS0649 // El campo 'ReferencePaintSplash._evaluateRadius' nunca se asigna y siempre tendrá el valor predeterminado 0
#pragma warning disable CS0649 // El campo 'ReferencePaintSplash._mask' nunca se asigna y siempre tendrá el valor predeterminado 
    [SerializeField] private LayerMask _mask;
#pragma warning restore CS0649 // El campo 'ReferencePaintSplash._mask' nunca se asigna y siempre tendrá el valor predeterminado 

    public override void Start()
    {
        base.Start();


    }

    public bool evaluateSimilarSplashAround()
    {

        Collider2D[] _hit = Physics2D.OverlapCircleAll(transform.position, _evaluateRadius, _mask);


        List<float> _hitDistances = new List<float>();
        List<GameObject> _unMatchedSplashes = new List<GameObject>();
        _hitDistances.Clear();
        _unMatchedSplashes.Clear();

        int _indexMostLowerDistance = 0;

        if (_hit.Length > 0)
        {
            foreach (var item in _hit)
            {
                if (item.gameObject.GetComponent<SelfPaintSplash>().MySplashColorType == MySplashColorType)
                {
                    if (!item.gameObject.GetComponent<SelfPaintSplash>().Matched)
                    {
                        _hitDistances.Add(Vector2.Distance(transform.position, item.gameObject.transform.position));
                        _unMatchedSplashes.Add(item.gameObject);
                        //item.gameObject.GetComponent<SelfPaintSplash>().Matched = true;
                        //return true;
                    }
                }
            }

            if (_unMatchedSplashes.Count > 0)
            {
                for (int i = 1; i < _hitDistances.Count; i++)
                {
                    if (_hitDistances[_indexMostLowerDistance] > _hitDistances[i])
                    {
                        _indexMostLowerDistance = i;
                    }
                }

                _unMatchedSplashes[_indexMostLowerDistance].GetComponent<SelfPaintSplash>().Matched = true;

                return true;
            }

            
        }



        return false;
    }

    void OnDrawGizmosSelected()
    {        
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _evaluateRadius);
    }

}
