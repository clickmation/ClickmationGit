using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Piece/Type/HorUpClear")]
public class PieceTypeHorUpClear : PieceType
{
    public override void OnClear(GamePiece piece, Board board)
    {
        List<GamePiece> upwardPieces = board.GetLine(piece.xIndex, piece.yIndex, new Vector2(1, 1));
        List<GamePiece> downwardPieces = board.GetLine(piece.xIndex, piece.yIndex, new Vector2(-1, 0));

        board.ClearPieceAt(upwardPieces.Union(downwardPieces).ToList());

        Destroy(piece.GetGameObject());
    }
}