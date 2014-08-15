using System;
using System.Collections.Generic;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Data.Repositories.Abstract;
using WeeWorld.ADS.Models.Validation;
using WeeWorld.ADS.Services.Abstract;

namespace WeeWorld.ADS.Services.Concrete
{
    public class GroupService : BaseService<Group>, IGroupService
    {
        public GroupService(IRepository<Group> groupRepo) : base(groupRepo)
	    {

	    }

        public Group GetByName(string name)
        {
            return repo.SingleOrDefault(u => u.Name.ToLower() == name.ToLower());
        }

        public override void Validate(Group group)
        {
            // if group is null, cant carry out any other validation
            if (group == null)
                throw new ValidationException("Object", "Group cannot be null");

            var errors = new ValidationErrorList();

            /// check that name is provided and it doesnt alreadt exist
            if (string.IsNullOrEmpty(group.Name))
            {
                errors.Add("Name", "Name is required");
            }
            else
            {
                var existingName = GetByName(group.Name);
                if (existingName != null && (existingName.Id != group.Id))
                {
                    errors.Add("Name", "Name already exists");
                }
                else if (group.Id > 0)
                {
                    // if updating a group, prevent name changes to the administrators group
                    var previousValues = GetById(group.Id);
                    if (previousValues.Name.ToLower() == "administrators" && group.Name.ToLower() != previousValues.Name.ToLower())
                    {
                        errors.Add("Name", "You cannot change the name of this group");
                    }
                }
            }

            if (errors.Count > 0)
                throw new ValidationException(errors);

        }

        public override void Delete(Group group)
        {
            if (group != null && group.Name.ToLower() == "administrators")
                throw new ValidationException("Name", "You cannot delete this group");

            base.Delete(group);
        }

    }
}