using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TrafficFineSystem.Dtos.ApprovalHistoryDtos;

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

                var validatorType =typeof(IValidator<>).MakeGenericType(argument.GetType());
                var validator =_serviceProvider.GetService(validatorType);
                if (validator is null)
                    continue;

                var validationContext =new ValidationContext<object>(argument);
                var result =await ((IValidator)validator).ValidateAsync(validationContext);
                if (result.IsValid)
                    continue;
                context.ModelState.Clear();
                foreach (var error in result.Errors)
                {
                    context.ModelState.AddModelError(error.PropertyName,error.ErrorMessage);
                }

                if (argument is ApproveTrafficFineDto approveDto)
                {
                    RedirectToDetails(context,approveDto.TrafficFineId, result);
                    return;
                }

                if (argument is RejectTrafficFineDto rejectDto)
                {
                    RedirectToDetails(context,rejectDto.TrafficFineId,result);
                    return;
                }
                var actionDescriptor = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
                context.Result = new ViewResult
                {
                    ViewName = actionDescriptor?.ActionName,
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(),context.ModelState)
                    {
                        Model = argument
                    }
                };
                return;
            }
            await next();
        }

        private static void RedirectToDetails(ActionExecutingContext context,int trafficFineId,FluentValidation.Results.ValidationResult result)
        {
            var factory =context.HttpContext.RequestServices.GetRequiredService<ITempDataDictionaryFactory>();
            var tempData =factory.GetTempData(context.HttpContext);
            tempData["Error"] =string.Join(" ",result.Errors.Select( x => x.ErrorMessage));
            context.Result =new RedirectToActionResult("Details","TrafficFine",new{id = trafficFineId});
        }
    }
}