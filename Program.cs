using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static ClassesinCS.Program;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Security.Cryptography;

public static class Extensions
{
    public static bool find<T>(this T[] array, T target)
    {
        return array.Contains(target);
    }
}
namespace ClassesinCS
{
    internal class Program
    {

        public enum CardFaces
        {
            Ace = 1,
            One,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            HighAce,
        }
        public enum Suits
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades,
        };

        public class Card
        {
            public CardFaces name;
            public Suits suit;
            public int value;

        }

        public class Deck
        {

            const int NumOfCards = 52;
            public Card[] arrCards = new Card[NumOfCards];
            public string newCard;
            public string newSuit;


            public string[,] SetupCardsForPlayers()
            {
                //Instantiating variables
                string[,] ShowDeck = new string[(int)Suits.Spades + 1, ((int)Suits.Spades + 1) * (int)CardFaces.King];
                string[] ShowSuits = new string[(int)Suits.Spades + 1];
                string[] ShowRanks = new string[((int)Suits.Spades + 1) * (int)CardFaces.King];

                for (int suit = (int)Suits.Clubs; suit <= (int)Suits.Spades; suit++)
                {

                    for (int card = (int)CardFaces.Ace; card <= (int)CardFaces.King; card++)
                    {
                        Card c = new Card
                        {
                            suit = (Suits)suit,
                            name = (CardFaces)card
                        };

                        c.value = (int)c.name;
                        switch (suit)
                        {
                            case 0:
                                newSuit = "Clubs";
                                break;
                            case 1:
                                newSuit = "Diamonds";
                                break;
                            case 2:
                                newSuit = "Hearts";
                                break;
                            case 3:
                                newSuit = "Spades";
                                break;
                        }

                        switch (card)
                        {
                            case 1:
                                newCard = "_Ace";
                                break;
                            case 2:
                                newCard = "_Two";
                                break;
                            case 3:
                                newCard = "_Three";
                                break;
                            case 4:
                                newCard = "_Four";
                                break;
                            case 5:
                                newCard = "_Five";
                                break;
                            case 6:
                                newCard = "_Six";
                                break;
                            case 7:
                                newCard = "_Seven";
                                break;
                            case 8:
                                newCard = "_Eight";
                                break;
                            case 9:
                                newCard = "_Nine";
                                break;
                            case 10:
                                newCard = "_Ten";
                                break;
                            case 11:
                                newCard = "_Jack";
                                break;
                            case 12:
                                newCard = "_Queen";
                                break;
                            case 13:
                                newCard = "_King";
                                break;
                        }


                        //Creates a two dimensional Array that casts one side as the suits and one as the ranks. 
                        ShowSuits[suit] = newSuit;
                        ShowRanks[card] = newCard;
                        ShowDeck[suit, card] = ShowSuits[suit] + ShowRanks[card];

                        int index = (13 * suit) + card - 1;

                        arrCards[index] = c;
                    }
                }

                return ShowDeck;
            }

            public int[] SetupcardsForDealer()
            {
                //Instantiating variables
                int[] Deck = new int[((int)Suits.Spades + 1) * (int)CardFaces.King];
                int[] suits = new int[(int)Suits.Spades + 1];
                int[] ranks = new int[((int)Suits.Spades + 1) * (int)CardFaces.King];
                int index = 0;

                //Creates a nested for loop that will instantiate the deck numerically
                for (int suit = (int)Suits.Clubs; suit <= (int)Suits.Spades; suit++)
                {

                    for (int card = (int)CardFaces.Ace; card <= (int)CardFaces.King; card++)
                    {
                        Card c = new Card
                        {
                            suit = (Suits)suit,
                            name = (CardFaces)card
                        };


                       

                        suits[suit] = suit;
                        ranks[card] = card;
                        Deck[index] = card;

                        index++;
                    }
                }

                return Deck;
            }

            

            public void HandingCardsOutToPlayer1()
            {
                //Instantiating variables.
                string[] ShuffledDeck = new string[52];
                int[] DealersDeck = new int[52];
                int[] ShuffledDealersDeck = new int[52];
                string[,] Deck = new string[(int)Suits.Spades + 1, ((int)Suits.Spades + 1) * (int)CardFaces.King];
                Deck = SetupCardsForPlayers();
                DealersDeck = SetupcardsForDealer();

                //Randomly shuffeld the deck
                System.Random random = new System.Random();
                for (int handingCardOut = 0; handingCardOut < 52; handingCardOut++)
                {
                    bool inDeck = true;

                    bool Quit = false;

                    while (inDeck && !Quit)
                    {
                        int randomSuit = random.Next(0, 4);
                        int randomCard = random.Next(1, 14);

                        string check = Deck[randomSuit, randomCard];
                        int behindCheck = DealersDeck[randomCard - 1];
                        inDeck = ShuffledDeck.find(check);


                        if (!inDeck)
                        {
                            Quit = true;
                            ShuffledDeck[handingCardOut] = check;
                            ShuffledDealersDeck[handingCardOut] = behindCheck;
                        }
                    }
                   //Console.Write("[" + ShuffledDeck[handingCardOut] + "]");
                   // Console.Write("(" + ShuffledDealersDeck[handingCardOut] + ")");

                }

                //Instantiatiing variables
                string[] Player1 = new string[52];
                int[] Player1Value = new int[52];
                string[] Player2 = new string[52];
                int[] Player2Value = new int[52];
                int dealing = 0;
                int dealing2 = 0;

                //Hands out the cards to each player
                for (int dealt = 0; dealt < 52; dealt ++)
                {
                    if (dealt % 2 == 0 || dealing == 0 )
                    {
                        Player2[dealing] = ShuffledDeck[dealt];
                        Player2Value[dealing] = ShuffledDealersDeck[dealt];
                        dealing++;
                    } 
                    else
                    {
                        Player1[dealing2] = ShuffledDeck[dealt];
                        Player1Value[dealing2] = ShuffledDealersDeck[dealt];
                        dealing2++;
                    }
                }
                
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                int timesPlayed = 0;
                bool GameOver = true;
                while (keyInfo.Key != ConsoleKey.Escape && GameOver)
                {
                    //Creating Space between lines
                    Console.WriteLine();
                    for (int i = 0; i < 52; i++)
                    {
                        Console.Write(Player1Value[i] + ";");
                    }
                    Console.WriteLine();
                    for (int i = 0; i < 52; i++)
                    {
                        Console.Write(Player2Value[i] + ";");
                    }
                    Console.WriteLine();

                    //Instantiating variables
                    int Player2Count = 0;
                    int Player1Count = 0;
                    bool player1Played = true;
                    bool player2Played = true;
                    string[] playedCards = new string[52];
                    int[] playedCardValues = new int[52];
                    int[] holdWarCards1 = new int[52];
                    int[] holdWarCards2 = new int[52];
                    string[] holdWarCards1Hand = new string[52];
                    string[] holdWarCards2Hand = new string[52];

                    //Allows the user to play one card and then prints it to the screen
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine("PLayer 1 plays a: " + Player1[0]);
                        
                        player1Played= false;
                    }

                    if (Console.ReadKey().Key == ConsoleKey.UpArrow)
                    {
                       
                        Console.WriteLine("Player 2 plays a: " + Player2[0]);
                        
                        player2Played = false;
                    }

                    //Creates an if statement that forces both players to play their card first then can continue
                    if (!player1Played && !player2Played)
                    {

                        //Scenario for when player1's card is higher than player2's card
                        if (Player1Value[0] > Player2Value[0])
                        {
                           
                            Console.WriteLine("Player 1 Won this round!");

                            //Find the lat card inside player1's hand
                            bool foundLastCard = true;
                            while (Player1Count < 52 && foundLastCard == true)
                            {

                                if (Player1Value[Player1Count] == 0)
                                {

                                    foundLastCard = false;
                                }
                                Player1Count++;
                            }

                            //Puts the cards played into the bottom of player1's deck.
                            Player1Value[Player1Count - 1] = Player1Value[0];
                            Player1[Player1Count - 1] = Player1[0];
                            if (Player2Value[0] != 0)
                            {
                                Player1Value[Player1Count] = Player2Value[0];
                                Player1[Player1Count] = Player2[0];
                            }

                            //Finds how many cards are inside each player's hand and then prints it to the screen.
                            bool LastCard1 = true;
                            int lastCardInhand1 = 0;
                            while (lastCardInhand1 < 52 && LastCard1 == true)
                            {

                                if (Player1Value[lastCardInhand1] == 0)
                                {

                                    LastCard1 = false;
                                }
                                lastCardInhand1++;
                            }

                            bool LastCard2 = true;
                            int lastCardInhand2 = 0;
                            while (lastCardInhand2 < 52 && LastCard2 == true)
                            {

                                if (Player2Value[lastCardInhand2] == 0)
                                {

                                    LastCard2 = false;
                                }
                                lastCardInhand2++;
                            }
                            Console.WriteLine("\nPlayer 1 has:" + (lastCardInhand1 - 2) + " in their hand.");
                            Console.WriteLine("Player 2 has:" + (lastCardInhand2 - 2) + " in their hand.");
                        }

                        //Scenario for when player2's card is higher than player1's card
                        else if (Player1Value[0] < Player2Value[0])
                        {
                           // Console.WriteLine("\n\n" + Player1Value[0] + "v." + Player2Value[0]);
                            Console.WriteLine("Player 2 Won this round!");

                            //Finds the last card in player2's hand
                            bool foundLastCard = true;
                            while (Player2Count < 52 && foundLastCard == true)
                            {
                                //Console.Write(Player2Value[Player2Count] + ";");
                                if (Player2Value[Player2Count] == 0)
                                {
                                    foundLastCard = false;
                                }
                                Player2Count++;
                            }
                            
                            //Puts the cards played onto the bottom of player2's deck
                            Player2Value[Player2Count - 1] = Player2Value[0];
                            Player2[Player2Count - 1] = Player2[0];
                            if (Player1Value[0] != 0)
                            {
                                Player2Value[Player2Count] = Player1Value[0];
                                Player2[Player2Count] = Player1[0];
                            }
                            
                            
                            

                            //Calculates how many cards are in each player's hand and then prints it to the screen
                            bool LastCard1 = true;
                            int lastCardInhand1 = 0;
                            while (lastCardInhand1 < 52 && LastCard1 == true)
                            {

                                if (Player1Value[lastCardInhand1] == 0)
                                {

                                    LastCard1 = false;
                                }
                                lastCardInhand1++;
                            }

                            bool LastCard2 = true;
                            int lastCardInhand2 = 0;
                            while (lastCardInhand2 < 52 && LastCard2 == true)
                            {

                                if (Player2Value[lastCardInhand2] == 0)
                                {

                                    LastCard2 = false;
                                }
                                lastCardInhand2++;
                            
                            }
                            Console.WriteLine("\nPlayer 1 has:" + (lastCardInhand1 - 2) + " in their hand.");
                            Console.WriteLine("Player 2 has:" + (lastCardInhand2 - 2) + " in their hand.");
                        }
                        else if (Player1Value[0] == Player2Value[0])
                        {
                            bool playerWon = true;
                            while (playerWon)
                            {
                                //Checks to see if each the player has less than four cards
                                int stopCountingforPlayer1 = 5;
                                int stopCountingforPlayer2 = 5;
                                for (int i = 0; i < 5; i++)
                                {
                                    if (Player1Value[i] == 0)
                                    {
                                        stopCountingforPlayer1 = i;
                                    }
                                    if (Player2Value[i] == 0)
                                    {
                                        stopCountingforPlayer2 = i;
                                    }
                                }

                                //Puts the first four cards into the temp array or it puts however many cards are in the hand into the temp array.
                                for (int i = 0; i < stopCountingforPlayer1; i++)
                                {
                                    holdWarCards1[i] = Player1Value[i];
                                    holdWarCards2[i] = Player2Value[i];
                                    holdWarCards1Hand[i] = Player1[i];
                                    holdWarCards2Hand[i] = Player2[i];
                                }

                                //If player1 has less than 4 cards then it will only move the cards over however many cards are remaining
                                if (stopCountingforPlayer1 < 4)
                                {
                                    int m = 0;
                                    for (int i = stopCountingforPlayer1; i < 52; i++)
                                    {
                                        Player1[m] = Player1[i];
                                        Player1Value[m] = Player1Value[i];
                                        m++;
                                    }
                                    if (stopCountingforPlayer2 == 5)
                                    {
                                        int holder = 0;
                                        for (int i = 4; i < 52; i++)
                                        {
                                            Player2[holder] = Player2[i];
                                            Player2Value[holder] = Player2Value[i];
                                            holder++;
                                        }
                                    }
                                } 
                                //If player2 has less than 4 cards then it will only move the cards over however many cards are remaining
                                if (stopCountingforPlayer2 < 4)
                                {
                                    int m = 0;
                                    for (int i = stopCountingforPlayer2; i < 52; i++)
                                    {
                                        Player2[m] = Player2[i];
                                        Player2Value[m] = Player2Value[i];
                                        m++;
                                    }
                                    if (stopCountingforPlayer1 == 5)
                                    {
                                        int holder = 0;
                                        for (int i = 4; i < 52; i++)
                                        {
                                            Player1[holder] = Player1[i];
                                            Player1Value[holder] = Player1Value[i];

                                        }
                                    }
                                }

                                //If both people have more than 4 cards then it will move everyone's cards down four elements.
                                if (stopCountingforPlayer1 == 5 && stopCountingforPlayer2 == 5)
                                {
                                    
                                    int holder = 0;
                                    for (int i = 4; i < 52; i++)
                                    {
                                        Player1[holder] = Player1[i];
                                        Player1Value[holder] = Player1Value[i];

                                        Player2[holder] = Player2[i];
                                        Player2Value[holder] = Player2Value[i];
                                        holder++;
                                    }
                                }

                                Console.WriteLine("******************WAR!******************");

                                if (Console.ReadKey().Key == ConsoleKey.Enter)
                                {
                                    Console.WriteLine("PLayer 1 plays a: " + Player1[0]);

                                    player1Played = false;
                                }
                                if (Console.ReadKey().Key == ConsoleKey.UpArrow)
                                {

                                    Console.WriteLine("Player 2 plays a: " + Player2[0]);

                                    player2Played = false;
                                }

                                //Scenario for when player1's card is higher than player2's hand
                                if (Player1Value[0] > Player2Value[0])
                                {
                                    bool foundLastCard = true;
                                    while (Player1Count < 52 && foundLastCard == true)
                                    {

                                        if (Player1Value[Player1Count] == 0)
                                        {

                                            foundLastCard = false;
                                        }
                                        Player1Count++;
                                    }
                                    Console.WriteLine("Player 1 Won the War!");

                                    //Puts the cards played in the war into player1's hand first doing the cards that player1 played first.

                                    int j = 0;
                                    int i = Player1Count - 1;
                                    bool hitzero = true;
                                    while (i < 52 && hitzero)
                                    {
                                        
                                        Player1Value[i] = holdWarCards1[j];
                                        Player1[i] = holdWarCards1Hand[j];
                                        if (Player1Value[i] == 0) { hitzero = false; }
                                        j++;
                                        i++;
                                    }
                                    j = 0;
                                    while (i < 52 )
                                    {
                                        Player1Value[i - 1] = holdWarCards2[j];
                                        Player1[i - 1] = holdWarCards2Hand[j];
                                        j++;
                                        i++;
                                    }
                                    playerWon = false;
                                }
                                //Scenario for when player2 has a higher card than player1
                                else if (Player1Value[0] < Player2Value[0])
                                {
                                    //Find the last card in player2's hand
                                    bool foundLastCard = true;
                                    while (Player2Count < 52 && foundLastCard == true)
                                    {
                                        //Console.Write(Player2Value[Player2Count] + ";");
                                        if (Player2Value[Player2Count] == 0)
                                        {
                                            foundLastCard = false;
                                        }
                                        Player2Count++;
                                    }

                                    Console.WriteLine("Player 2 Won the War!");
                                    
                                    //Puts the cards played in the war into player2's hand first doing the cards that player2 played first.

                                    int j = 0;
                                    int i = Player2Count - 1;
                                    bool hitZero = true;
                                    while (i < 52 && hitZero)
                                    {
                                        
                                        Player2Value[i] = holdWarCards2[j];
                                        Player2[i] = holdWarCards2Hand[j];
                                        if (Player2Value[i] == 0) { hitZero= false; }
                                        i++;
                                        j++;
                                    }
                                    j = 0;
                                    while (i < 52)
                                    {
                                        Player2Value[i - 1] = holdWarCards1[j];
                                        Player2[i - 1] = holdWarCards1Hand[j];
                                        j++;
                                        i++;
                                    }

                                    playerWon = false;
                                }
                            }
                            //Calculates how many cards are in each player's hand and then prints it to the screen
                            bool LastCard1 = true;
                            int lastCardInhand1 = 0;
                            while (lastCardInhand1 < 52 && LastCard1 == true)
                            {

                                if (Player1Value[lastCardInhand1] == 0)
                                {

                                    LastCard1 = false;
                                }
                                lastCardInhand1++;
                            }

                            bool LastCard2 = true;
                            int lastCardInhand2 = 0;
                            while (lastCardInhand2 < 52 && LastCard2 == true)
                            {

                                if (Player2Value[lastCardInhand2] == 0)
                                {

                                    LastCard2 = false;
                                }
                                lastCardInhand2++;

                            }
                            Console.WriteLine("\nPlayer 1 has:" + (lastCardInhand1 - 2) + " in their hand.");
                            Console.WriteLine("Player 2 has:" + (lastCardInhand2 - 2) + " in their hand.");

                        }
                        //Moves the cards down one element for each hand
                        int k = 0;
                        for (int i = 1; i < 52; i++)
                        {
                            Player1[k] = Player1[i];
                            Player1Value[k] = Player1Value[i];

                            Player2[k] = Player2[i];
                            Player2Value[k] = Player2Value[i];
                            k++;
                        }
                        //Keeps track of how many times it runs through this loop.
                        timesPlayed++;
                    }
                    else
                    {
                        Console.WriteLine("Please allow both users to play their first card.");
                    }

                    if (Player1Value[0] == 0)
                    {
                        Console.WriteLine("Player 2 Has won the War!");
                        for (int i = 0; i < 52; i++)
                        {
                            Console.WriteLine("(" + i + ")" + "[" + Player2Value[i] + "]");
                        }
                    } else if (Player2Value[0] == 0)
                    {
                        Console.WriteLine("Player 1 Has won the War!");
                        for (int i = 0; i < 52; i++)
                        {
                            Console.WriteLine("(" + i + ")" + "[" + Player1Value[i] + "]");
                        }
                    }
                }

                Console.WriteLine("You played " + timesPlayed + " times.");

            }


            public string[] ShuffleDeck()
            {
                string[] ShuffledDeck = new string[52];
                int[] DealersDeck = new int[52];
                int[] ShuffledDealersDeck = new int[52];
                string[,] Deck = new string[(int)Suits.Spades + 1, ((int)Suits.Spades + 1) * (int)CardFaces.King];
                Deck = SetupCardsForPlayers();
                DealersDeck = SetupcardsForDealer();

                System.Random random = new System.Random();
                for (int handingCardOut = 0; handingCardOut < 52; handingCardOut++)
                {
                    bool inDeck = true;
                   
                    bool Quit = false;

                    while (inDeck && !Quit)
                    {
                        int randomSuit = random.Next(0, 4);
                        int randomCard = random.Next(1, 14);

                        string check = Deck[randomSuit, randomCard];
                        int behindCheck = DealersDeck[randomCard - 1];
                        inDeck = ShuffledDeck.find(check);
                        

                        if (!inDeck)
                        {
                            Quit = true;
                            ShuffledDeck[handingCardOut] = check;
                            ShuffledDealersDeck[handingCardOut] = behindCheck;
                        }
                    }
                    Console.Write("[" + ShuffledDeck[handingCardOut] + "]");
                    Console.Write("(" + ShuffledDealersDeck[handingCardOut] + ")");

                }
                return ShuffledDeck;
            }
        }


        static void Main(string[] args)
            {
                Console.WriteLine("WELCOME TO WAR!");

                Deck deck = new Deck();
                deck.HandingCardsOutToPlayer1();

                Console.ReadLine();
            }

            static void ExceptionFormatting()
            {
                try
                {
                    Console.Write("Choose a number: ");
                    int num1 = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Choose another number: ");
                    int num2 = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine(num1 / num2);

                }
                //To catch individual excpetions
                catch (DivideByZeroException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                //This block will always run even it its gets caught
                finally
                {
                    Console.WriteLine("Nice Try");
                }
            }

        }
    }