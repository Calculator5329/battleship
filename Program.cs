using System;

namespace thirdProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, int> letters = new Dictionary<char, int>() {
                {'A', 1},
                {'B', 2},
                {'C', 3},
                {'D', 4},
                {'E', 5},
                {'F', 6},
                {'G', 7},
                {'H', 8},
                {'I', 9},
                {'J', 10}
            };

            Board computerBoard = new Board();

            computerBoard.printDisplayMatrix();

            Random rnd = new Random();

            int cRow;
            int cColumn;

            // Adds ships to the computer board
            for(int i = 0; i < 5; i++) {
                cRow = rnd.Next(1, 10);
                cColumn = rnd.Next(1, 10);

                while(!computerBoard.insertShip(cRow, cColumn, rnd.Next(0, 3), i)){
                    cRow = rnd.Next(1, 10);
                    cColumn = rnd.Next(1, 10);
                }
            }

            Boolean play = true;
            int ammo = 0;
            int uRow;
            int uColumn;

            while(play) {
                Console.Write("Enter a spot to attack (Ex: A5) or press enter to exit: ");
                String input = Console.ReadLine() ?? "A5";
                input = input.ToUpper();

                if((input == "") || (input == "EXIT") || (input == " ") || input == "EX") {
                    break;
                }

                try {
                    uRow = letters[Convert.ToChar(input[0])] - 1;
                    uColumn =  int.Parse(input.Substring(1)) - 1;

                    string fireMsg = computerBoard.fire(uRow, uColumn);

                    computerBoard.printDisplayMatrix();

                    Console.WriteLine(fireMsg);
                    Console.WriteLine();

                    ammo += 1;
                    
                }
                // I dont need to catch each individual exception for now, but I might need to in the future. I added a general exception catch at the end just in case.
                catch(FormatException) {
                    Console.WriteLine("Error, please try again");
                }
                catch(IndexOutOfRangeException) {
                    Console.WriteLine("Error, please try again");
                }
                catch(KeyNotFoundException) {
                    Console.WriteLine("Error, please try again");
                }
                catch(ArgumentOutOfRangeException) {
                    Console.WriteLine("Error, please try again");
                }
                catch(NullReferenceException) {
                    Console.WriteLine("Error, please try again");
                }
                catch(Exception) {
                    Console.WriteLine("Error, please try again");
                }

                if(computerBoard.checkAllSunk()) {
                    Console.WriteLine("Congratulations! You have won!");
                    Console.WriteLine("Ammo used: " + ammo);
                    play = false;
                    Console.WriteLine("Press enter to exit the program");
                    Console.ReadLine();
                }
                
                
            }
            

        }
        
    }

}
