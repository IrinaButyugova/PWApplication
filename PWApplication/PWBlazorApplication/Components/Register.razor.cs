using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using PWBlazorApplication.Models;
using PWApplication.BLL.Services;
using PWApplication.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace PWBlazorApplication.Components
{
	public partial class Register
	{
		[Inject]
		IAccountService AccountService { get; set; }
		[Inject]
		NavigationManager Navigation { get; set; }

		[Parameter]
		public EventCallback OnCancelClickCallback { get; set; }

		private RegisterModel registerModel = new RegisterModel();
		private EditContext? editContext;
		private ValidationMessageStore? messageStore;

		protected override void OnInitialized()
		{
			editContext = new(registerModel);
			messageStore = new(editContext);
		}

		private async void ValidSubmit()
		{
			messageStore.Clear();

			if (editContext == null || !editContext.Validate())
			{
				return;
			}

			var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<RegisterModel, User>()
					   .ForMember("UserName", opt => opt.MapFrom(x => x.Name)));
			var mapper = new Mapper(mapperConfig);
			var user = mapper.Map<RegisterModel, User>(registerModel);
			var result = await AccountService.Register(user, registerModel.Password);

			if (result.Succeeded)
			{
				Guid key = Guid.NewGuid();
				var loginModel = new LoginModel()
				{
					Email = registerModel.Email,
					Password = registerModel.Password
				};
				BlazorCookieLoginMiddleware.Logins[key] = loginModel;
				Navigation.NavigateTo($"/login?key={key}", true);
			}
			else
			{
				foreach (var error in result.Errors)
				{
					messageStore.Add(() => registerModel.Email, error.Description);
				}
			}

			if (editContext.GetValidationMessages().Count() > 0)
			{
				editContext.NotifyValidationStateChanged();
			}
		}
	}
}
