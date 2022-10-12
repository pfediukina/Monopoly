using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailTile : BaseTile
{

    public override DialogInfo OnPlayerAtTile(UnitController unit, Board board)
    {
        DialogInfo d_info = new DialogInfo();
        //d_info.ID = DialogID.Jail;
        //d_info.Caption = tileInfo.Name;
        //if (unit.GetPlayerInfo().Jail == 0)
        //{
        //    d_info.Info = $"You are in jail. Wait 3 turns and try to roll a double.";
        //    unit.GetPlayerInfo().Jail = 3;
        //    d_info.Button1 = "OK";
        //    d_info.Button2 = "";
        //    return d_info;
        //}
        //int num1 = Random.Range(1, 7);
        //int num2 = Random.Range(1, 7);
        //unit.GetPlayerInfo().Jail--;
        //if (unit.GetPlayerInfo().Jail <= 0 && num1 != num2) // if 3rd roll
        //{
        //    d_info.Info = $"You waited 3 moves. You're out of jail";
        //}
        //else if (num1 == num2) // if double
        //{
        //    d_info.Info = $"You rolled {num1} and {num2}. You're out of jail";
        //    unit.GetPlayerInfo().Jail = 0;
        //}
        //else // if not 3rd and not double
        //{
        //    d_info.Info = $"You rolled {num1} and {num2}. You're not out of jail";
        //}

        //d_info.Button1 = "OK";
        //d_info.Button2 = "";
        return d_info;
    }
}
