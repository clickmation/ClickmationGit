using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Piece/Type/VertClear")]
public class PieceTypeVertClear : PieceType
{
    public override void OnClear(GamePiece piece, Board board)
    {
        List<GamePiece> upwardPieces = board.GetLine(piece.xIndex, piece.yIndex, new Vector2(0, 2));
        List<GamePiece> downwardPieces = board.GetLine(piece.xIndex, piece.yIndex, new Vector2(0, -1));

        board.ClearPieceAt(upwardPieces.Union(downwardPieces).ToList());

        Destroy(piece.GetGameObject());
    }
}