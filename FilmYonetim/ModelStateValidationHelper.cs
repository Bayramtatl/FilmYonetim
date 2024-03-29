﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FilmYonetim.Presentation
{
	public class ModelStateValidationHelper
	{
		public static IEnumerable<object> GetModelErrorList(ModelStateDictionary modelstate)
		{
			var errorModel =
				from x in modelstate.Keys
				where modelstate[x].Errors.Count > 0
				select new
				{
					key = x,
					errors = modelstate[x].Errors.
						Select(y => y.ErrorMessage).
						ToArray()
				};

			return errorModel;
		}
	}
}
