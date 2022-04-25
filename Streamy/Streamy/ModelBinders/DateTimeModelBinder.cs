using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Streamy.ModelBinders
{
    public class DateTimeModelBinder : IModelBinder
    {
        private readonly string _customDateFormat;

        public DateTimeModelBinder(string customDateFormat)
        {
            _customDateFormat = customDateFormat;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult result = bindingContext
                 .ValueProvider
                 .GetValue(bindingContext.ModelName);

            if (result != ValueProviderResult.None && !string.IsNullOrEmpty(result.FirstValue))
            {
                DateTime parsedValue = DateTime.MinValue;
                bool success = false;
                string dateValue = result.FirstValue;

                try
                {
                    parsedValue = DateTime.ParseExact(dateValue, _customDateFormat, CultureInfo.InvariantCulture);
                    success = true;
                }

                catch (FormatException)
                {
                    try
                    {
                        parsedValue = DateTime.Parse(dateValue, new CultureInfo("bg-bg"));
                    }
                    catch (Exception e)
                    {

                        bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                    }
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                }

                if (success)
                {
                    bindingContext.Result = ModelBindingResult.Success(parsedValue);
                }
            }

            return Task.CompletedTask;
        }
    }
}
