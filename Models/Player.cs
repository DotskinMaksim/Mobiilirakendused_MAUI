using System;
using System.Collections.ObjectModel;
using SQLite;

namespace MAUI_project.Models
{
   

	public class Player
	{


        public List<Card> PlayerHand { get; set; }
		public string Name { get; set; }
		public Frame HandFrame { get; set; }
		public Slot Slot { get; set; }


        public Player()
		{
		}
	}
}

