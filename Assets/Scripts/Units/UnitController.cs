using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class UnitController : MonoBehaviour
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public GameObject gameBoard;


    //------------------------------
    public int step;
    public float stepSize;
    public bool move = false; 
    public bool restore = false;
    //-------------------------

    //Прямой реф на Инфу пока что
    public UnitInfo _unitInfo;

    private sBoard board;
    private Vector3 _startPos = new Vector3(1.6f, 0.5f, 1.1f);

    public void Start()
    {
        board = gameBoard.GetComponent<sBoard>();

        //Можем позволить себе убрать дрочь на ресурсы
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

    public void Update()
    {
        if (move)
        {
            move = false;
            Move(step);             
        }
        if (restore)
        {
            restore = false;
            transform.position = board.GetTileCenter(0);
            _unitInfo.position = 0;        
        }
    }

    public void SetUnitInfo(UnitInfo info)
    {
        if (info == null) return;
        _unitInfo = info;  
    }

    public void Move(int step)
    {
        Vector3 lastPoint;
        int lastIndex = _unitInfo.position;
        float time = 1;
        for (int i = 1; i <= step; i++)
        {
            lastIndex = (_unitInfo.position + 1) % 40;
            lastPoint = board.GetTileCenter(lastIndex);
            Debug.Log(Vector3.Lerp(board.GetTileCenter(_unitInfo.position), lastPoint, time));
            transform.position = lastPoint;
        }
        _unitInfo.position = lastIndex;
    }

    //private Vector3 GetOffset()
    //{
    //    Vector3 offset = new Vector3(0, 0, 0);
    //    if (_unitInfo.position > 0 && _unitInfo.position < 10)
    //    {
    //        offset.x = stepSize;
    //        offset.z = 0;
    //    }
    //    else if (_unitInfo.position >= 10 && _unitInfo.position < 20)
    //    {
    //        offset.x = 0;
    //        offset.z = -stepSize;
    //    }
    //    else if (_unitInfo.position >= 20 && _unitInfo.position < 30)
    //    {
    //        offset.x = -stepSize;
    //        offset.z = 0;
    //    }
    //    else
    //    {
    //        offset.x = 0;
    //        offset.z = stepSize;
    //    }
    //    return offset;
    //}
}
