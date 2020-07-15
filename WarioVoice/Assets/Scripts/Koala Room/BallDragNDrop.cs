using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallDragNDrop : MonoBehaviour
{
    private bool _isDraggingBall = false;
    private Vector3 _startingPosition = Vector3.zero;
    [SerializeField] private Canvas myCanvas;
    private bool _canBallBeDragged = true;
    private bool _isMouseOnTito = false;

    public bool IsDraggingBall { get => _isDraggingBall; set => _isDraggingBall = value; }
    public bool CanBallBeDragged { get => _canBallBeDragged; set => _canBallBeDragged = value; }


    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = gameObject.transform.position;      
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDraggingBall)
        {
            Vector3 MousePosition = Input.mousePosition;
            //gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(MousePosition.x, MousePosition.y, 0));
            Vector3 pos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            gameObject.transform.position = pos;
            Debug.Log(gameObject.transform.localPosition);
        }
    }

    public void startDrag()
    {
        if (CanBallBeDragged)
        {
            IsDraggingBall = true;
        }
    }

    public void endDrag()
    {
        if (CanBallBeDragged)
        {
            gameObject.transform.position = _startingPosition;
            IsDraggingBall = false;
            if (_isMouseOnTito)
            {
                FindObjectOfType<MoodActionsController>().playWithTito();
            }
        }
    }

    public void mouseOnTiTo()
    {
        _isMouseOnTito = true;
    }

    public void mouseOffTiTo()
    {
        _isMouseOnTito = false;
    }
}
