using Volo.Abp.Domain.Entities;

namespace MyCompanyName.MyProjectName.Entities
{
	public class Auto : BasicAggregateRoot<Guid>
	{
		public string Marca { get; set; }
		public string Modelo { get; set; }
		public string Color { get; set; }
		public int Año { get; set; }
	}
}
