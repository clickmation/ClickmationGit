using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Piece/Type/HorDownClear")]
public class PieceTypeHorDownClear : PieceType
{
    public override void OnClear(GamePiece piece, Board board)
    {
        List<Vector2Int> upwardPieces = board.GetLine(piece.xIndex, piece.yIndex, new Vector2(1, 0));
        List<Vector2Int> downwardPieces = board.GetLine(piece.xIndex, piece.yIndex, new Vector2(-1, 1));

        board.AddToGroup(upwardPieces.Union(downwardPieces).ToList());

        Destroy(piece.GetGameObject());
    }
}