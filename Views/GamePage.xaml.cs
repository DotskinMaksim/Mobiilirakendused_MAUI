using System.Collections.Generic;
using System.Collections.ObjectModel;
using AndroidX.ConstraintLayout.Helper.Widget;
using AndroidX.Lifecycle;
using MAUI_project.Models;
using System.Threading.Tasks;
using Microsoft.Maui.Layouts;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MAUI_project.Views;

public partial class GamePage : ContentPage
{
    public List<Card> Deck { get; private set; }
    public List<Card> deckForDrawing;

    public List<Player> players;

    public List<Slot> slots;

    public List<Card> playedCards;

    Player playerTurnToMove , player1 , player2;

    Frame selectedFrame;

    Label player1CardsCount, player2CardsCount;
    public List<Label> playerCountsLabels;

    bool reverseMode, lowerMode;

    int countOfMoves;

    bool enableSlots;

    Image imageOfCardBack;

    public GamePage(string player1Name, string player2Name, int deckSize, bool reverseCardMode, bool lowerCardMode)
	{
		InitializeComponent();

       


        imageOfCardBack = new Image
        {
            Source = "back",
            WidthRequest = 100,
            HeightRequest = 150,
            Aspect = Aspect.AspectFill
        };



        reverseMode = reverseCardMode;
        lowerMode = lowerCardMode;

        enableSlots = true;

        slots = new List<Slot>();

        Deck = new List<Card>(GenerateDeck(deckSize));

        playerCountsLabels = new List<Label>();


        deckForDrawing = new List<Card>(Deck);

        countOfMoves = 0;

        player1 = new Player { Name = player1Name, PlayerHand = DrawRandomCards(deckSize / 2), HandFrame = null, Slot=null };

        player2 = new Player { Name = player2Name, PlayerHand = DrawRandomCards(deckSize / 2), HandFrame = null, Slot = null };


        playerTurnToMove = player1;
        player1ValuesFrame.BorderColor = Colors.Red;
        player2ValuesFrame.BorderColor = null;

        playedCards = new List<Card>();


        players = new List<Player> { player1, player2 };

        player1Values.Children.Add(new Label { Text = player1Name, Padding = 10 });

        player2Values.Children.Add(new Label { Text = player2Name, Padding = 10 });


        GeneratePlayersCountsLabels();
        GeneratePlayersHandsFrames();
        GenerateSlotsFrames();

        UpdatePlayersCardsCountLabels();


        TestEndPage();
    }

    private async void TestEndPage()
    {
        await Navigation.PushAsync(new EndPage(player1, 10));

    }

    private void ShowRules(object sender, EventArgs e)
    {
        DisplayAlert("Game rules",
              "1. The deck is evenly divided between the players\n" +
              "2. Each player places their stack of cards face down in front of them\n" +
              "3. Each player takes the top card from their stack and places it in the center\n" +
              "4. The card with the highest rank wins (in 'lower card mode' the lowest card beats an ace)\n" +
              "5. Depending on the game mode, it is taken by either the winner or the loser\n" +
              "6. If two cards of the same rank are played consecutively, it's a 'War'\n" +
              "7. In a 'War', draw cards are postponed and the war continues until one of the players wins\n" +
              "8. The game continues until one player wins all the cards or vice versa, depending on the mode",
              "OK");
    }

    private void GeneratePlayersCountsLabels()
    {
        player1CardsCount = new Label
        {
            Text = "",
            TextColor = Colors.DarkGray,
            BackgroundColor = Colors.Transparent,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 24, 
            FontAttributes = FontAttributes.Bold
        };
        player2CardsCount = new Label
        {
            Text = "",
            TextColor = Colors.DarkGray,
            BackgroundColor = Colors.Transparent,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 24, 
            FontAttributes = FontAttributes.Bold
        };
        playerCountsLabels.Add(player1CardsCount);
        playerCountsLabels.Add(player2CardsCount);
    }


    private void SwitchPlayerTurnToMove()
    {
        if (playerTurnToMove == player1)
        {
            playerTurnToMove = player2;
            player2ValuesFrame.BorderColor = Colors.Red;
            player1ValuesFrame.BorderColor = default;


        }
        else
        {
            playerTurnToMove = player1;
            player1ValuesFrame.BorderColor = Colors.Red;
            player2ValuesFrame.BorderColor = default;



        }
    }

    private void MakeTheMove(Frame frame)
    {
        if (playerTurnToMove.Slot.SlotFrame == frame && playerTurnToMove.HandFrame==selectedFrame)
        {
            Card playingCard = playerTurnToMove.PlayerHand[0];
            string rankName = playingCard.RankName;
            string rankSuit = playingCard.Suit;
            //string cardSource = $"card_{rankName}_of_{rankSuit}";
            string cardSource = playingCard.GetCardImageSource(playingCard);


            Slot slot= slots.FirstOrDefault(slot => slot.SlotFrame == frame);

            slot.SlotFrame.Content = new Image { Source = cardSource };
            slot.SlotCard = playingCard;

            playerTurnToMove.PlayerHand.RemoveAt(0);

            playerTurnToMove.HandFrame.BorderColor = Colors.Transparent;
            selectedFrame = null;

            UpdatePlayersCardsCountLabels();

            SwitchPlayerTurnToMove();
            CompareCards();

            countOfMoves++;
        }
    }
    private void UpdatePlayersCardsCountLabels()
    {

        Dictionary<Label, Player> labelsFrames = new Dictionary<Label, Player>
        {
            { player1CardsCount, player1 },
            { player2CardsCount, player2 }
        };
        foreach (var item in labelsFrames)
        {
            if (item.Key.Text == "0")
            {
                item.Value.HandFrame.Content = new Image
                {
                    Source = imageOfCardBack.Source,
                    WidthRequest = imageOfCardBack.WidthRequest,
                    HeightRequest = imageOfCardBack.HeightRequest,
                    Aspect = imageOfCardBack.Aspect
                };
            }
            item.Key.Text = item.Value.PlayerHand.Count.ToString();
            if (item.Key.Text == "0")
            {
                item.Value.HandFrame.Content = null;
            }
        }
       
    }
    private bool BothClotsHaveCards()
    {
        foreach (var slot in slots)
        {
            if (slot.SlotFrame.Content==null)
            {
                return false;
            }
        }
        return true;

    }

    private async void MakeThePauseForColoring(int delay)
    {
        Color player1CurrentOutlineColor = player1ValuesFrame.BorderColor;
        Color player2CurrentOutlineColor = player2ValuesFrame.BorderColor;

        player1ValuesFrame.BorderColor = null;
        player2ValuesFrame.BorderColor = null;

        enableSlots = false;

        await Task.Delay(delay);

        enableSlots = true;


        player1ValuesFrame.BorderColor = player1CurrentOutlineColor;
        player2ValuesFrame.BorderColor = player2CurrentOutlineColor;


    }

    private async void HighLightSlots(Slot wonSlot, Slot lostSlot)
    {
        lostSlot.SlotFrame.BorderColor = Colors.Green;
        wonSlot.SlotFrame.BorderColor = Colors.Red;
        await Task.Delay(500);
        lostSlot.SlotFrame.BorderColor = Colors.Black;
        wonSlot.SlotFrame.BorderColor = Colors.Black;
    }

    private async void CompareCards()
    {
        if (BothClotsHaveCards())
        {
            MakeThePauseForColoring(1000);
            await Task.Delay(500);


            Slot slot1 = slots[0];
            Slot slot2 = slots[1];

            int rank1 = slot1.SlotCard.Rank;
            int rank2 = slot2.SlotCard.Rank;

            Slot wonSlot = null;
            Slot lostSlot = null;
            bool isDraw = false;

            int lowerDeckRank = Deck[0].Rank;

            

            playedCards.Add(slot1.SlotCard);
            playedCards.Add(slot2.SlotCard);

            if (lowerMode)
            {
                if ((rank1 == 14 && rank2 == lowerDeckRank))
                {
                    wonSlot = slot2;
                    lostSlot = slot1;
                    HighLightSlots(wonSlot, lostSlot);

                }
                else if ((rank1 == lowerDeckRank && rank2 == 14))
                {
                    wonSlot = slot1;
                    lostSlot = slot2;
                    HighLightSlots(wonSlot, lostSlot);


                }
                else if (rank1 > rank2)
                {
                    wonSlot = slot1;
                    lostSlot = slot2;
                    HighLightSlots(wonSlot, lostSlot);


                }
                else if (rank1 < rank2)
                {
                    wonSlot = slot2;
                    lostSlot = slot1;
                    HighLightSlots(wonSlot, lostSlot);
                }
                else
                {
                    isDraw = true;
                }
                
            }
            else
            {
         
                if (rank1 > rank2)
                {
                    wonSlot = slot1;
                    lostSlot = slot2;
                    HighLightSlots(lostSlot, wonSlot);


                }
                else if (rank1 < rank2)
                {
                    wonSlot = slot2;
                    lostSlot = slot1;
                    HighLightSlots(lostSlot, wonSlot);

                }
                else
                {
                    isDraw = true;
                }
               
            }
           


            if (!isDraw)
            {
                if (reverseMode)
                {
                    Player lostPlayer = players.FirstOrDefault(lostPlayer => lostPlayer.Slot == lostSlot);
                    foreach (var card in playedCards)
                    {
                        lostPlayer.PlayerHand.Add(card);

                    }
                }
                else
                {
                    Player wonPlayer = players.FirstOrDefault(wonPlayer => wonPlayer.Slot == wonSlot);
                    foreach (var card in playedCards)
                    {
                        wonPlayer.PlayerHand.Add(card);

                    }
                }

     

                playedCards.Clear();

            }
            else
            {
                foreach (var player in players)
                {
                    if (player.PlayerHand.Count == 0)
                    {
                        foreach (var card in playedCards)
                        {
                            player.PlayerHand.Add(card);
                        }
                        playedCards.Clear();

                    }
                }
                slot1.SlotFrame.BorderColor = Colors.Yellow;
                slot2.SlotFrame.BorderColor = Colors.Yellow;
                await Task.Delay(500);
                slot1.SlotFrame.BorderColor = Colors.Black;
                slot2.SlotFrame.BorderColor = Colors.Black;

            }
            ClearSlots();
            DrawPlayedCards();
            UpdatePlayersCardsCountLabels();

            CheckWorWin();


        }
    }

    private async void CheckWorWin()
    {
        foreach (var player in players)
        {
            if (reverseMode)
            {
                if (player.PlayerHand.Count == 0)
                {
                    await DisplayAlert("Victory!", $"Player {player.Name} won the game", "OK");

                    await Navigation.PushAsync(new EndPage(player, countOfMoves/2));

                }
            }
            else
            {
                if (player.PlayerHand.Count == Deck.Count)
                {
                    await DisplayAlert("Victory!", $"Player {player.Name} won the game", "OK");

                    await Navigation.PushAsync(new EndPage(player, countOfMoves/2));

                }
            }

           
        }
    }

    private void DrawPlayedCards()
    {
        playedCardsLayout.Children.Clear();

        if (playedCards.Count != 0)
        {
            for (int i = 0; i < playedCards.Count; i += 2)
            {
                var verticalLayout = new VerticalStackLayout();

                var firstCard = playedCards[i];
                verticalLayout.Children.Add(new Image
                {
                    Source = firstCard.GetCardImageSource(firstCard),
                    WidthRequest = 50,
                    HeightRequest = 75
                });

                if (i + 1 < playedCards.Count)
                {
                    var secondCard = playedCards[i + 1];
                    verticalLayout.Children.Add(new Image
                    {
                        Source = secondCard.GetCardImageSource(secondCard),
                        WidthRequest = 50,
                        HeightRequest = 75
                    });
                }

                playedCardsLayout.Children.Add(verticalLayout);
            }
        }
    }


    private void ClearSlots()
    {
        foreach (var slot in slots)
        {
            slot.SlotFrame.Content = null;
        }
    }

    private void GeneratePlayersHandsFrames()
    {
        List<StackLayout> playersHandsStacks = new List<StackLayout> { player1CardsLayout, player2CardsLayout };

        foreach (var stack in playersHandsStacks)
        {

            int index = playersHandsStacks.IndexOf(stack);
            Frame imageFrame = new Frame
            {
                Content = new Image
                {
                    Source = imageOfCardBack.Source,
                    WidthRequest = imageOfCardBack.WidthRequest,
                    HeightRequest = imageOfCardBack.HeightRequest,
                    Aspect = imageOfCardBack.Aspect
                },
                Padding = 0,
                BorderColor = Colors.Transparent,
                HasShadow = false,
                WidthRequest = 110,
                HeightRequest = 160,
            };

            Grid grid = new Grid
            {
                Children =
                {
                    imageFrame,
                    playerCountsLabels[index]
                }
            };
            
            
            imageFrame.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => SelectFrame(imageFrame))
            });
            players[index].HandFrame = imageFrame;

            stack.Children.Add(grid);
        }
    }

    private void SelectFrame(Frame frame)
    {
        if (enableSlots)
        {


            if (playerTurnToMove.HandFrame == frame)
            {
                if (selectedFrame == frame)
                {
                    selectedFrame = null;
                    frame.BorderColor = Colors.Transparent;

                }
                else
                {
                    selectedFrame = frame;
                    frame.BorderColor = Colors.Black;
                }


            }
        }
    }

    private void GenerateSlotsFrames()
    {
        for (int i = 0; i < 2; i++)
        {

            Frame slotFrame = new Frame
            {
                Padding = 0,
                BorderColor = Colors.Black,
                HasShadow = false,
                WidthRequest = 100,
                HeightRequest = 150,
                
            };
            slotFrame.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => MakeTheMove(slotFrame))
            });




            slotsLayout.Children.Add(slotFrame);

            Slot slot= new Slot { SlotFrame = slotFrame, SlotCard = null };

            players[i].Slot = slot;

            slots.Add(slot);



        }
    }

    private List<Card> GenerateDeck(int deckSize)
    {
        Dictionary<string, int> ranks = new Dictionary<string, int>();

        switch (deckSize)
        {
            //case 8:
            //    ranks = new Dictionary<string, int>
            //            {
            //                { "king", 13 }, { "ace", 14 }
            //            };
            //    break;
            case 24:
                ranks = new Dictionary<string, int>
                        {
                            { "nine", 9 },
                            { "ten", 10 }, { "jack", 11 }, { "queen", 12 }, { "king", 13 }, { "ace", 14 }
                        };
                break;
            case 36:
                ranks = new Dictionary<string, int>
                        {

                            { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 },
                            { "ten", 10 }, { "jack", 11 }, { "queen", 12 }, { "king", 13 }, { "ace", 14 }
                        }; ;
                break;
            case 52:
                ranks = new Dictionary<string, int>
                        {
                            { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 },
                            { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 },
                            { "ten", 10 }, { "jack", 11 }, { "queen", 12 }, { "king", 13 }, { "ace", 14 }
                        };
                break;
            default:
                throw new ArgumentException("Invalid deck size");
        }

        string[] suits = { "hearts", "diamonds", "clubs", "spades" };

        var deck = new List<Card>();

        foreach (var suit in suits)
        {
            foreach (var rank in ranks)
            {
                deck.Add(new Card(suit, rank.Key, rank.Value));
            }
        }

        return deck;
    }

    private List<Card> DrawRandomCards(int numberOfCards)
    {
        var random = new Random();
        var randomCards = new List<Card>();

        for (int i = 0; i < numberOfCards; i++)
        {
            var index = random.Next(deckForDrawing.Count);
            randomCards.Add(deckForDrawing[index]);
            deckForDrawing.RemoveAt(index);
        }

        return randomCards;
    }
}
