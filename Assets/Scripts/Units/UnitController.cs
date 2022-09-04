using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class UnitController : MonoBehaviour
{
    [SerializeField]
    public int ID;

    public UnitInfo _unitInfo;

    private Board board;
    private Vector3 _startPos = new Vector3(1.6f, 0.5f, 1.1f);

    public void Start()
    {

        //ћожем позволить себе убрать дрочь на ресурсы
        //var units = new List<UnitInfo>(Resources.FindObjectsOfTypeAll<UnitInfo>());
        //foreach(var unit in units)
        //{
        //    if(ID == unit.ID)
        //    {
        //        SetUnitInfo(unit);
        //        break;
        //    }
        //}
        SetUnitInfo(_unitInfo);
    }

    public UnitInfo GetUnitInfo()
    {
        return _unitInfo;
    }

    public void Init(Board gameBoard)
    {
        board = gameBoard;
        transform.position = board.GetTileCenter(0);
        _unitInfo.position = 0;
    }

    public void SetUnitInfo(UnitInfo info)
    {
        if (info == null) return;
        _unitInfo = info;  
    }

    public void Move(int step)
    {
        int lastPos = _unitInfo.position + step;
        transform.position = board.GetTileCenter(lastPos);
        _unitInfo.position = lastPos % 40;
    }
}
