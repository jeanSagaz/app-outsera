using Application.ViewModels.Requests;

namespace Application.Validations
{
    public class MovieRequestValidation : MovieValidation<MovieRequestViewModel>
    {
        public MovieRequestValidation()
        {
            ValidateYear();
            ValidateTitle();
            ValidateStudio();
            ValidateProducer();
        }
    }
}
