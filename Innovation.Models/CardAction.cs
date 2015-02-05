namespace Innovation.Models
{
	public delegate void CardActionDelegate(object[] parameters);

	public class CardAction
	{
		public bool IsDemand { get; set; }
		public CardActionDelegate ActionHandler { get; set; }
	}
}
