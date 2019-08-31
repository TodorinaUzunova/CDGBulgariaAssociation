using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CDGBulgaria.Data.Models
{
	public class CDGDiseaseType
	{

		[Key]
		public int Id { get; set; }

		[Required]
		public string Name  { get; set; }
	}
}
