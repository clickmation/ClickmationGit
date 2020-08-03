using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceType : ScriptableObject
{
    public string pieceName;
    public Sprite pieceImage;

    public abstract void OnClear(GamePiece piece, Board board = null);
}
