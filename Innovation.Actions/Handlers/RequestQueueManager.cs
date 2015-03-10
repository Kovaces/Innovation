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
		public static void ReceiveActionResponse(Game game, IPlayer fromPlayer, string answer)
		{
			Request request = game.RequestQueue.PopRequestForPlayerByType(fromPlayer, RequestType.Boolean);
			if (request == null)
				return;

			ActionEnum selectedAction = ActionEnum.None;
			if (Enum.TryParse(answer, true, out selectedAction))
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

		public static CardActionResults DogmaResponse(CardActionParameters parameters)
		{
			ICard card = parameters.Answer.SingleCard;

			Dogma.Action(card, parameters.TargetPlayer, parameters.Game);

			return new CardActionResults(true,false);
		}

		public static CardActionResults MeldResponse(CardActionParameters parameters)
		{
			ICard card = parameters.Answer.SingleCard;

			parameters.TargetPlayer.Hand.Remove(card);

			Meld.Action(card, parameters.TargetPlayer);

			return new CardActionResults(true, false);
		}




		public static void ReceiveBooleanResponse(Game game, IPlayer fromPlayer, Boolean answer)
		{
			Request request = game.RequestQueue.PopRequestForPlayerByType(fromPlayer, RequestType.Boolean);
			if (request == null)
				return;

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

		public static void ReceiveCardResponse(Game game, IPlayer fromPlayer, List<string> answers)
		{
			if (answers.Count <= 0)
				throw new ArgumentNullException("Invalid choice.");

			Request request = game.RequestQueue.PopRequestForPlayerByType(fromPlayer, RequestType.Boolean);
			if (request == null)
				return;

			List<string> possibleAnswers = ((List<ICard>)request.Objects).Select(x => x.Id).ToList();
			if (answers.Any(x => !possibleAnswers.Contains(x)))
				throw new ArgumentNullException("Invalid choice.");

			List<ICard> possibleCards = (List<ICard>)request.Objects;
			List<ICard> selectedCards = possibleCards.Where(x => answers.Contains(x.Id)).ToList();

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

		public static void ReceivePlayerResponse(Game game, IPlayer fromPlayer, List<string> answers)
		{
			if (answers.Count <= 0)
				return;

			Request request = game.RequestQueue.PopRequestForPlayerByType(fromPlayer, RequestType.Boolean);

			if (request == null)
				throw new ArgumentNullException("Invalid choice.");

			List<string> possibleAnswers = ((List<IPlayer>)request.Objects).Select(x => x.Id).ToList();
			if (answers.Any(x => !possibleAnswers.Contains(x)))
				throw new ArgumentNullException("Invalid choice.");

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

		public static void ReceiveSplayResponse(Game game, IPlayer fromPlayer, string answer)
		{
			Request request = game.RequestQueue.PopRequestForPlayerByType(fromPlayer, RequestType.Splay);

			Color selectedColor = Color.None;
			if (Enum.TryParse(answer, true, out selectedColor))
			{
				List<Color> possibleColors = (List<Color>)request.Objects;
				if (!possibleColors.Contains(selectedColor))
					throw new ArgumentNullException("Invalid choice.");

				if (selectedColor == Color.None)
					throw new ArgumentNullException("Invalid choice.");

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




		public static void PickCards(Game game, IPlayer activePlayer, IPlayer targetPlayer, IEnumerable<ICard> cards, int minimumNumberToSelect, int maximumNumberToSelect, Dictionary<IPlayer, Dictionary<Symbol, int>> playerSymbolCounts, CardActionDelegate cardActionDelegate)
		{
			game.IsWaiting = true;

			game.RequestQueue.AddRequest(new Request()
			{
				Type = RequestType.Card,

				ActivePlayer = activePlayer,
				TargetPlayer = targetPlayer,
				PlayerSymbolCounts = playerSymbolCounts,

				Objects = cards,
				MinimumNumberToSelect = minimumNumberToSelect,
				MaximumNumberToSelect = maximumNumberToSelect,

				ResponseHandler = cardActionDelegate
			});

			targetPlayer.PickMultipleCards(cards, minimumNumberToSelect, maximumNumberToSelect);
		}

		public static void AskToSplay(Game game, IPlayer activePlayer, IPlayer targetPlayer, List<Color> colorsToSplay, SplayDirection splayDirection, Dictionary<IPlayer, Dictionary<Symbol, int>> playerSymbolCounts, CardActionDelegate cardActionDelegate)
		{
			game.IsWaiting = true;
			game.RequestQueue.AddRequest(new Request()
			{
				Type = RequestType.Splay,

				ActivePlayer = activePlayer,
				TargetPlayer = targetPlayer,
				PlayerSymbolCounts = playerSymbolCounts,

				Objects = colorsToSplay,

				ResponseHandler = cardActionDelegate
			});

			targetPlayer.AskToSplay(colorsToSplay, splayDirection);
		}

		public static void PickAction(Game game, IPlayer activePlayer)
		{
			game.IsWaiting = true;
			game.RequestQueue.AddRequest(new Request()
			{
				Type = RequestType.Action,

				ActivePlayer = activePlayer,
				TargetPlayer = activePlayer,
			});

			activePlayer.PickAction();
		}

		public static void AskQuestion(Game game, IPlayer activePlayer, IPlayer targetPlayer, string question, Dictionary<IPlayer, Dictionary<Symbol, int>> playerSymbolCounts, CardActionDelegate cardActionDelegate)
		{
			game.IsWaiting = true;
			game.RequestQueue.AddRequest(new Request()
			{
				Type = RequestType.Boolean,

				ActivePlayer = activePlayer,
				TargetPlayer = targetPlayer,
				PlayerSymbolCounts = playerSymbolCounts,

				Objects = question
			});

			activePlayer.AskQuestion(question);
		}

		public static void PickPlayer(Game game, IPlayer activePlayer, IPlayer targetPlayer, List<IPlayer> players, int minimumNumberToSelect, int maximumNumberToSelect, Dictionary<IPlayer, Dictionary<Symbol, int>> playerSymbolCounts, CardActionDelegate cardActionDelegate)
		{
			game.IsWaiting = true;
			game.RequestQueue.AddRequest(new Request()
			{
				Type = RequestType.Boolean,

				ActivePlayer = activePlayer,
				TargetPlayer = targetPlayer,
				MinimumNumberToSelect = minimumNumberToSelect,
				MaximumNumberToSelect = maximumNumberToSelect,
				PlayerSymbolCounts = playerSymbolCounts,

				Objects = players
			});

			activePlayer.PickPlayer(players, minimumNumberToSelect, maximumNumberToSelect);
		}
	}
}