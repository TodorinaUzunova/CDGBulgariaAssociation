using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Services.Models
{
	public class CDGDiseaseTypeServiceModel : IMapFrom<CDGDiseaseType>, IMapTo<CDGDiseaseType>
	{
		public int Id { get; set; }

		public string Name { get; set; }
	}
}
