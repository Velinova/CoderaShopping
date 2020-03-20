using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CoderaShopping.App_Start
{
    public static class Helpers
    {
        public static void InvalidModelState(ModelStateDictionary modelState)
        {
            var errors = modelState.Where(x => x.Value.Errors.Any())
                    .Select(x => x.Value.Errors.FirstOrDefault()?.ErrorMessage);

            throw new Exception(JsonConvert.SerializeObject(errors));
        }
    }
}