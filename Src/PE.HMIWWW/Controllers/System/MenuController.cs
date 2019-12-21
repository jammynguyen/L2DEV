using PE.HMIWWW.Core;
using PE.Services.System;
using SMF.DbEntity;
using SMF.DbEntity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.ViewModel.System;
using System.Threading.Tasks;

namespace PE.HMIWWW.Controllers.System
{
	public class MenuController : BaseController
	{
		#region services

		IMainMenuService _mainMenuService;

		#endregion
		#region ctor
		public MenuController(IMainMenuService mainMenuService)
		{
			_mainMenuService = mainMenuService;
		}

		#endregion
		#region interface
	
		public ActionResult Index()
		{
            VM_Menu vMenu = _mainMenuService.GetMainMenu(ModelState, User.Identity.Name);
            return PartialView("menu/_MainMenu", vMenu);
        }
	}

	#endregion
}
