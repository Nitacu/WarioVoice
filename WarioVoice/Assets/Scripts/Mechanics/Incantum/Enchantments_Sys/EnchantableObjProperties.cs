using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantableObjProperties : MonoBehaviour
{
    [SerializeField] private bool _allowShowTag = true;
    [SerializeField] private bool _allowMakeBigger = true;
    [SerializeField] private bool _allowMakeSmaller = true;
    [SerializeField] private bool _allowChangeMaterial = true;
    [SerializeField] private bool _allowFreeze = true;
    [SerializeField] private bool _allowChangeGravite = true;
    [SerializeField] private bool _allowGrab = false;
    [SerializeField] private float _gravityScale;
    [SerializeField] private bool _allowIgnite; // para saber si se puede quemar el objeto
    [SerializeField] private bool _onfire;  //para saber si ya esta quemandose
    [SerializeField] private bool _onBurn = false;
    [SerializeField] private bool _destroyWhiteFire = false;
    

    // GET Y SET
    public bool AllowShowTag { get => _allowShowTag; set => _allowShowTag = value; }
    public bool AllowMakeBigger { get => _allowMakeBigger; set => _allowMakeBigger = value; }
    public bool AllowMakeSmaller { get => _allowMakeSmaller; set => _allowMakeSmaller = value; }
    public float GravityScale { get => _gravityScale; set => _gravityScale = value; }
    public bool AllowIgnite { get => _allowIgnite; set => _allowIgnite = value; }
    public bool Onfire { get => _onfire; set => _onfire = value; }
    public bool AllowChangeMaterial { get => _allowChangeMaterial; set => _allowChangeMaterial = value; }
    public bool AllowFreeze { get => _allowFreeze; set => _allowFreeze = value; }
    public bool AllowChangeGravite { get => _allowChangeGravite; set => _allowChangeGravite = value; }
    public bool DestroyWhiteFire { get => _destroyWhiteFire; set => _destroyWhiteFire = value; }
    public bool AllowGrab { get => _allowGrab; set => _allowGrab = value; }
    public bool OnBurn { get => _onBurn; set => _onBurn = value; }
}
