using Volo.Abp.Application.Dtos;

namespace MyCompanyName.MyProjectName.Services.Dtos
{
    public class PersonaDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
