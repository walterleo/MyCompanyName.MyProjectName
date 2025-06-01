// Pages/Auto/Index.cshtml.cs
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCompanyName.MyProjectName.Services;
using MyCompanyName.MyProjectName.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MyCompanyName.MyProjectName.Web.Pages.Auto
{
	public class IndexModel : AbpPageModel
	{
		public List<AutoDto> AutoItems { get; set; }
		private readonly AutoAppService _autoAppService;

		public IndexModel(AutoAppService autoAppService)
		{
			_autoAppService = autoAppService;
		}

		public async Task OnGetAsync()
		{
			AutoItems = await _autoAppService.GetListAsync();
		}
	}
}
