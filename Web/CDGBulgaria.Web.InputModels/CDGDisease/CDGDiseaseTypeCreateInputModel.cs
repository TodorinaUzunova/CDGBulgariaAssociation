using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Web.InputModels.CDGDisease
{
	public class CDGDiseaseTypeCreateInputModel : IMapTo<CDGDiseaseTypeServiceModel>
	{
		[Required(ErrorMessage = "CDGDiseaseTypeName is required!")]
		public string Name { get; set; }
	}

}
