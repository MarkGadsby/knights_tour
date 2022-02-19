CKnightsTour knights_tour = new CKnightsTour();
knights_tour.find_closed_tour();

class CKnightsTour
{
    // the board dimensions
    private const int BOARD_DIM = 8;

    // array of tuples representing all eight of 
    // the possible moves a knight can make
    (int x, int y)[] poss_moves = { (+1,-2), 
                                    (+2,-1),
                                    (+2,+1), 
                                    (+1,+2),
                                    (-1,+2),
                                    (-2,+1),
                                    (-2,-1),
                                    (-1,-2) };

    // 2d int array to represent the board  
    private int[,] chess_board;
    // tuple to repesent the current square    
    (int x, int y) current_square; 
    // steps of the tour
    int step_number;

    ///////////////////////////////////////////////////////////////////////////
    // Constructor       
    //
    ///////////////////////////////////////////////////////////////////////////
    public CKnightsTour()
    {
        // instantiate and initialise the chess board squares to -1
        chess_board = new int[BOARD_DIM, BOARD_DIM];
        
        for (int x = 0; x < BOARD_DIM; x++)
        {
            for (int y = 0; y < BOARD_DIM; y++)
            {
                chess_board[x,y] = -1;
            }
        }

        // set the current square
        current_square = (0, 0);
        step_number = 1;
        mark_move();
    }

    ///////////////////////////////////////////////////////////////////////////
    // Helper function to check if the proposed move is still within the bounds 
    // of the chess board
    //
    ///////////////////////////////////////////////////////////////////////////
    private bool on_the_board((int x,int y) onward_move)
    {
        if (onward_move.x >= 0 && 
            onward_move.x < BOARD_DIM &&
            onward_move.y >= 0 && 
            onward_move.y < BOARD_DIM)
            {
                return true;
            }

        return false;
    }
 
    ///////////////////////////////////////////////////////////////////////////
    // Helper function to check if the proposed onward move is to an unvisited
    // square
    //
    ///////////////////////////////////////////////////////////////////////////
    private bool free_square((int x,int y) onward_move)
    {
        if (chess_board[onward_move.x, onward_move.y] == -1)
        {
            return true;            
        }        
        return false;            
    }

    ///////////////////////////////////////////////////////////////////////////
    // Helper function to return the number of possible onward moves available 
    // from the candidate square
    //
    ///////////////////////////////////////////////////////////////////////////
    private int get_num_of_possible_moves((int x,int y)candidate_square)
    {
        // check the candidate square is valid itself first 
        if (!on_the_board(candidate_square) || !free_square(candidate_square))
            return poss_moves.Length;

        // then count all the valid possible onward moves from the candidate square
        int count = 0;

        for (int i = 0; i < poss_moves.Length; i++)
        {
            (int,int) onward_move = (candidate_square.x + poss_moves[i].x, candidate_square.y + poss_moves[i].y);                 

            if (on_the_board(onward_move) && free_square(onward_move))
                count++;
        }
        return count;
    }

    ///////////////////////////////////////////////////////////////////////////
    // Find and take the next move using Warnsdorff's heuristic
    //
    // Warnsdorff's rule is a heuristic for finding a single knight's tour. 
    // The knight is moved so that it always proceeds to the square from 
    // which the knight will have the fewest onward moves. 
    ///////////////////////////////////////////////////////////////////////////
    private bool take_next_step()
    {
        int min_moves = poss_moves.Length;
        int min_moves_index = -1;
        int current_result = 0;
        (int x, int y) candidate_square;

        for (int i = 0; i < poss_moves.Length; i++)
        {
            // try out each possible move from the current square
            candidate_square.x = current_square.x + poss_moves[i].x;
            candidate_square.y = current_square.y + poss_moves[i].y;

            current_result = get_num_of_possible_moves(candidate_square);

            if (current_result < min_moves)
            {
                min_moves = current_result;
                min_moves_index = i;
            }
        }

        if (min_moves_index == -1)
            return false;

        // make move
        current_square.x = current_square.x + poss_moves[min_moves_index].x;
        current_square.y = current_square.y + poss_moves[min_moves_index].y;
        mark_move();
      
        return true;
    }

    ///////////////////////////////////////////////////////////////////////////
    // Helper method to stamp the current step in the current square 
    //
    ///////////////////////////////////////////////////////////////////////////
    private void mark_move()
    {
        chess_board[current_square.x, current_square.y] = step_number++;
    }

    ///////////////////////////////////////////////////////////////////////////
    // Helper method to display the chess board on the screen
    //
    ///////////////////////////////////////////////////////////////////////////
    private void print_chess_board()
    {
        for (int x = 0; x < BOARD_DIM; x++)
        {
            Console.Write("\n");
            for (int y = 0; y < BOARD_DIM; y++)
            {
                Console.Write(chess_board[x,y]);
                Console.Write("\t");
            }
            Console.Write("\n");
        }
        Console.Write("\n");
    }

    ///////////////////////////////////////////////////////////////////////////
    // Access method to complete the knights tour and print result
    //
    ///////////////////////////////////////////////////////////////////////////
    public void find_closed_tour()
    {
        while (take_next_step())
        {
        }
        print_chess_board();
    }
} 