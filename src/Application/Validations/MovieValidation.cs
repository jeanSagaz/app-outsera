using Application.ViewModels.Requests;
using Core.Resources;
using FluentValidation;

namespace Application.Validations
{
    public abstract class MovieValidation<T> : AbstractValidator<MovieRequestViewModel>
    {
        protected void ValidateYear()
        {
            RuleFor(c => c.Year)
                .NotEmpty().WithMessage(Errors.YearNotEmpty)
                .GreaterThan(0).WithMessage(Errors.YearGreaterThan)
                .OverridePropertyName("year");
        }

        protected void ValidateTitle()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage(Errors.TitleNotEmpty)
                .OverridePropertyName("title");
        }

        protected void ValidateStudio()
        {
            RuleFor(c => c.Studio)
                .NotEmpty().WithMessage(Errors.StudioNotEmpty)
                .OverridePropertyName("studio");
        }

        protected void ValidateProducer()
        {
            RuleFor(c => c.Producer)
                .NotEmpty().WithMessage(Errors.ProducerNotEmpty)
                .OverridePropertyName("producer");
        }
    }
}
