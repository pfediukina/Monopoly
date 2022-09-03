using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UnitController : MonoBehaviour
{
    public int ID;
    public GameObject gameBoard;
<<<<<<< Updated upstream


    //------------------------------
    public int step;
    public float stepSize;
    public bool move = false; 
    public bool restore = false;
    //-------------------------

    private sBoard board;
    private UnitInfo _unitInfo;
    private Vector3 _startPos = new Vector3(1.6f, 0.5f, 1.1f);

    public void Start()
    {
        board = gameBoard.GetComponent<sBoard>();
        var units = new List<UnitInfo>(Resources.FindObjectsOfTypeAll<UnitInfo>());
        foreach(var unit in units)
        {
            if(ID == unit.ID)
            {
                SetUnitInfo(unit);
                break;
            }
        }
    }
=======
    public UnitInfo _unitInfo;

    private sBoard board;
    //---------------------
    private List<GameObject> _lines;
>>>>>>> Stashed changes

    public void InitPlayer(sBoard newBoard)
    {
        board = newBoard;
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
        
        int _endPos = _unitInfo.position + step;
        int _startPos = _unitInfo.position;
        for(int i = _startPos; i <= _endPos; i++)
        {
            _unitInfo.position = i;
            transform.position = board.GetTileCenter(_unitInfo.position);
        }
        //Debug.Log("Pos: " + _startPos + "/" + _unitInfo.position);
        _unitInfo.position = _unitInfo.position % 40;
    }
}
