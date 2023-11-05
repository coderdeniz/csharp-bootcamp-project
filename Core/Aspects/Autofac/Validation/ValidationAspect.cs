using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;

        // önemli not aspect'lerde dependency injection yoktur.
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }
            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);

            var entityType = _validatorType.BaseType.GetGenericArguments()[0];

            var methodParamsEntity = invocation.Arguments.Where(t => t.GetType() == entityType).ToList();

            foreach (var entity in methodParamsEntity)
            {
                ValidationTool.Validate(validator, entity);
            }
        }

        // test amaçlı
        protected override void OnSuccess(IInvocation invocation)
        {
            Console.WriteLine("Validation başarıyla çalıştı");
        }

    }
}
