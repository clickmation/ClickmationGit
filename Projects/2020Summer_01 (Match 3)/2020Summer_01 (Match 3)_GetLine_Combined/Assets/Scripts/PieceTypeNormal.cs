using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Piece/Type/Normal")]
public class PieceTypeNormal : PieceType
{
    public override void OnClear(GamePiece piece, Board board = null)
    {
        Destroy(piece.GetGameObject());
    }
}
