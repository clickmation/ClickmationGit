using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Piece/Type/SurroundClear")]
public class PieceTypeSurroundClear : PieceType
{
    public override void OnClear(GamePiece piece, Board board)
    {
        List<Vector2Int> surroundingPieces = board.GetSurrounding(piece.xIndex, piece.yIndex);

        board.AddToGroup(surroundingPieces);

        Destroy(piece.GetGameObject());
    }
}
