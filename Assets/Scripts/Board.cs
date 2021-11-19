using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{

    public Dictionary<GamePiece, Grid> playerPositions;
    public Dictionary<GamePiece, List<Moves>> playerMoves;
    public bool isCapturedMove;

    readonly List<Grid> kingDirections = new List<Grid>()
    {
        new Grid() { x = 1,y = 1 },
        new Grid() { x = 1,y = -1 },
        new Grid() { x = -1,y = 1 },
        new Grid() { x = -1,y = -1 }
    };

    readonly List<Grid> redDirections = new List<Grid>()
    {
        new Grid() { x = 1,y = 1 },
        new Grid() { x = -1,y = 1 }
    };

    readonly List<Grid> blueDirections = new List<Grid>()
    {
        new Grid() { x = 1,y = -1 },
        new Grid() { x = -1,y = -1 }
    };


    public Board()
    {
        playerPositions = new Dictionary<GamePiece, Grid>();
        Init();
    }

    void Init()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                GamePiece current = new GamePiece((Player)i, j);
                int x = (j * 2) % 8 + 1 - (j > 3 && j < 8 && i == 0?1:0) -(!(j > 3 && j < 8) && i == 1 ? 1 : 0);
                int y = (j/4) + (i == 1 ? 5 : 0);
                Grid tempGrid = new Grid() { x = x, y = y };
                playerPositions[current] = tempGrid;
            }
        }
    }

    public void CalculateMoves(Player currentPlayer)
    {
        playerMoves = new Dictionary<GamePiece, List<Moves>>();
        isCapturedMove = false;
        foreach (var item in playerPositions)
        {
            Grid itemGrid = item.Value;
            GamePiece currentPiece = item.Key;

            if (currentPiece.player != currentPlayer) continue;

            List<Grid> currentPieceDirection = currentPiece.pieceType == Constants.KING_PIECE ? kingDirections :
                                               currentPiece.player == Player.RED ? redDirections
                                               : blueDirections;

            List<Moves> tempMoves = new List<Moves>();

            foreach (Grid tempDirection in currentPieceDirection)
            {
                Grid toCheck = new Grid() { x = tempDirection.x + itemGrid.x, y = tempDirection.y + itemGrid.y };
                if(IsValidGrid(toCheck) && GetPieceAtGrid(toCheck).pieceNumber != -1 && 
                    GetPieceAtGrid(toCheck).player != currentPlayer)
                {
                    Grid doubleCheck = new Grid() { x = tempDirection.x + toCheck.x, y = tempDirection.y + toCheck.y };
                    if(IsValidGrid(doubleCheck) && GetPieceAtGrid(doubleCheck).pieceNumber == -1)
                    {
                        Moves tempMove = new Moves() { start = itemGrid, end = doubleCheck, 
                            isCapture = true, capturedPiece = GetPieceAtGrid(toCheck) };
                        tempMoves.Add(tempMove);
                        isCapturedMove = true;
                    }
                }
                else if(IsValidGrid(toCheck) && GetPieceAtGrid(toCheck).pieceNumber == -1)
                {
                    Moves tempMove = new Moves() { start = itemGrid, end = toCheck, isCapture = false };
                    tempMoves.Add(tempMove);
                }
            }

            if (tempMoves.Count > 0)
            {
                playerMoves[currentPiece] = tempMoves;
            }
        }

        if(isCapturedMove)
        {
            Dictionary<GamePiece, List<Moves>> tempDictionary = new Dictionary<GamePiece, List<Moves>>();

            foreach (var item in playerMoves)
            {
                List<Moves> tempMoves = new List<Moves>();
                foreach (var move in item.Value)
                {
                    if(move.isCapture)
                    {
                        tempMoves.Add(move);
                    }
                }
                if(tempMoves.Count > 0)
                {
                    tempDictionary[item.Key] = tempMoves;  
                }
            }

            playerMoves = tempDictionary;
        }
    }

    public GamePiece GetPieceAtGrid(Grid passedPosition)
    {
        foreach (var item in playerPositions)
        {
            if(item.Value.x == passedPosition.x && item.Value.y == passedPosition.y)
            {
                return item.Key;
            }
        }
        return new GamePiece(Player.RED,-1);
    }

    public void UpdateMove(Moves move)
    {
        GamePiece selected = GetPieceAtGrid(move.start);
        playerPositions[selected] = move.end;
        if(move.isCapture)
        {
            playerPositions.Remove(move.capturedPiece);
        }
    }

    public void UpgradePiece(GamePiece kingPiece)
    {
        Grid currentPlayerPos = playerPositions[kingPiece];
        playerPositions.Remove(kingPiece);
        kingPiece.pieceType = Constants.KING_PIECE;
        playerPositions[kingPiece] = currentPlayerPos;
    }
         

    public static bool IsValidGrid(Grid temp)
    {
        return temp.x >= 0 && temp.x < 8 && temp.y >= 0 && temp.y < 8;
    }
}
