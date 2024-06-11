using System;
namespace MAUI_project.Models
{
	public class Card
	{
        public string Suit { get; set; }
        public int Rank { get; set; }
        public string RankName { get; set; }

        public Card(string suit, string rankName, int rank)
        {
            Suit = suit;
            RankName = rankName;
            Rank = rank;
        }
        public string GetCardImageSource(Card card)
        {
            return $"card_{card.RankName}_of_{card.Suit}";
        }
    }
}

