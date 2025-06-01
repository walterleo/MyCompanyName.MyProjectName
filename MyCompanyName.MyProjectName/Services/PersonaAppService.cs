using MyCompanyName.MyProjectName.Entities;
using MyCompanyName.MyProjectName.Services.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace MyCompanyName.MyProjectName.Services
{
    public class PersonaAppService : ApplicationService
    {
        private readonly IRepository<Persona, Guid> _personaRepository;

        public PersonaAppService(IRepository<Persona, Guid> personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<List<PersonaDto>> GetListAsync() 
        { 
            var items=await _personaRepository.GetListAsync();

            return ObjectMapper.Map<List<Persona>, List<PersonaDto>>(items);
        }
        public async Task<PersonaDto> GetAsync(Guid id) { 

            var personaItem=await _personaRepository.GetAsync(id);

            if (personaItem == null) {
                throw new EntityNotFoundException(typeof(Persona), id);
            }

            return ObjectMapper.Map<Persona,PersonaDto>(personaItem);
        }

        public async Task<PersonaDto> CreateAsync(string name, string lastName) {

            var personaItem = await _personaRepository.InsertAsync(
                    new Persona { Name = name, LastName = lastName }
                );
            return ObjectMapper.Map<Persona, PersonaDto>(personaItem);
        }

        public Task DeleteAsync(Guid id) { 
            return _personaRepository.DeleteAsync(id);
        }

        public async Task<PersonaDto> Update(Guid id, string name, string lastName) { 
            var personaItem=await _personaRepository.GetAsync(id);

            if (personaItem == null) {
                // Manejar el caso en que no se encuentra la persona con el ID proporcionado
                // Puedes lanzar una excepción o devolver un valor nulo según tu lógica.
                throw new EntityNotFoundException(typeof(Persona), id);
            }
            personaItem.LastName = lastName;
            personaItem.Name = name;

            await _personaRepository.UpdateAsync(personaItem);
            return ObjectMapper.Map<Persona,PersonaDto>(personaItem);
        }
    }
}
