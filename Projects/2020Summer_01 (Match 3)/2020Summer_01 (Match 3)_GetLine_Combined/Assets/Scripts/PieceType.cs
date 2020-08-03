using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceType : ScriptableObject
{
    public Sprite sprite;

    public abstract void OnClear(GamePiece piece, Board board = null);
}
