
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Interfaces;


namespace Innovation.Tests.Helpers
{
    public class Mocks
    {
        public static void ConvertPlayersToMock(Game.Game game)
        {
            //for (int i = 0; i < game.Players.Count; i++)
            //{
            //    IPlayer mockPlayer = MockRepository.GenerateStub<IPlayer>();
            //    mockPlayer.Hand = game.Players[i].Hand;
            //    mockPlayer.Tableau = game.Players[i].Tableau;
            //    mockPlayer.Name = game.Players[i].Name;
            //    mockPlayer.Team = game.Players[i].Team;

            //    mockPlayer.Stub(p => p.AskToSplay(Arg<List<Color>>.Is.Anything, Arg<SplayDirection>.Is.Anything))
            //            .Return(Color.None)
            //            .WhenCalled(p =>
            //            {
            //                List<Color> colors = (List<Color>)p.Arguments[0];
            //                p.ReturnValue = colors.First();
            //            }
            //        );

            //    mockPlayer.Stub(p => p.AskQuestion(Arg<string>.Is.Anything)).Return(true);
            //    mockPlayer.Stub(p => p.PickCardFromHand()).Return(mockPlayer.Hand.First());
            //    mockPlayer.Stub(p => p.PickPlayer(Arg<List<IPlayer>>.Is.Anything, Arg<int>.Is.Anything, Arg<int>.Is.Anything))
            //            .Return(null)
            //            .WhenCalled(p =>
            //                {
            //                    List<IPlayer> players = (List<IPlayer>)p.Arguments[0];
            //                    int min = (int)p.Arguments[1];
            //                    int max = (int)p.Arguments[2];
            //                    p.ReturnValue = players.Take(Math.Max(1,min));
            //                }
            //        );
            //    mockPlayer.Stub(p => p.PickCard(Arg<List<ICard>>.Is.Anything))
            //            .Return(null)
            //            .WhenCalled(p =>
            //            {
            //                List<ICard> cards = (List<ICard>)p.Arguments[0];
            //                p.ReturnValue = cards.First();
            //            }
            //        );
            //    mockPlayer.Stub(p => p.PickMultipleCards(Arg<List<ICard>>.Is.Anything, Arg<int>.Is.Anything, Arg<int>.Is.Anything))
            //            .Return(null)
            //            .WhenCalled(p =>
            //            {
            //                List<ICard> cards = (List<ICard>)p.Arguments[0];
            //                p.ReturnValue = cards.Take(Math.Max((int)p.Arguments[1], 1)).ToList();
            //            }
            //        );

            //    game.Players[i] = mockPlayer;
            //}
        }

        public static void PlayerDrawsCards(IPlayer player, int numberOfCards)
        {
            //player.Stub(p => p.PickMultipleCards(Arg<List<ICard>>.Is.Anything, Arg<int>.Is.Anything, Arg<int>.Is.Anything))
            //        .Return(null)
            //        .WhenCalled(p =>
            //        {
            //            List<ICard> cards = (List<ICard>)p.Arguments[0];
            //            p.ReturnValue = cards.Take(numberOfCards).ToList();
            //        }
            //    ).Repeat.Any();
        }
    }
}
