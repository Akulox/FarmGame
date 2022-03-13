using System;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public GameObject joystickBase;
    public GameObject joystick;

    public Player player;

    private Vector2 _startPosition;
    private float joystickRadius = 75f;
    private bool _isDrag;
    private bool _wasDragged;


    public void OnStartDrag()
    {
        _isDrag = true;
        joystick.GetComponent<RectTransform>().position = joystickBase.GetComponent<RectTransform>().position = _startPosition = Input.mousePosition;
        joystickBase.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void OnDrag()
    {
        if (!_wasDragged)
        {
            _wasDragged = true;
        }
        PositionJoystick();
    }

    public void OnEndDrag()
    {
        if (_wasDragged)
        {
            _wasDragged = false;
        }
        else
        {
            player.Swing();
        }
        _isDrag = false;
        joystickBase.GetComponent<CanvasGroup>().alpha = 0;
    }
    
    private float AngleToVector2(Vector2 b)
    {
        float signedAngle = (float)(Math.Atan2(_startPosition.y - b.y, _startPosition.x - b.x) / Math.PI * 180);
        signedAngle = signedAngle < 0 ? signedAngle + 360 : signedAngle;
        return signedAngle;
    }

    private float GetAngleToTouch()
    {
        return AngleToVector2(Input.mousePosition);
    }
    
    private void PositionJoystick()
    {
        float angle = GetAngleToTouch();
        joystick.GetComponent<RectTransform>().position = 
            Radius() > 1 ? 
                new Vector2(_startPosition.x - (float)Math.Cos(angle * Math.PI / 180) * joystickRadius, _startPosition.y - (float)Math.Sin(angle * Math.PI / 180) * joystickRadius) : 
                new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    private float Radius()
    {
        return (float)
            ((Math.Pow(Input.mousePosition.x - _startPosition.x, 2) + 
              Math.Pow(Input.mousePosition.y - _startPosition.y, 2)) / 
             Math.Pow(joystickRadius, 2));
    }
    
    private void Update()
    {
        if (_isDrag & Radius() > 0.1f && !player.playerAnimationManager.isSwinging)
        {
            if (!player.isWalking)
            {
                player.StartWalk();
            }
            player.WalkInDir(GetAngleToTouch());
        }
        else
        {
            player.StopWalk();
        }
    }
}
