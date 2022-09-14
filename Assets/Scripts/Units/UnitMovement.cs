using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private UnitController _unit;
    private CameraFollow _unitCamera;
    private Board _board;


    public void Init(Board board, UnitController unit)
    {
        _unitCamera = GetComponentInChildren<CameraFollow>();
        if (_unitCamera == null)
            Debug.Log("Error Camera");
        _board = board;   
        _unit = unit;
        _unitCamera.SetOffset(new Vector3(0, 5, 4));
    }

    public void Move(int step)
    {
        var unitInfo = _unit.GetUnitInfo();
        int newPos = unitInfo.position + step;
        if(_unitCamera)
            _unitCamera.RotateCameraBySide(unitInfo.position, newPos);
        unitInfo.position = newPos % 40;
        transform.position = _board.GetTile(newPos).GetPlayerVectorPos(_unit);
        _unit.SetUnitInfo(unitInfo);
        
    }
}
