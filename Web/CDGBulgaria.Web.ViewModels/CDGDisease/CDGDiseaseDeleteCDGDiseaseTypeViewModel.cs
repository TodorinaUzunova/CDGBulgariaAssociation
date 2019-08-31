using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Web.ViewModels.CDGDisease
{
	public class CDGDiseaseDeleteCDGDiseaseTypeViewModel : IMapFrom<CDGDiseaseTypeServiceModel>
	{

		public string Name { get; set; }
	}

}
