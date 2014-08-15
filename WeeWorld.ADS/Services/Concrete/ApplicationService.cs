using System;
using System.Collections.Generic;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Data.Repositories.Abstract;
using WeeWorld.ADS.Models.Validation;
using WeeWorld.ADS.Services.Abstract;

namespace WeeWorld.ADS.Services.Concrete
{
    public class ApplicationService : BaseService<Application>, IApplicationService
    {
        public ApplicationService(IRepository<Application> applicationRepo) : base(applicationRepo)
	    {

	    }

        /// <summary>Get single application using it's unique name. returns null if it doesnt exist</summary>
        public Application GetByName(string name)
        {
            return repo.SingleOrDefault(a => a.Name.ToLower() == name.ToLower());
        }

        public override void Validate(Application application)
        {
            // if app is null, cant carry out any other validation
            if (application == null)
                throw new ValidationException("Object", "Application cannot be null");

            var errors = new ValidationErrorList();

            /// check that name is provided and it doesnt alreadt exist
            if (string.IsNullOrEmpty(application.Name))
            {
                errors.Add("Name", "Name is required");
            }
            else
            {
                var existingName = GetByName(application.Name);
                if (existingName != null && (existingName.Id != application.Id))
                {
                    errors.Add("Name", "Name already exists");
                }
            }

            if (string.IsNullOrEmpty(application.Platform))
            {
                errors.Add("Platform", "Platform is required");
            }

            if (errors.Count > 0)
                throw new ValidationException(errors);

        }

    }
}