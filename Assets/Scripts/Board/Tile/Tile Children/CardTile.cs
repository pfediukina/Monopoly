using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardTile : BaseTile
{
    private CardsInfo _cards;
    public override void Init()
    {
        base.Init();
        tileBuilder.SetNameOffset();
        _cards = Resources.Load<CardsInfo>("CardsInfo");
        //_cards = Resources.FindObjectsOfTypeAll<CardsInfo>().FirstOrDefault();
    }
    public override DialogInfo OnPlayerAtTile(UnitController unit, Board board)
    {
        tile_rent = 0;
        DialogInfo d_info = new DialogInfo();

        //bool is_chance = tileInfo.Type == TileType.Chance ? true : false;
        //int cardID = Random.Range(0, is_chance ? _cards.Chances.Count : _cards.Chests.Count);
        //CardInfo card = is_chance ? _cards.Chances[cardID] : _cards.Chests[cardID];
        //d_info.Info = card.Desc;
        //d_info.Button1 = "OK";
        //d_info.Button2 = "";
        //if (card.Type == CardType.Position)
        //{
        //    unit.MovePlayerToTile(card.Value);
        //    d_info.ID = DialogID.Chance;
        //}
        //else
        //{
        //    tile_rent = card.Value;
        //    d_info.ID = DialogID.Chest;
        //}
        return d_info;
    }
}
