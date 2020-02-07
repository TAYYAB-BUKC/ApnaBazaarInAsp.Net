﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApnaBazaar.Entities
{
	public class BaseEntity
	{
		public int ID { get; set; }

		[Required]
		[MinLength(2),MaxLength(50)]
		public string Name { get; set; }

		[MaxLength(150)]
		public string Description { get; set; }
		public string Imagepath { get; set; }
	}
}
