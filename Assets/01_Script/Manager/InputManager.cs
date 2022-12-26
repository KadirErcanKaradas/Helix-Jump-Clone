using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{ 
    private Vector2 _firstPos;
    private Vector2 _secondPos;
    private Vector2 _currentPos;
    public Vector2 MoveFactor => _currentPos;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _firstPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButton(0))
        {
            _secondPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
  
            _currentPos = new Vector2(
                _secondPos.x - _firstPos.x,
                _secondPos.y - _firstPos.y
            );
            _currentPos = _currentPos.normalized;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _currentPos = Vector2.zero;
        }
          
    }
}