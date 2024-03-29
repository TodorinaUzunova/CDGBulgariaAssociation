﻿using AspNetCoreTemplate.Services.Mapping;
using AutoMapper;
using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Web.InputModels.Answer
{
	public class AnswerCreateInputModel:IMapTo<AnswerServiceModel>, IHaveCustomMappings
	{
		[Required(ErrorMessage = "Content is required!")]
		public string Content { get; set; }


		[Required(ErrorMessage = "QuestionContent is required!")]
		public string QuestionContent { get; set; }

		public string QuestionId { get; set; }

		public string AuthorId { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<AnswerCreateInputModel, AnswerServiceModel>()
				.ForMember(destination => destination.Question, 
				           opts => opts.MapFrom(origin => new QuestionServiceModel() {Content=origin.QuestionContent}));	    
		}
	}
}
