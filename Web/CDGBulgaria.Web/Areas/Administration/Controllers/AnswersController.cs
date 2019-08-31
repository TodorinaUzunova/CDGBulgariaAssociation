﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Mapping;
using CDGBulgaria.Services.Models;
using CDGBulgaria.Web.Areas.Adminstration.Controllers;
using CDGBulgaria.Web.InputModels.Answer;
using CDGBulgaria.Web.ViewModels.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CDGBulgaria.Web.Areas.Administration.Controllers
{
    public class AnswersController : AdminController
    {
		private readonly IAnswersService answersService;
		private readonly IQuestionsService questionsService;

		public AnswersController(IAnswersService answersService, IQuestionsService questionsService)
		{
			this.answersService = answersService;
			this.questionsService = questionsService;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Create()
		{

			var allQuestions = await this.questionsService.GetAllQuestions().ToListAsync();

			this.ViewData["questions"] = allQuestions.Select(question => new AnswerCreateQuestionViewModel
			{
				Id = question.Id,
				Content = question.Content,
			});
			return this.View();
		}

		
		[HttpPost(Name = "Create")]
		[Authorize]
		public async Task<IActionResult> Create(AnswerCreateInputModel answerCreateInputModel)
		{
			if (!this.ModelState.IsValid)
			{
				var allQuestions = await this.questionsService.GetAllQuestions().ToListAsync();

				this.ViewData["questions"] = allQuestions.Select(question=>new AnswerCreateQuestionViewModel
				{
					//Id = question.Id,
					Content =question.Content,
				});
				return this.View(answerCreateInputModel);
			}
			string authorId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			//string author = this.User.FindFirst(ClaimTypes.Name).Value;

			AnswerServiceModel answerServiceModel = answerCreateInputModel.To<AnswerServiceModel>();
			answerServiceModel.AuthorId = authorId;

			await this.answersService.CreateAnswer(answerServiceModel);
			return this.Redirect("/");
		}
	}
} 