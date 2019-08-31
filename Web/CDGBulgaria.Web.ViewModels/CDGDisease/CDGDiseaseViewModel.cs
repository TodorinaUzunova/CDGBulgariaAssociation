using AspNetCoreTemplate.Services.Mapping;
using AutoMapper;
using CDGBulgaria.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDGBulgaria.Web.ViewModels.CDGDisease
{
	public class CDGDiseaseViewModel:IMapFrom<CDGDiseaseServiceModel>, IHaveCustomMappings
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string CDGDiseaseTypeName  { get; set; }

	

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<CDGDiseaseServiceModel, CDGDiseaseViewModel>()
				.ForMember(destination=>destination.CDGDiseaseTypeName,
				opts=>opts.MapFrom(origin=>origin.CDGDiseaseType.Name));
		}
	}
}
