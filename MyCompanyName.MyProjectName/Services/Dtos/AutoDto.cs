using Volo.Abp.Application.Dtos;

namespace MyCompanyName.MyProjectName.Services.Dtos
{
	public class AutoDto : EntityDto<Guid>
	{
		public string Marca { get; set; }
		public string Modelo { get; set; }
		public string Color { get; set; }
		public int Año { get; set; }
	}
}
