namespace IndustryIncident.Models.ViewModels
{
	public class IncidentViewModel
	{
		public int Id { get; set; }

		public string Iduser { get; set; }

		public IncidentType Type { get; set; }

		public string Description { get; set; } = null!;

		public Zone Zone { get; set; }

		public DateTime Date { get; set; }

		public Indicator Indicator { get; set; }

        public decimal? Objectif { get; set; }

        public decimal? Taux { get; set; }


    }
}
