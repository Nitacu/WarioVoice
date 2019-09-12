using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferencePaintSplash : PaintSplash
{
    private bool _evaluatorSplash;
    public bool EvaluatorSplash { get => _evaluatorSplash; set => _evaluatorSplash = value; }

    [SerializeField] private float _evaluateRadius;
    [SerializeField] private LayerMask _mask;

    public override void Start()
    {
        base.Start();


    }

    public bool evaluateSimilarSplashAround()
    {
        if (_evaluatorSplash)
        { }


        Collider2D[] _hit = Physics2D.OverlapCircleAll(transform.position, _evaluateRadius, _mask);

        if (_hit.Length > 0)
        {
            foreach (var item in _hit)
            {
                if (item.gameObject.GetComponent<SelfPaintSplash>().MySplashColorType == MySplashColorType)
                {
                    if (!item.gameObject.GetComponent<SelfPaintSplash>().Matched)
                    {
                        item.gameObject.GetComponent<SelfPaintSplash>().Matched = true;
                        return true;
                    }
                }
            }

            
        }



        return false;
    }

   /* void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _evaluateRadius);
    }*/

}
