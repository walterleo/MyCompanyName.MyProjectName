using Volo.Abp.Domain.Entities;

namespace MyCompanyName.MyProjectName.Entities
{
    public class Persona : BasicAggregateRoot<Guid>
    {
        public string Name{ get; set; }
        public string LastName { get; set; }
    }
}
