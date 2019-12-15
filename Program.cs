using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureGame
{
    class Program
    {
      
        static void Main(string[] args)
        {

            CharacterGeneration.CharStats MainCharacter ;
            int choice;

            Console.WriteLine("welcome to the game, would you like to:");
            Console.WriteLine("1) generate a new character");
            Console.WriteLine("2) Load a character");
            choice = int.Parse(Console.ReadLine());


            switch (choice)
            {
                case 1:
                    MainCharacter = CharacterGeneration.SetupMainCharacter();
                    CharacterGeneration.DisplayStats(MainCharacter);
                    MainCharacter = FightClub.FightMechanic(MainCharacter);
                    break;
                case 2:
                    MainCharacter = CharacterGeneration.LoadCharacter();
                    CharacterGeneration.DisplayStats(MainCharacter);
                 
                    break;
                case 3:
                    Console.WriteLine("thank you for playing, goodbye");
                    break;
            }
            

            Console.ReadLine();

        }

        }
    }

