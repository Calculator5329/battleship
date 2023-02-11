namespace thirdProject
{
    public class Board
    {
        private int[,] matrix = new int[10, 10];
        public string[,] displayMatrix = new string[10, 10];
        private int[] patrolBoat = {2, 2};
        private int[] submarine = {3, 3, 3};
        private int[] destroyer = {4, 4, 4};
        private int[] battleship = {5, 5, 5, 5};
        private int[] carrier = {6, 6, 6, 6, 6};
        private int[][] shipBlueprints;
        private string[] messages = {"You have already looked for a ship here!", "You missed!", "You have already hit a ship in this spot",
                                 "You hit a Patrol Boat!", "You hit a Submarine!", "You hit a Destroyer!", "You hit a Battleship!", "You hit a Carrier!"};

        public Board() {
             
            // Initialize blueprints
            shipBlueprints = new int[][] {patrolBoat, submarine, destroyer, battleship, carrier};

            // The board will be 10x10 with 0 representing a blank space, -1 representing a blank spot the player fired at,
            // 1 representing a spot you hit a ship, and 2-10 representing where a ship is.
            for(int i = 0; i < 10; i++) {
                for(int j = 0; j < 10; j++) {
                    matrix[i, j] = 0;
                    displayMatrix[i, j] = "_";
                }
            }

        }
        public string fire(int row, int column) {
            // This will implement fire function and return the resulting message
            string returnMsg = "";
            string[] messages = {"You have already looked for a ship here!", "Wasted ur ammo lol", "You have already hit a ship in this spot",
                                 "You hit a Patrol Boat!", "You hit a Submarine!", "You hit a Destroyer!", "You hit a Battleship!", "You hit a Carrier!",
                                 "You sunk the Patrol Boat!", "You sunk the Submarine!", "You sunk the Destroyer!", "You sunk the Battleship!", "You sunk the Carrier!"};

            switch(matrix[row, column]) {
                case -1:
                    returnMsg = messages[0];
                    break;
                case 0:
                    returnMsg = messages[1];
                    matrix[row, column] = -1;
                    displayMatrix[row, column] = "O";
                    break;
                case 1:
                    returnMsg = messages[2];
                    break;
                case 2:
                    returnMsg = messages[3];
                    matrix[row, column] = 1;
                    displayMatrix[row, column] = "P";
                    
                    // If you sink the ship, return a different message
                    if(checkSunk(0)) {
                        returnMsg = messages[8];
                    }
                    break;
                case 3:
                    returnMsg = messages[4];
                    matrix[row, column] = 1;
                    displayMatrix[row, column] = "S";

                    // If you sink the ship, return a different message
                    if(checkSunk(1)) {
                        returnMsg = messages[9];
                    }
                    break;
                case 4:
                    returnMsg = messages[5];
                    matrix[row, column] = 1;
                    displayMatrix[row, column] = "D";

                    // If you sink the ship, return a different message
                    if(checkSunk(2)) {
                        returnMsg = messages[10];
                    }
                    break;
                case 5:
                    returnMsg = messages[6];
                    matrix[row, column] = 1;
                    displayMatrix[row, column] = "B";

                    // If you sink the ship, return a different message
                    if(checkSunk(3)) {
                        returnMsg = messages[11];
                    }
                    break;
                case 6:
                    returnMsg = messages[7];
                    matrix[row, column] = 1;
                    displayMatrix[row, column] = "C";

                    // If you sink the ship, return a different message
                    if(checkSunk(4)) {
                        returnMsg = messages[12];
                    }
                    break;
                default:
                    returnMsg = "err";
                    break;
            }

            return returnMsg;
        }
        public bool insertShip(int row, int column, int direction, int shipType) {
            // direction 0=up, 1=right, 2=down, 3=left
            // shipType 0=patrolBoat, 1=submarine, 2=destroyer, 3=battleship, 4=carrier
            // This function will return true if insertion was succesful and false if not

            int[] shipBlueprint = shipBlueprints[shipType];

            switch(direction) {
                case 0:
                    //Insert facing up

                    // If the desired coords don't place the ship off of the board
                    if((row - shipBlueprint.Length) > 0) {
                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            // Checking if another ship is already at the coords, returns false if it finds a overlap
                            if(matrix[row - i, column] != 0) {
                                return false;
                            }
                        }
                        
                        // If we passed the previous checks, insert the ship and then return true
                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            matrix[row - i, column] = shipBlueprint[i];
                        }
                        return true;
                    }
                    // If the desired coords place the ship off of the board, flip the direction
                    else {
                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            // Checking if another ship is already at the coords, returns false if it finds a overlap
                            if(matrix[row + i, column] != 0) {
                                return false;
                            }
                        }

                        // If we passed the previous checks, insert the ship and then return true
                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            matrix[row + i, column] = shipBlueprint[i];
                        }
                        return true;
                    }
                case 1:
                    //Insert facing right

                    // If the desired coords don't place the ship off of the board
                    if((column + shipBlueprint.Length - 1) < Math.Sqrt(matrix.Length)) {
                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            // Checking if another ship is already at the coords, returns false if it finds a overlap
                            if(matrix[row, column + i] != 0) {
                                return false;
                            }
                        }

                        // If we passed the previous checks, insert the ship and then return true
                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            matrix[row, column + i] = shipBlueprint[i];
                        }
                        return true;
                    }
                    // If we can't insert the ship at the designated coords, flip the direction
                    else {

                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            // Checking if another ship is already at the coords, returns false if it finds a overlap
                            if(matrix[row, column - i] != 0) {
                                return false;
                            }
                        }

                        // If we passed the previous checks, insert the ship and then return true
                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            matrix[row, column - i] = shipBlueprint[i];
                        }
                        return true;
                    }
                case 2:
                    //Insert facing down

                    // If the desired coords don't place the ship off of the board
                    if(row + shipBlueprint.Length - 1 < Math.Sqrt(matrix.Length)) {
                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            // Checking if another ship is already at the coords, returns false if it finds a overlap
                            if(matrix[row + i, column] != 0 ) {
                                return false;
                            }
                        }

                        // If we passed the previous checks, insert the ship and then return true
                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            matrix[row + i, column] = shipBlueprint[i];
                        }
                        return true;
                    }
                    // If we can't insert the ship at the designated coords, flip the direction
                    else {

                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            // Checking if another ship is already at the coords, returns false if it finds a overlap
                            if(matrix[row - i, column] != 0) {
                                return false;
                            }
                        }

                        // If we passed the previous checks, insert the ship and then return true
                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            matrix[row - i, column] = shipBlueprint[i];
                        }
                        return true;
                    }
                case 3:
                    //Insert facing left
                    
                    // If the desired coords don't place the ship off of the board
                    if((column + shipBlueprint.Length) > 0) {
                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            // Checking if another ship is already at the coords, returns false if it finds a overlap
                            if(matrix[row, column - i] != 0) {
                                return false;
                            }
                        }

                        // If we passed the previous checks, insert the ship and then return true
                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            matrix[row, column - i] = shipBlueprint[i];
                        }
                        return true;
                    }
                    // If we can't insert the ship at the designated coords, flip the direction
                    else {
                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            // Checking if another ship is already at the coords, returns false if it finds a overlap
                            if(matrix[row, column + i] != 0) {
                                return false;
                            }
                        }
                        
                        // If we passed the previous checks, insert the ship and then return true
                        for(int i = 0; i < shipBlueprint.Length; i++) {
                            matrix[row, column + i] = shipBlueprint[i];
                        }
                        return true;
                    }
                default:
                    return false;
            }

        }
        public int[,] Matrix {
            get { return matrix;}
            set { matrix = value; }
        }
        public string[] Messages {
            get { return messages;}
            set { messages = value; }
        }
        public void printMatrix() {

            Console.WriteLine();
            char[] letterChars = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'};
            Console.WriteLine("  1 2 3 4 5 6 7 8 9 10");

            for(int row = 0; row < matrix.GetLength(0); row++) {
                string currentMsg = letterChars[row] +  " ";
                for(int column = 0; column < matrix.GetLength(1); column++) {
                    currentMsg += Convert.ToString(matrix[row, column]) + " ";
                }
                Console.WriteLine(currentMsg);
            }
            Console.WriteLine();
        }
        public void printDisplayMatrix() {
            Console.WriteLine();
            char[] letterChars = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'};
            Console.WriteLine("  1 2 3 4 5 6 7 8 9 10");

            for(int row = 0; row < displayMatrix.GetLength(0); row++) {
                string currentMsg = letterChars[row] +  " ";
                for(int column = 0; column < displayMatrix.GetLength(1); column++) {
                    currentMsg += Convert.ToString(displayMatrix[row, column]) + " ";
                }
                Console.WriteLine(currentMsg);
            }
            Console.WriteLine();
        }
        public bool checkSunk(int shipIndex) {
            // shipType 0=patrolBoat, 1=submarine, 2=destroyer, 3=battleship, 4=carrier
            // Representation in matrix: 2=patrolBoat, 3=submarine, 4=destroyer, 5=battleship, 6=carrier
            // This function will check if a specific type of ship is sunk

            int matrixIndex = shipIndex + 2;
            bool containsShip = false;
            
            for(int row = 0; row < matrix.GetLength(0); row++) {
                for(int column = 0; column < matrix.GetLength(1); column++) {
                    if(matrix[row, column] == matrixIndex) {
                        containsShip = true;
                    }
                }
            }

            return !containsShip;

        }
        public bool checkAllSunk() {
            // This function checks if all ships are sunk 

            bool containsShips = false;
            for(int row = 0; row < matrix.GetLength(0); row++) {
                for(int column = 0; column < matrix.GetLength(1); column++) {
                    if(matrix[row, column] > 1) {
                        containsShips = true;
                    }
                }
            }

            return !containsShips;
        }
    }
}