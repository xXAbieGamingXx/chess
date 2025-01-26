using ChessChallenge.API;
using ChessChallenge.Application.APIHelpers;
using ChessChallenge.Chess;
public class MyBot : IChessBot
{
    // Piece values: null, pawn, knight, bishop, rook, queen, king
    double[] pieceValues = { 0, 1.04, 3.905, 4.125, 6.38, 12.69, 100000 }; // thanks stockfish
    int moveNum;
    public ChessChallenge.API.Move Think(ChessChallenge.API.Board board, Timer timer)
    {
        
        moveNum = board.PlyCount;
        ChessChallenge.API.Move[] moves = board.GetLegalMoves();
        Eval(board);

        return new ChessChallenge.API.Move("e2e4", board);
    }
    double Eval(ChessChallenge.API.Board board){
        double eval = 0;
        PieceType pieceType = new PieceType();
        for(int i = 2; i == 6; i++){
            if(i == 2){
                pieceType = PieceType.Knight;
            }else if(i == 3){
                pieceType = PieceType.Bishop;
            }else if(i == 4){
                pieceType = PieceType.Rook;
            }else{
                pieceType = PieceType.Queen;
            }
            ChessChallenge.API.PieceList white = board.GetPieceList(pieceType, true);
            ChessChallenge.API.PieceList black = board.GetPieceList(pieceType, true);
            foreach(Piece piece in white){
                eval += pieceValues[i];
            }
            foreach(Piece piece in black){
                eval -= pieceValues[i];
            }
        }

        if(board.IsWhiteToMove){ // change for middle/endgame???
            eval+=.5;
        }else{
            eval-=.5; // change for middle/endgame???
        }

        //for(legalMove in sliding/knights)

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
        return 0;
    }

    double evalWithMate(ChessChallenge.API.Board board, ChessChallenge.Chess.Board resultBoard){
        double mate = mateSoon(board, resultBoard, 3);
        bool tie = canForceTie(board, 3);
        if(mate != 0){
            return mate;
        }
        return 0;
    }

    bool canForceTie(ChessChallenge.API.Board board, int depth){
        return false;
    }

    double mateSoon(ChessChallenge.API.Board board, ChessChallenge.Chess.Board resultBoard, int depth){ // return 1 for white and -1 for black
        ChessChallenge.API.Move[] moves = board.GetLegalMoves();
        GameResult gameState = Arbiter.GetGameState(resultBoard);
        if(depth == 0 || gameState != GameResult.InProgress){
            return 0; // no mate or no reason to search further
        }
        double value = 0;
        foreach(ChessChallenge.API.Move move in moves){
            board.MakeMove(move);
            gameState = Arbiter.GetGameState(resultBoard);
            if(gameState == GameResult.InProgress){
                // find if there was a line that guarentees mate
            }else if(gameState != GameResult.WhiteIsMated && gameState != GameResult.BlackIsMated){
                return 0;
            }
            if(board.IsWhiteToMove){
                return -1; // white was just mated by black
            }
            return 1; // black was just mated by white
        }
        return value;
    }
}