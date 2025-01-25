using ChessChallenge.API;
using ChessChallenge.Chess.Result
using static ChessChallenge.Application.ConsoleHelper;
using Math
using ChessChallenge.Chess.MoveGeneration.MoveGenerator

public class MyBot : IChessBot
{
    // Piece values: null, pawn, knight, bishop, rook, queen, king
    double[] pieceValues = { 0, 1.04, 3.905, 4.125, 6.38, 12.69, 100000 }; // thanks stockfish
    int moveNum = board.PlyCount;
    public Move Think(Board board, Timer timer)
    {
        

        Move[] moves = board.GetLegalMoves();
        return new Move("e2e4", board);
    }
    double eval(Board board){
        double eval = 0;
        for(int i = 1; i = 6, i++;){
            PieceList[] white = board.getPieceList(i, true);
            PieceList[] black = board.getPieceList(i, true);
            for(PieceList piece in white){
                eval += pieceValues{i}
            }
            for(PieceList piece in black){
                eval -= pieceValues{i}
            }
        }

        if(board.whiteToMove){ // change for middle/endgame???
            eval+=.5
        }else{
            eval-=.5 // change for middle/endgame???
        }

        for(legalMove in sliding/knights)

        // center control   number of attackers/pawns in the center 4
        // king safety/pressure open   file, safe to back ranks?
        // connected rooks (check if any pieces are between them and same file)
        // development (early game)
        // mates 3/4ish nodes
        // piece mobility   available move locations
        // pawn structure (only doubled??)
        // pieces safe and arent able to be trapped (2 nodes)
        // piece positioning (eyeball or steal)
        // passed pawns (mid-endgame)
        // can force a tie (3 nodes)
        // attacked pieces
        // check that the move chosen doesnt blunder mate (mateSoon() with the move made on the board)

    }

    double evalWithMate(Board board){
        double mate = mateSoon();
        bool tie = canForceTie();
        if(mate != 0){
            return mate;
        }
    }

    bool canForceTie(Board board, int depth){

    }

    double mateSoon(Board board, int depth){ // return 1 for white and -1 for black
        Move[] moves = board.GetLegalMoves();
        Arbiter arbiter = new Arbiter();
        GameResult gameState = arbiter.GetGameState(board);
        if(depth == 0 || gameState != GameResult.InProgress){
            return 0; // no mate or no reason to search further
        }
        double value;
        foreach(Move move in moves){
            board.makeMove(move);
            gameState = arbiter.GetGameState();
            if(gameState == GameResult.InProgress){
                // find if there was a line that guarentees mate
            }else if(gameState != GameResult.WhiteIsMated && gameState != gameState.BlackIsMated){
                return 0;
            }
            if(board.IsWhiteToMove){
                return -1 // white was just mated by black
            }
            return 1 // black was just mated by white
        }
        return value
    }
}