using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player { RED,BLUE };

public struct Grid { public int x, y; };

public struct Moves { public Grid start, end; public bool isCapture; public GamePiece capturedPiece; };

public static class Constants
{
    public const string CLICK = "CLICK";
    public const string MOVE = "MOVE";
    public const string FINISHED = "FINISHED";
    public const string NORMAL_PIECE = "NORMAL";
    public const string KING_PIECE = "KING";
}
