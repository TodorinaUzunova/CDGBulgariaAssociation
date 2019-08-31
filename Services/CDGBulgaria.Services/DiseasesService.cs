using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Data;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services.Contracts;
using CDGBulgaria.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDGBulgaria.Services
{
	public class DiseasesService : IDiseasesService
	{
		private readonly CDGBulgariaDbContext context;

		public DiseasesService(CDGBulgariaDbContext context)
		{
			this.context = context;
		}

		public async Task<bool> CreateDiseaseType(CDGDiseaseTypeServiceModel cdgDiseaseTypeServiceModel)
		{
			CDGDiseaseType diseaseType = new CDGDiseaseType()
			{
				Name = cdgDiseaseTypeServiceModel.Name
			};
			await this.context.CDGDiseaseTypes.AddAsync(diseaseType);

			int result = await this.context.SaveChangesAsync();
			return result > 0; ;
		}
		public async Task<bool> CreateDisease(CDGDiseaseServiceModel cdgDiseaseServiceModel)
		{
			CDGDiseaseType diseaseTypeFromDb =
			   context.CDGDiseaseTypes
			   .SingleOrDefault(diseaseType => diseaseType.Name == cdgDiseaseServiceModel.CDGDiseaseType.Name);

			if (diseaseTypeFromDb== null)
			{
				throw new ArgumentNullException(nameof(diseaseTypeFromDb));
			}
			//CDGDisease cdgDisease = AutoMapper.Mapper.Map<CDGDisease>(cdgDiseaseServiceModel);
			CDGDisease cdgDisease = new CDGDisease()
			{
				Name = cdgDiseaseServiceModel.Name,
				Description = cdgDiseaseServiceModel.Description,
				CDGDiseaseType = diseaseTypeFromDb,
			};
			cdgDisease.CDGDiseaseType = diseaseTypeFromDb;
			await this.context.CDGDiseases.AddAsync(cdgDisease);

			int result = await this.context.SaveChangesAsync();
			return result > 0;
		}

		

		public IQueryable<CDGDiseaseTypeServiceModel> GetAllTypes()
		{
			 var cdgDiseaseTypes = this.context.CDGDiseaseTypes.To<CDGDiseaseTypeServiceModel>();

			return cdgDiseaseTypes;
		}

		public async Task<bool> Delete(int id)
		{
			CDGDisease diseaseFromDb = await this.context.CDGDiseases.SingleOrDefaultAsync(a => a.Id == id);

			if (diseaseFromDb == null)
			{
				throw new ArgumentNullException(nameof(diseaseFromDb));
			}

			this.context.CDGDiseases.Remove(diseaseFromDb);
			int result = await this.context.SaveChangesAsync();

			return result > 0;
		}

		public async Task<bool> Edit(int id, CDGDiseaseServiceModel diseaseServiceModel)
		{
		var  diseaseTypeFromDb =
			   context.CDGDiseaseTypes
			   .SingleOrDefault(diseaseType => diseaseType.Name == diseaseServiceModel.CDGDiseaseType.Name);

			if (diseaseTypeFromDb == null)
			{
				throw new ArgumentNullException(nameof(diseaseTypeFromDb));
			}

			CDGDisease diseaseFromDb = await this.context.CDGDiseases.SingleOrDefaultAsync(d => d.Id == id);

			if (diseaseFromDb == null)
			{
				throw new ArgumentNullException(nameof(diseaseFromDb));
			}

			diseaseFromDb.Name = diseaseServiceModel.Name;
			diseaseFromDb.Description = diseaseServiceModel.Description;
			diseaseFromDb.CDGDiseaseType = diseaseTypeFromDb;

			this.context.CDGDiseases.Update(diseaseFromDb);
			int result = await this.context.SaveChangesAsync();

			return result > 0; ;
		}

		public IQueryable<CDGDiseaseServiceModel> GetAll()
		{
			var cdgDiseases = this.context.CDGDiseases.To<CDGDiseaseServiceModel>();

			return cdgDiseases;
		}


		public async  Task<CDGDiseaseServiceModel> GetCDGDiseaseById(int id)
		{
			CDGDiseaseServiceModel disease = await this.context.CDGDiseases.To<CDGDiseaseServiceModel>()
				.SingleOrDefaultAsync(d => d.Id == id);

			return disease;
		}
	}
}
