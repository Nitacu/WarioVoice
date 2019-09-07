using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcelotMovements : MonoBehaviour
{

    [SerializeField] private float VELOCITY_WALL = 1;
    [SerializeField] private float VELOCITY_RUN = 2;

    [SerializeField] private bool _allowMove = false;
    [SerializeField] private bool _nearToObject = false; // para saber si esta cerca a un obstaculo

    private float _velocity = 0;
    private float _forceJump = 300;
    //variables para el salto
    [SerializeField] private float _time = 0;
    private float _rateVelocity;
    [SerializeField] private GameObject _nearToCollision;
    [SerializeField] private bool _inJump = false;
    [SerializeField] private GameObject _grabObj;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        _anim.SetFloat("Velocity", _velocity);

        if (AllowMove && !NearToObject)
        {
            transform.Translate(Vector2.right * Time.deltaTime * _velocity);
        }
        else if (InJump && NearToObject)
        {
            jump();
        }
    }

    public void walkForward()
    {
        if (PlayerGrimoire.GetInstance()._walkForward)
        {
            if (transform.rotation.y == -1)
                GetComponent<Transform>().Rotate(new Vector3(0, -180, 0)); //lo resta lo que tenia antes para volver al origen

            AllowMove = true;
            _velocity = VELOCITY_WALL;
        }
    }

    public void walkBackwards()
    {

        if (PlayerGrimoire.GetInstance()._walkBackwards)
        {
            if (transform.rotation.y != -1)
                GetComponent<Transform>().Rotate(new Vector3(0, 180, 0));

            AllowMove = true;
            _velocity = VELOCITY_WALL;
        }
    }

    public void idle()
    {
        if (PlayerGrimoire.GetInstance()._stayPut)
        {
            AllowMove = false;
            _velocity = 0;
        }
    }

    public void stopMove()
    {
        if (!InJump)
        {
            AllowMove = false;
            _velocity = 0;

        }
    }

    public void move()
    {
        if (PlayerGrimoire.GetInstance()._wall)
        {
            if (!_nearToObject)
            {
                if (transform.rotation.y == -1)
                    GetComponent<Transform>().Rotate(new Vector3(0, -180, 0)); //lo resta lo que tenia antes para volver al origen

                AllowMove = true;
                _velocity = VELOCITY_WALL;

            }
        }
    }

    public void run()
    {
        if (PlayerGrimoire.GetInstance()._run)
        {
            if (!_nearToObject)
            {
                AllowMove = true;
                _velocity = VELOCITY_RUN;
            }
        }
    }

    public void jump()
    {
        if (PlayerGrimoire.GetInstance()._jump)
        {
            if (!NearToObject)
            {

                AllowMove = false;
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 150);
            }
            else
            {
                // mira si tiene el script luego mira si lo puede saltar
                if ((NearToCollision.GetComponent<EnchantableObj>().TopPoint.position.y - transform.position.y) <= 2.5f)
                {
                    InJump = true;
                    _rateVelocity = 1 / Vector3.Distance(transform.position, NearToCollision.GetComponent<EnchantableObj>().TopPoint.position) * 0.5f;
                    if (_time == 0)
                    {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.up * _forceJump);
                    }

                    if (_time <= 1)
                    {
                        _time += Time.deltaTime * _rateVelocity;
                        AllowMove = true;
                        transform.position = Vector3.Lerp(transform.position, NearToCollision.GetComponent<EnchantableObj>().TopPoint.position, _time);

                    }
                    else
                    {
                        InJump = false;
                        _time = 0;
                        _nearToObject = false;
                        NearToCollision = null;
                        AllowMove = false;
                    }

                }
            }

            _anim.SetBool("Jump", InJump);
            _anim.SetFloat("Time_Jump", _time);
        }
    }

    public void grab()
    {
        if (PlayerGrimoire.GetInstance()._grabObj)
        {
            if (GrabObj)
            {
                GetComponent<OcelotProperties>()._hasObj = true;
                GrabObj.GetComponent<EnchantableObj>().followCat();
            }
        }
    }

    public bool NearToObject { get => _nearToObject; set => _nearToObject = value; }
    public GameObject NearToCollision { get => _nearToCollision; set => _nearToCollision = value; }
    public GameObject GrabObj { get => _grabObj; set => _grabObj = value; }
    public bool InJump { get => _inJump; set => _inJump = value; }
    public bool AllowMove { get => _allowMove; set => _allowMove = value; }
}
