using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "BoardInfo", menuName = "Info/BoardInfo", order = 2)]
public class BoardInfo : ScriptableObject
{
    [SerializeField]
    public List<TileInfo> _tileInfos;

    //Скриптбл Обж чтобы хранить инфу о тайлах и вставить реф в Борд
    public TileInfo GetTileByID(int id)
    {
        return _tileInfos.Find(x => x.ID == id);
    }
}
