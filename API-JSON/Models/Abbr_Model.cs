namespace API_JSON.Models
{
	public class Abbr_Model
	{
		internal readonly object Abbreviations;

		public string Id { get; set; }
		public string Term { get; set; }
		public string Definition { get; set; }
		public string Category_Name { get; set; }
		public string Parent_Category { get; set; }
		public string Category { get; set; }

	}
}
