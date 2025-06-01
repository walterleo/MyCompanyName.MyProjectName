// /Services/AutoAppService.cs
using MyCompanyName.MyProjectName.Entities;
using MyCompanyName.MyProjectName.Services.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace MyCompanyName.MyProjectName.Services
{
	public class AutoAppService : ApplicationService
	{
		private readonly IRepository<Auto, Guid> _autoRepository;

		public AutoAppService(IRepository<Auto, Guid> autoRepository)
		{
			_autoRepository = autoRepository;
		}

		// GET: api/app/auto
		public async Task<List<AutoDto>> GetListAsync()
		{
			var items = await _autoRepository.GetListAsync();
			return ObjectMapper.Map<List<Auto>, List<AutoDto>>(items);
		}

		// <<< Cambiamos a *UN SOLA* entidad DTO como parámetro >>>
		public async Task<AutoDto> CreateAsync(CreateUpdateAutoDto input)
		{
			var nuevoAuto = new Auto
			{
				Marca = input.Marca,
				Color = input.Color,
				Modelo = input.Modelo,
				Año = input.Anio  // asignamos al campo “Año” de tu entidad Auto
			};

			var creado = await _autoRepository.InsertAsync(nuevoAuto);
			return ObjectMapper.Map<Auto, AutoDto>(creado);
		}

		// DELETE: api/app/auto/{id}
		public async Task DeleteAsync(Guid id)
		{
			await _autoRepository.DeleteAsync(id);
		}

		// PUT: api/app/auto/{id}
		public async Task<AutoDto> UpdateAsync(Guid id, CreateUpdateAutoDto input)
		{
			var auto = await _autoRepository.GetAsync(id);
			auto.Marca = input.Marca;
			auto.Color = input.Color;
			auto.Modelo = input.Modelo;
			auto.Año = input.Anio;

			var actualizado = await _autoRepository.UpdateAsync(auto);
			return ObjectMapper.Map<Auto, AutoDto>(actualizado);
		}
	}
}
