using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece
{
    public Player player { get; private set; }
    public int pieceNumber { get; private set; }

    public string pieceType;
    
    public GamePiece(Player tempPlayer, int temp)
    {
        player = tempPlayer;
        pieceNumber = temp;
        pieceType = Constants.NORMAL_PIECE;
    }

}
