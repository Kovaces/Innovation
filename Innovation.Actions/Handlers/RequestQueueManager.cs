using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Actions.Handlers
{
	public class RequestQueueManager
	{
		public static void ReceiveActionResponse(Game game, Player fromPlayer, List<string> answers)
		{
			if (answers.Count <= 0)
				return;

			Request request = game.RequestQueue.PopRequestForPlayerByType(fromPlayer, RequestType.Boolean);
			if (request == null)
				return;

			ActionEnum selectedAction = ActionEnum.None;
			if (Enum.TryParse(answers[0], true, out selectedAction))
			{
				switch (selectedAction)
				{
					case ActionEnum.None:
						return;

					case ActionEnum.Achieve:
						if (!Achieve.Action(fromPlayer, game))
						{
							//message player with failure
						}
						break;

					case ActionEnum.Dogma:
						game.ActionQueue.AddAction(new QueuedAction()
						{
							Game = game,
							ActivePlayer = request.ActivePlayer,
							TargetPlayer = request.TargetPlayer,
							Type = QueuedActionType.PickCard,
							Parameters = new ActionParameters()
							{
								Cards = fromPlayer.Tableau.GetTopCards().ToList(),
								MinSelections = 1,
								MaxSelections = 1,
								ResponseHandler = DogmaResponse
							}
						});
						ActionQueueManager.PopNextAction(game);
						break;

					case ActionEnum.Draw:
						game.ActionQueue.AddAction(new QueuedAction()
						{
							Game = game,
							ActivePlayer = request.ActivePlayer,
							TargetPlayer = request.TargetPlayer,
							Type = QueuedActionType.PickCard,
							Parameters = new ActionParameters()
							{
								Cards = fromPlayer.Tableau.GetTopCards().ToList(),
								MinSelections = 1,
								MaxSelections = 1,
								ResponseHandler = DogmaResponse
							}
						});
						ActionQueueManager.PopNextAction(game);
						break;

					case ActionEnum.Meld:
						game.ActionQueue.AddAction(new QueuedAction()
						{
							Game = game,
							ActivePlayer = request.ActivePlayer,
							TargetPlayer = request.TargetPlayer,
							Type = QueuedActionType.PickCard,
							Parameters = new ActionParameters()
							{
								Cards = fromPlayer.Hand.ToList(),
								MinSelections = 1,
								MaxSelections = 1,
								ResponseHandler = MeldResponse
							}
						});
						ActionQueueManager.PopNextAction(game);
						break;

					case ActionEnum.Surrender:
						throw new NotImplementedException("Never give up!  Never surrender!");
						break;
				}
			}

		}

		public static bool DogmaResponse(CardActionParameters parameters)
		{
			ICard card = parameters.Answer.SingleCard;

			Dogma.Action(card, parameters.TargetPlayer, parameters.Game);

			return true;
		}

		public static bool MeldResponse(CardActionParameters parameters)
		{
			ICard card = parameters.Answer.SingleCard;

			parameters.TargetPlayer.Hand.Remove(card);

			Meld.Action(card, parameters.TargetPlayer);

			return true;
		}




		public static void ReceiveBooleanResponse(Game game, Player fromPlayer, List<string> answers)
		{
			if (answers.Count <= 0)
				return;

			Request request = game.RequestQueue.PopRequestForPlayerByType(fromPlayer, RequestType.Boolean);
			if (request == null)
				return;

			bool answer = bool.Parse(answers[0]);

			if (answer)
			{
				ActionQueueManager.ExecuteCardAction(
					game,
					request.ResponseHandler,
					new CardActionParameters()
					{
						Game = game,
						ActivePlayer = request.ActivePlayer,
						TargetPlayer = request.TargetPlayer,
						PlayerSymbolCounts = request.PlayerSymbolCounts,
						Answer = new GenericAnswer() { Boolean = answer }
					}
				);
			}
		}

		public static void ReceiveCardResponse(Game game, Player fromPlayer, List<string> answers)
		{
			if (answers.Count <= 0)
				return;

			Request request = game.RequestQueue.PopRequestForPlayerByType(fromPlayer, RequestType.Boolean);
			if (request == null)
				return;

			List<string> possibleAnswers = ((List<ICard>)request.Objects).Select(x => x.ID).ToList();
			if (answers.Any(x => !possibleAnswers.Contains(x)))
			{
				return;                              // invalid answer returned
			}

			List<ICard> possibleCards = (List<ICard>)request.Objects;
			List<ICard> selectedCards = possibleCards.Where(x => answers.Contains(x.ID)).ToList();

			ActionQueueManager.ExecuteCardAction(
				game,
				request.ResponseHandler,
				new CardActionParameters()
				{
					Game = game,
					ActivePlayer = request.ActivePlayer,
					TargetPlayer = request.TargetPlayer,
					PlayerSymbolCounts = request.PlayerSymbolCounts,

					Answer = new GenericAnswer() { MultipleCards = selectedCards }
				}
			);
		}

		public static void ReceivePlayerResponse(Game game, Player fromPlayer, List<string> answers)
		{
			if (answers.Count <= 0)
				return;

			Request request = game.RequestQueue.PopRequestForPlayerByType(fromPlayer, RequestType.Boolean);

			if (request == null)
				return;

			List<string> possibleAnswers = ((List<IPlayer>)request.Objects).Select(x => x.Id).ToList();
			if (answers.Any(x => !possibleAnswers.Contains(x)))
			{
				return;                              // invalid answer returned
			}

			List<IPlayer> possiblePlayers = (List<IPlayer>)request.Objects;
			List<IPlayer> selectedPlayers = possiblePlayers.Where(x => answers.Contains(x.Id)).ToList();

			ActionQueueManager.ExecuteCardAction(
				game,
				request.ResponseHandler,
				new CardActionParameters()
				{
					Game = game,
					ActivePlayer = request.ActivePlayer,
					TargetPlayer = request.TargetPlayer,
					PlayerSymbolCounts = request.PlayerSymbolCounts,

					Answer = new GenericAnswer() { Players = selectedPlayers }
				}
			);
		}

		public static void ReceiveSplayResponse(Game game, Player fromPlayer, List<string> answers)
		{
			if (answers.Count <= 0)
				return;

			Request request = game.RequestQueue.PopRequestForPlayerByType(fromPlayer, RequestType.Splay);

			Color selectedColor = Color.None;
			if (Enum.TryParse(answers[0], true, out selectedColor))
			{
				List<Color> possibleColors = (List<Color>)request.Objects;
				if (!possibleColors.Contains(selectedColor))
					return;

				if (selectedColor == Color.None)
					return;

				ActionQueueManager.ExecuteCardAction(
					game,
					request.ResponseHandler,
					new CardActionParameters()
					{
						Game = game,
						ActivePlayer = request.ActivePlayer,
						TargetPlayer = request.TargetPlayer,
						PlayerSymbolCounts = request.PlayerSymbolCounts,

						Answer = new GenericAnswer() { Color = selectedColor }
					}
				);
			}
		}




		public static void PickCards(Game game, IPlayer questionedPlayer, IPlayer activePlayer, IPlayer targetPlayer, IEnumerable<ICard> cards, int minimumNumberToSelect, int maximumNumberToSelect, Dictionary<IPlayer, Dictionary<Symbol, int>> playerSymbolCounts, CardActionDelegate cardActionDelegate)
		{
			game.RequestQueue.AddRequest(new Request()
			{
				Type = RequestType.Card,

				QuestionedPlayer = questionedPlayer,
				ActivePlayer = activePlayer,
				TargetPlayer = targetPlayer,
				PlayerSymbolCounts = playerSymbolCounts,

				Objects = cards,
				MinimumNumberToSelect = minimumNumberToSelect,
				MaximumNumberToSelect = maximumNumberToSelect,

				ResponseHandler = cardActionDelegate
			});

			questionedPlayer.PickCard(cards);
		}

		public static void AskToSplay(Game game, IPlayer questionedPlayer, IPlayer activePlayer, IPlayer targetPlayer, List<Color> colorsToSplay, SplayDirection splayDirection, Dictionary<IPlayer, Dictionary<Symbol, int>> playerSymbolCounts, CardActionDelegate cardActionDelegate)
		{
			game.RequestQueue.AddRequest(new Request()
			{
				Type = RequestType.Splay,

				QuestionedPlayer = questionedPlayer,
				ActivePlayer = activePlayer,
				TargetPlayer = targetPlayer,
				PlayerSymbolCounts = playerSymbolCounts,

				Objects = colorsToSplay,

				ResponseHandler = cardActionDelegate
			});

			questionedPlayer.AskToSplay(colorsToSplay, splayDirection);
		}
	}
}