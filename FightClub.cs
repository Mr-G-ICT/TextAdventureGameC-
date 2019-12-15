using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureGame
{
    class FightClub
    {
//this class will contain the fight mechanic for the game.
//need to resarch teh D&D fight mechanic
        private static CharacterGeneration.CharStats GenerateEnemy()
        {
            /* probably going to be a class, used to generate a random enemy based on character generation subs*/
            CharacterGeneration.CharStats EnemyCharacter;


            //default initialisation first, then on to fight mechanic
            EnemyCharacter.CharacterName = " badguy";
            EnemyCharacter.CharacterClass = "";
            EnemyCharacter.CharacterRace = "";
            EnemyCharacter.Strength = 15;
            EnemyCharacter.Dexterity = 14;
            EnemyCharacter.Constitution = 13;
            EnemyCharacter.Intelligence = 12;
            EnemyCharacter.Wisdom = 10;
            EnemyCharacter.Charisma = 8;

            return EnemyCharacter;
        }


        public static CharacterGeneration.CharStats FightMechanic(CharacterGeneration.CharStats MainCharacter)
        {
            /***************************************************************
            /*
            /*
            /*
            /*
            /* 
            /********************************************************************/

            CharacterGeneration.CharStats EnemyCharacter;
            //Initialise Variables
            int StrengthModifier;
            int DexterityModifier;
            Random Dice = new Random();
            int CharacterDiceRoll = Dice.Next(1, 10);
            int EnemyDiceRoll = Dice.Next(1,8);
            bool death = false;

            EnemyCharacter = GenerateEnemy();

            while ((MainCharacter.Constitution > 0) || (EnemyCharacter.Constitution > 0))
            {


                //generate the modifiers for the encounter using Modifier function
                StrengthModifier = 6;
                DexterityModifier = 2;
                // StrengthModifier = CalculateModifier(MainCharacter.Strength, EnemyCharacter.Strength);
                //DexterityModifier = CalculateModifier(MainCharacter.Dexterity, MainCharacter.Dexterity);

                Console.WriteLine(MainCharacter.Strength & EnemyCharacter.Strength & StrengthModifier & DexterityModifier);
                Console.ReadLine();


                    CharacterDiceRoll = Dice.Next(1, 10);
                    EnemyDiceRoll = Dice.Next(1, 8);

                //depending on the dice roll whichever person scores the highest wins that round and adds
                //the modifier to their strength. could be modified to health.
                if (CharacterDiceRoll > EnemyDiceRoll)
                {
                    //based on dexterity, give character the chance to dodge
                    int ChanceToDodge = Dice.Next(1, DexterityModifier);
                    Console.WriteLine("ChanceToDodge" + ChanceToDodge);

                    Console.WriteLine(MainCharacter.CharacterName + " wins");
                    MainCharacter.Strength = MainCharacter.Strength + StrengthModifier;
                    MainCharacter.Dexterity = MainCharacter.Dexterity + DexterityModifier;

                    EnemyCharacter.Constitution = EnemyCharacter.Constitution - StrengthModifier;

                }
                else if (EnemyDiceRoll > CharacterDiceRoll)
                {
                    Console.WriteLine(EnemyCharacter.CharacterName + " wins");

                    MainCharacter.Constitution = MainCharacter.Constitution - StrengthModifier;
                    MainCharacter.Dexterity = MainCharacter.Dexterity - DexterityModifier;


                    EnemyCharacter.Strength = EnemyCharacter.Strength + StrengthModifier;

                }
                else
                {


                    Console.WriteLine("draw");
                }

                //determines if a charicter has dies, loop quit condition
                if (MainCharacter.Constitution <= 0)
                {
                    Console.WriteLine(MainCharacter.CharacterName + " has died, you are dead");
                    death = true;
                    Environment.Exit(0);
                } else if (EnemyCharacter.Constitution <= 0)
                {
                    Console.WriteLine(EnemyCharacter.CharacterName + " has died");
                    return MainCharacter;
                    death = true;
                 
                }

                Console.Write(MainCharacter.CharacterName + "'s strength " + MainCharacter.Strength + " ");
                Console.WriteLine(MainCharacter.CharacterName + "'s skill " + MainCharacter.Dexterity);
                Console.WriteLine("Monster " + EnemyCharacter.Strength + " Monster " + EnemyCharacter.Dexterity);
                Console.ReadLine();
                }
            return MainCharacter;
            }

        private static int CalculateModifier(int CharStat, int EnemyStat)
    {
            //modifier is a mix of character D roll and constitution
            Random Dice = new Random();
            int Modifier;
            Modifier = Dice.Next(1, 6);

            if (CharStat > EnemyStat)
                    {
            }
                

            return 42;
        }
        

        private static void RunAway()
        {

        }


    }
}
