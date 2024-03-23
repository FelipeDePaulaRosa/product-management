using MediatR;
using System.Linq;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using CrossCutting.Notifications;

namespace Domain.Utils.FluentValidations
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly INotificationContext _notificationContext;
        
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
            INotificationContext notificationContext)
        {
            _validators = validators;
            _notificationContext = notificationContext;
        }
        
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestToValidate = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(requestToValidate))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count == 0)
                return await next();
        
            var errors = failures.Select(x => x.ErrorMessage);
            _notificationContext.AddNotifications(errors);
            return default!;
        }
    }
}