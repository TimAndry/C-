using System;
using System.Collections.Generic;

namespace DeckOfCards {

    public class Deck {
        List<Card> Cards = new List<Card> ();
        public Deck () {
            int[] vals = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            string[] names = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };
            string[] suits = new string[] { "Spades", "Hearts", "Clubs", "Diamonds" };

            for (int i = 0; i < suits.Length; i++) {
                for (int j = 0; j < names.Length; j++) {
                    Card acard = new Card (names[j], suits[i], vals[j]);
                    Cards.Add(acard);
                }
            }
        }


        public void Shuffle () {
            Random rand = new Random();
            for(int end = Cards.Count-1; end > 0; end--){
                int randx = rand.Next(end);
                Card temp = Cards[randx];
                Cards[randx] = Cards[end];
                Cards[end] = temp;
            }
        }

        public void Deal(){
            Card top = Cards[0];
            Cards.Remove(top);  
        }

        public void Reset(){
            Cards = new List<Card>();
            int[] vals = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            string[] names = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };
            string[] suits = new string[] { "Spades", "Hearts", "Clubs", "Diamonds" };

            for (int i = 0; i < suits.Length; i++) {
                for (int j = 0; j < names.Length; j++) {
                    Card acard = new Card (names[j], suits[i], vals[j]);
                    Cards.Add(acard);
                }
            }
        }
    }
}