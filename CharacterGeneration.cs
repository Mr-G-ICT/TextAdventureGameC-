﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace TextAdventureGame
{
    class CharacterGeneration
    {
        //structure to be used for all characters
        public struct CharStats
        {
            public string CharacterName;
            public string CharacterRace;
            public string CharacterClass;
            public int Strength;
            public int Dexterity;
            public int Constitution;
            public int Intelligence;
            public int Wisdom;
            public int Charisma;

            //constructor for the charstats, but 
            public CharStats(string CName, string CClass, string CRace, int CStrength, int CDexterity, int CConstitution, int CIntelligence, int CWisdom, int CCharisma)
                {
                //stats are set to default as per D&D rules V5, re-rolled later
                CharacterName = CName;
                CharacterClass = CClass;
                CharacterRace = CRace;
                Strength = CStrength;
                Dexterity = CDexterity;
                Constitution = CConstitution;
                Intelligence = CIntelligence;
                Wisdom = CWisdom;
                Charisma = CCharisma;
            }

        };

        //decared this as random so that the random number generator is more random
        public static Random dice = new Random();

        public static CharStats SetupMainCharacter()
        {
            CharStats MainCharacter;
            string[] Line = new string[8];
            int choice = 0;

            string[] RaceChoices = { "Human", "Dwarf", "Elf", "Orc", "Zombie" };

            //gonna set up a database eventually to populate this 
            string[,] RaceBonuses = { { "Dw", "Con", "2" }, { "El", "Dex", "2" }, { "Hu", "All", "1" }, { "Or", "Str", "2" }, { "Or", "Con", "2" }, { "Zo", "Con", "2" } };


            string[] ClassChoices = { "Paladin", "Hunter", "Archer", "Warrior", "Necromancer" };
            string[,] ClassBonuses = { {"Pal"," "," " }};

            //Set up the character with default statistics
            MainCharacter = MainCharacter = new CharStats("", "", "", 15, 14, 13, 12, 10, 8);
            MainCharacter.CharacterName = ChooseCharacterName();
            MainCharacter.CharacterRace = ChooseCharacterRaceOrClass(RaceChoices, "Race");
            MainCharacter.CharacterClass = ChooseCharacterRaceOrClass(ClassChoices, "Class");

            Console.WriteLine("do you want to 1) generate your own stats or go with the 2) default?");
            choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                MainCharacter.Strength = GenerateStats();
                MainCharacter.Dexterity = GenerateStats();
                MainCharacter.Constitution = GenerateStats();
                MainCharacter.Intelligence = GenerateStats();
                MainCharacter.Wisdom = GenerateStats();
                MainCharacter.Charisma = GenerateStats();
            }
            MainCharacter = AddRacialBonuses(MainCharacter, RaceBonuses);
            return MainCharacter;

        }

        public static CharStats LoadCharacter()
        {
            /**************************************************/
            /*Name: Load Character
            /* Description: opens a file, stores all the data into 
            /* an array for then putting into the structure later
            /*Inputs: None
            /* output: Line to initialise, then sends the final main character back to the main program
             * Extends: to initialise character where the data taken from the file is placed in the structure 
            /****************************************************/
            
            const string FILE_NAME = "E:\\StMarys2018\\Year 12 Computing - Programming\\TextAdventureGame\\gamefile.txt";
            //get all the data into an array, to put in a structure later.
            string[] Line = new string[9];
            int Count = 0;

            FileStream LinkToFile = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read);
            StreamReader FileReader = new StreamReader(LinkToFile);

            FileReader.BaseStream.Seek(0, SeekOrigin.Begin);


            if (File.Exists(FILE_NAME) == true)
            {
                while ((FileReader.Peek() != -1) || (Count < 9))
                {
                    Line[Count] = FileReader.ReadLine();
                    Count = Count + 1;
                }
            }
            FileReader.Close();
            //call the charstats constructor and set up the character
            return ( new CharStats(Line[0], Line[1], Line[2], int.Parse(Line[3]), int.Parse(Line[4]), int.Parse(Line[5]), int.Parse(Line[6]), int.Parse(Line[7]), int.Parse(Line[8])));
        }

  
     

        private static string ChooseCharacterName()
        {
            /***********************************************************/
            /* Name: CharacterCreation
            /* Description:
            /* Inputs: NONE
            /* Ouputs: The Character Name
            /* 
            /* EXTRA STUFF: build in some exception handling, however this should be good
            /**********************************************************/

            string CharName = "";

            //simple loop to check if the name is present and it is more than 2 characters
            do
            {
                Console.WriteLine("please enter the name of your character");
                CharName = Console.ReadLine();

                //if statement to display error message to users
                if ((CharName == "") && (CharName.Length < 2))
                {
                    Console.WriteLine("character names must be at least 2 characters");
                }
            } while ((CharName == "") && (CharName.Length < 2));


            return CharName;
        }

        public static string ChooseCharacterRaceOrClass(string[] ArrayOfValues, string Selector)
        {
            /***********************************************************/
            /* Name: ChooseCharacterRaceOrClass
            /* Description:chooses the character race
            /* Inputs: the array containing all the potential choices, the parameter telling which to use
            /* Ouputs: The Character Race or class, dependent on set parameter
            /* 
            /**********************************************************/


            int Choice = -1;
            int Count = 0;
            string ChoiceString = "";   //included for error handling

            Console.WriteLine("please choose your");

            //display the contents of the array for the user to choose
            while (Count <= ArrayOfValues.Length - 1)
            {
                Console.WriteLine(Count + ") " + ArrayOfValues[Count]);
                Count = Count + 1;
            }


            //loop until valid data entered
            do
            {

                //error handler 1, check if the data entered is an int 
                do
                {
                    ChoiceString = Console.ReadLine();
                } while (!int.TryParse(ChoiceString, out Choice));


                //error handler 2, displays error messages if data is incorrect
                if ((Choice < 0) || (Choice > ArrayOfValues.Length - 1))
                {
                    Console.WriteLine("invalid race entered, please try again");
                }


                //passing these checks, convert the string to an int for processing
                if (int.TryParse(ChoiceString, out Choice))
                {
                    Choice = int.Parse(ChoiceString);
                }

            } while ((Choice < 0) || (Choice > ArrayOfValues.Length - 1));

            return ArrayOfValues[Choice];
        }

        private static int GenerateStats()
        {
            /***********************************************************/
            /* Name: GenerateStats
            /* Description: Generates the statistics based on standard D&D rules
            /* Inputs: 
            /* Ouputs: the Stat for D&D
            /* 
            /**********************************************************/

            int GeneratedStat = 0;
            int min = 7;
            int count = 0;
            int total = 0;

            //loop as D&D rolls 4 dice then takes the 3 largest values to form the statistic
            while (count < 4)
            {
                //roll a dice
                GeneratedStat = dice.Next(1, 7);

                //calculate the minimum to take away at the end
                if (min > GeneratedStat)
                {
                    min = GeneratedStat;
                }
                total = total + GeneratedStat;

                count = count + 1;
            }

            //take the min away, so you have the top 3 dice
            total = total - min;

            return total;
        }
        private static CharStats AddRacialBonuses(CharStats MainCharacter, string[,] RaceBonuses)
        {
            /***************************************************************
            /*Name: Add Racial Bonuses
            /*Description:
            /*Inputs: the main character stats, the bonuses for each of the races
            /*Outputs: the updated main character statistics
            /* Next Steps: set up the loop so it goes through every race bonus
            /*     Functionalise the bit that adds the bonus to the main character 
            /*****************************************************************/

            //take the first two characters out to relate to the RaceBonuses
            string CharRace = MainCharacter.CharacterRace.Substring(0, 2);
            int Count = 0;
            Console.WriteLine("array length" + RaceBonuses.GetLength(0));
           
            
            //Set up a loop to go through the whole racebonuses array.
            while (Count < RaceBonuses.GetLength(0))
            {
                if (RaceBonuses[Count, 0] == CharRace)
                {
                    //switch statement needs refining for this. probably class it
                    switch (RaceBonuses[Count, 1])
                    {
                        case "Str":
                            MainCharacter.Strength = MainCharacter.Strength + int.Parse(RaceBonuses[Count, 2]);
                            break;
                        case "Dex":
                            MainCharacter.Dexterity = MainCharacter.Dexterity + int.Parse(RaceBonuses[Count, 2]);
                            break;
                        case "Con":
                            MainCharacter.Constitution = MainCharacter.Constitution + int.Parse(RaceBonuses[Count, 2]);
                            break;
                        case "Int":
                            MainCharacter.Intelligence = MainCharacter.Intelligence + int.Parse(RaceBonuses[Count,2]);
                            break;
                        case "Wis":
                            MainCharacter.Wisdom = MainCharacter.Wisdom + int.Parse(RaceBonuses[Count, 2]);
                            break;
                        case "Car":
                            MainCharacter.Charisma = MainCharacter.Charisma + int.Parse(RaceBonuses[Count, 2]);
                            break;
                        case "All":
                        //build in a catchall for add to all race bonus, needs refining
                          MainCharacter.Strength = MainCharacter.Strength + int.Parse(RaceBonuses[Count, 2]);
                          MainCharacter.Dexterity = MainCharacter.Dexterity + int.Parse(RaceBonuses[Count, 2]);
                          MainCharacter.Constitution = MainCharacter.Constitution + int.Parse(RaceBonuses[Count, 2]);
                          MainCharacter.Intelligence = MainCharacter.Intelligence + int.Parse(RaceBonuses[Count, 2]);
                          MainCharacter.Wisdom = MainCharacter.Wisdom + int.Parse(RaceBonuses[Count, 2]);
                          MainCharacter.Charisma = MainCharacter.Charisma + int.Parse(RaceBonuses[Count, 2]);
                          break;
                    } //end case
                } //end if
                Count = Count + 1;
            }//end while
            return MainCharacter;     
           
        }



        public static void DisplayStats(CharStats MainCharacter)
        {

            Console.WriteLine("character Stats");
            Console.WriteLine("character Name: " + MainCharacter.CharacterName);
            Console.WriteLine("character Class: " + MainCharacter.CharacterClass);
            Console.WriteLine("Character Race: " + MainCharacter.CharacterRace);
            Console.WriteLine("Character Strength: " + MainCharacter.Strength);
            Console.WriteLine("Character Dexterity: " + MainCharacter.Dexterity);
            Console.WriteLine("Character Constitution: " + MainCharacter.Constitution);
            Console.WriteLine("Character Intelligence: " + MainCharacter.Intelligence);
            Console.WriteLine("Character Wisdom: " + MainCharacter.Wisdom);
            Console.WriteLine("Character Charisma: " + MainCharacter.Charisma);

        }

    }
}
