using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TrafficFineSystem.Filters
{
    public class FluentValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public FluentValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is null)
                    continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                var validator = _serviceProvider.GetService(validatorType);

                if (validator is null)
                    continue;
                context.ModelState.Clear();

                var validationContext = new ValidationContext<object>(argument);

                var result =await ((IValidator)validator).ValidateAsync(validationContext);

                if (result.IsValid)
                    continue;

                foreach (var error in result.Errors)
                {
                    context.ModelState.AddModelError(error.PropertyName,error.ErrorMessage);
                }

                var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

                context.Result = new ViewResult
                {
                    ViewName = actionDescriptor?.ActionName,
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(),
                        context.ModelState)
                    {
                        Model = argument
                    }
                };

                return;
            }
            await next();
        }
    }
}