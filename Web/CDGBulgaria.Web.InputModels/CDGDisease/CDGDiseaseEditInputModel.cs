using AspNetCoreTemplate.Services.Mapping;
using AutoMapper;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Web.InputModels.CDGDisease
{
	public class CDGDiseaseEditInputModel:IMapFrom<CDGDiseaseServiceModel>, IMapTo<CDGDiseaseServiceModel>, IHaveCustomMappings
	{
		

		[Required(ErrorMessage = "Name is required!")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Description is required!")]
		public string Description { get; set; }

		[Required(ErrorMessage = "CDGDiseaseType is required!")]
		public string CDGDiseaseType { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<CDGDiseaseServiceModel, CDGDiseaseEditInputModel>()
				.ForMember(destination => destination.CDGDiseaseType,
				opts => opts.MapFrom(origin => origin.CDGDiseaseType.Name));

			configuration.CreateMap<CDGDiseaseEditInputModel, CDGDiseaseServiceModel>()
				.ForMember(destination => destination.CDGDiseaseType,
				opts => opts.MapFrom(origin => new CDGDiseaseTypeServiceModel { Name = origin.Name }));

			
		}
	}
}
