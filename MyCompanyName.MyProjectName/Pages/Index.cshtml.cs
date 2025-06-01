using MyCompanyName.MyProjectName.Services;
using MyCompanyName.MyProjectName.Services.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MyCompanyName.MyProjectName.Pages;

public class IndexModel : AbpPageModel
{
    public List<TodoDto> TodoItems { get; set; }
    public List<PersonaDto> PersonasItems { get; set; }

    private readonly TodoAppService _todoAppService;
    private readonly PersonaAppService _personaAppService;

    public IndexModel(TodoAppService todoAppService, PersonaAppService personaAppService)
    {
        _todoAppService = todoAppService;
        _personaAppService = personaAppService;
    }

    public async Task OnGetAsync()
    {
        TodoItems = await _todoAppService.GetListAsync();
        PersonasItems = await _personaAppService.GetListAsync();
    }
}