using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class UnitController : MonoBehaviour
{
    [SerializeField]
    public int ID;
    public CameraFollow newCamera;
    public UnitInfo Info;
    public List<GameObject> Meshes;

    private UnitInfo _unitInfo;
    private Board _board;
    private UnitMovement _movement;

    public void Move(int step) { _movement.Move(step); }

    public void Init(Board gameBoard)
    {
        _unitInfo = Info;
        _board = gameBoard;

        ResetPlayer();
        _movement = GetComponent<UnitMovement>();
        _movement.Init(gameBoard, this);
        _movement.Move(0);
        newCamera.RotateCameraBySide(38, 0);
        
    }

    private void ResetPlayer()
    {
        _unitInfo.position = 0;
        _unitInfo.money = 1500;
        _unitInfo.jail = 0;
    }

    public UnitInfo GetUnitInfo()
    {
        return _unitInfo;
    }

    public void SetUnitInfo(UnitInfo info)
    {
        if (info == null) return;
        _unitInfo = info;
        foreach (var mesh in Meshes)
        {
            var tempMaterial = new Material(mesh.GetComponent<Renderer>().sharedMaterial);

            Color color = _unitInfo.color;
            tempMaterial.color = color;
            mesh.GetComponent<Renderer>().sharedMaterial = tempMaterial;
        }
    }

    public void SetCameraActive(bool active)
    {
        newCamera.gameObject.SetActive(active);
    }
}
