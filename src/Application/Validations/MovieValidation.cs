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
                .NotEmpty().WithMessage(Errors.YearNotEmpty);
        }

        protected void ValidateTitle()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage(Errors.TitleNotEmpty);
        }

        protected void ValidateStudio()
        {
            RuleFor(c => c.Studio)
                .NotEmpty().WithMessage(Errors.StudioNotEmpty);
        }

        protected void ValidateProducer()
        {
            RuleFor(c => c.Producer)
                .NotEmpty().WithMessage(Errors.ProducerNotEmpty);
        }
    }
}
