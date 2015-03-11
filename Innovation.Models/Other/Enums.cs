namespace Innovation.Models.Enums
{
	public enum ActionEnum
	{
		None,
		Achieve,
		Dogma,
		Draw,
		Meld,
		Surrender
	}

	public enum Symbol
	{
		Blank,
		Clock,
		Crown,
		Factory,
		Leaf,
		Lightbulb,
		Tower,
	}

	public enum Color
	{
		None,
		Blue,
		Green,
		Purple,
		Red,
		Yellow
	}

	public enum SplayDirection
	{
		None,
		Left,
		Right,
		Up
	}
	
	public enum ActionType
	{
		Optional,
		Required,
		Demand
	}

	public enum RequestType
	{
		Action,
		Boolean,
		Card,
		Player,
		Splay
	}

	public enum QueuedActionType
	{
		ImmediateDelegate,
		EndDogma,
		AskQuestion,
		PickCard,
		PickPlayer,
		PickAction,
		Draw,
	}
}
