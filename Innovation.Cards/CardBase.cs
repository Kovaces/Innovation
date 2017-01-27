using Innovation.Actions;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Innovation.Game;
using Innovation.Interfaces;
using Innovation.Storage;


namespace Innovation.Cards
{
    public abstract class CardBase : ICard
    {
        public string Id => "C_" + new Regex("[^a-zA-Z0-9]").Replace(Name, "");

        public abstract Symbol Top { get; }
        public abstract Symbol Left { get; }
        public abstract Symbol Center { get; }
        public abstract Symbol Right { get; }

        public abstract string Name { get; }
        public abstract int Age { get; }
        public abstract Color Color { get; }
        public abstract IEnumerable<ICardAction> Actions { get; }

        public bool HasSymbol(Symbol symbol)
        {
            return ((Top == symbol) || (Left == symbol) || (Center == symbol) || (Right == symbol));
        }

        //Protected Properties and Methods
        protected void ValidateParameters(ICardActionParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (parameters.TargetPlayer == null)
                throw new ArgumentOutOfRangeException(nameof(parameters), "Target player cannot be null");
            
            if (parameters.ActivePlayer == null)
                throw new ArgumentOutOfRangeException(nameof(parameters), "Active player cannot be null");

            if (parameters.AgeDecks == null)
                throw new ArgumentOutOfRangeException(nameof(parameters), "Age Decks cannot be null");
        }

        protected ICard DrawAndReveal(ICardActionParameters parameters, int age)
        {
            var drawnCard = Draw.Action(age, parameters.AgeDecks);

            parameters.TargetPlayer.Interaction.RevealCard(parameters.TargetPlayer.Id, drawnCard);

            return drawnCard;
        }

        protected void AskToSplay(ICardActionParameters parameters, Color color, SplayDirection direction)
        {
            ValidateParameters(parameters);

            var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, String.Format("You may splay your {0} cards {1}.", color, direction));
            if (!answer.HasValue || !answer.Value)
            {
                return;
            }

            PlayerActed(parameters);

            parameters.TargetPlayer.SplayStack(color, direction);
        }

        protected void PlayerActed(ICardActionParameters parameters)
        {
            if (parameters.TargetPlayer != parameters.ActivePlayer)
                parameters.AddToStorage("AnotherPlayerTookDogmaActionKey", true);
        }
    }
}
