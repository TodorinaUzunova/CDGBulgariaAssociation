using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDGBulgaria.Web.Areas.Adminstration.Controllers;
using Microsoft.AspNetCore.Mvc;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Mapping;
using Microsoft.AspNetCore.Authorization;
using CDGBulgaria.Web.ViewModels.CDGDisease;
using CDGBulgaria.Web.InputModels.CDGDisease;
using CDGBulgaria.Services.Models;
using Microsoft.EntityFrameworkCore;


namespace CDGBulgaria.Web.Areas.Administration.Controllers
{
	public class DiseasesController : AdminController
	{
		private readonly IDiseasesService diseasesService;

		public DiseasesController(IDiseasesService diseasesService)
		{
			this.diseasesService = diseasesService;
		}

		[HttpGet("/Administration/Diseases/Type/Create")]
		public async Task<IActionResult> CreateType()
		{
			return this.View("Type/Create");
		}

		[HttpPost("/Administration/Diseases/Type/Create")]
		public async Task<IActionResult> CreateType(CDGDiseaseTypeCreateInputModel cdgDiseaseTypeCreateInputModel)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View("Type/Create", cdgDiseaseTypeCreateInputModel);
			}

			CDGDiseaseTypeServiceModel cdgDiseaseTypeServiceModel = cdgDiseaseTypeCreateInputModel.To<CDGDiseaseTypeServiceModel>();
			await this.diseasesService.CreateDiseaseType(cdgDiseaseTypeServiceModel);
			return this.Redirect("/");
		}
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Create()
		{

			var allCDGDiseaseTypes = await this.diseasesService.GetAllTypes().ToListAsync();

			this.ViewData["types"] = allCDGDiseaseTypes.Select(
				diseaseType=> new CDGDiseaseCreateCDGDiseaseTypeViewModel
				{
					Name = diseaseType.Name
				})
				.ToList();

			return this.View();
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Create(CDGDiseaseCreateInputModel cdgDiseaseCreateInputModel)
		{
			if (!this.ModelState.IsValid)
			{
				var allCDGDiseaseTypes = await this.diseasesService.GetAllTypes().ToListAsync();

				this.ViewData["types"] = allCDGDiseaseTypes.Select(
					diseaseType => new CDGDiseaseCreateCDGDiseaseTypeViewModel
					{
						Name = diseaseType.Name
					})
					.ToList();

				return this.View();
			}
			

			CDGDiseaseServiceModel cdgDiseaseServiceModel = cdgDiseaseCreateInputModel.To<CDGDiseaseServiceModel>();
			await this.diseasesService.CreateDisease(cdgDiseaseServiceModel);
			return this.Redirect("/Diseases/All");
		}


		[HttpGet(Name = "Edit")]
		public async Task<IActionResult> Edit(int id)
		{
			CDGDiseaseEditInputModel cdgDiseaseEditInputModel = (await this.diseasesService.GetCDGDiseaseById(id)).To<CDGDiseaseEditInputModel>();

			if (cdgDiseaseEditInputModel == null)
			{
				return this.Redirect("/");
				throw new ArgumentNullException(nameof(cdgDiseaseEditInputModel));
			}

			var allCDGDiseaseTypes = await this.diseasesService.GetAllTypes().ToListAsync();

			this.ViewData["types"] = allCDGDiseaseTypes.Select(cdgType => new CDGDiseaseCreateCDGDiseaseTypeViewModel
			{
				Name = cdgType.Name
  			})
				.ToList();
			return this.View(cdgDiseaseEditInputModel);

		}

		[HttpPost(Name = "Edit")]
		public async Task<IActionResult> Edit(int id, CDGDiseaseEditInputModel diseaseEditInputModel)
		{

			if (!this.ModelState.IsValid)
			{
				var allCDGDiseaseTypes = await this.diseasesService.GetAllTypes().ToListAsync();

				this.ViewData["types"] = allCDGDiseaseTypes.Select(cdgType => new CDGDiseaseCreateCDGDiseaseTypeViewModel()
				{
					Name = cdgType.Name
				})
			  .ToList();
				return this.View(diseaseEditInputModel);
			}

			CDGDiseaseServiceModel diseaseServiceModel = diseaseEditInputModel.To<CDGDiseaseServiceModel>();

			await this.diseasesService.Edit(id, diseaseServiceModel);

			return this.Redirect("/");
		}

		[HttpGet(Name = "Delete")]
		public async Task<IActionResult> Delete(int id)
		{

			CDGDiseaseDeleteModel cdgDiseaseDeleteViewModel = (await this.diseasesService.GetCDGDiseaseById(id)).To<CDGDiseaseDeleteModel>();

			if (cdgDiseaseDeleteViewModel == null)
			{
				return this.Redirect("/");
				throw new ArgumentNullException(nameof(cdgDiseaseDeleteViewModel));
			}
			var allCDGDiseaseTypes = await this.diseasesService.GetAllTypes().ToListAsync();

			this.ViewData["types"] = allCDGDiseaseTypes.Select(cdgType => new CDGDiseaseCreateCDGDiseaseTypeViewModel()
			{
				Name = cdgType.Name
			})
		  .ToList();
			return this.View(cdgDiseaseDeleteViewModel);
		}


		//[HttpGet(Name = "Delete")]
		//public async Task<IActionResult> Delete(int id)
		//{

		//	CDGDiseaseDeleteModel cdgDiseaseDeleteViewModel = (await this.diseasesService.GetCDGDiseaseById(id)).To<CDGDiseaseDeleteModel>();

		//	if (cdgDiseaseDeleteViewModel == null)
		//	{
		//		return this.Redirect("/");
		//		throw new ArgumentNullException(nameof(cdgDiseaseDeleteViewModel));
		//	}

		//	return this.View(cdgDiseaseDeleteViewModel);
		//}

		[HttpPost]
		[Route("/Administration/Diseases/Delete/{id}")]
		public async Task<IActionResult> DeleteConfirm(int id)
		{

			await this.diseasesService.Delete(id);
			return this.Redirect("/Diseases/All");
		}

	}
}