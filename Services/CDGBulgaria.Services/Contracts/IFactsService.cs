﻿using CDGBulgaria.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDGBulgaria.Services.Contracts
{
	public interface IFactsService
	{

		IQueryable<FactServiceModel> GetAllFacts();

		Task<bool> Create(FactServiceModel factServiceModel);
	}
}
