using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using WeeWorld.ADS.Data.Enums;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Data.Repositories.Abstract;
using WeeWorld.ADS.Models.Validation;
using WeeWorld.ADS.Services.Abstract;

namespace WeeWorld.ADS.Services.Concrete
{
    public class BuildService : BaseService<Build>, IBuildService
    {
        private readonly IApplicationService applicationService;

        public BuildService(IRepository<Build> repo, IApplicationService applicationService) : base(repo)
	    {
            this.applicationService = applicationService;
	    }
        
        public IEnumerable<Build> GetByApp(int appId)
        {
            var app = applicationService.GetById(appId);

            return app.Builds.OrderByDescending(b => b.DateCreated);
        }

        /// <summary>Grab a single build identided by its builder number (and app name)</summary>
        public Build GetByVersionNumber(int appId, string buildNumber)
        {
            return repo.SingleOrDefault(b => b.Application.Id == appId
                                          && b.BuildNumber.ToLower() == buildNumber.ToLower());
        }

        public override Build Update(Build build)
        {
            Validate(build);

            // grab the existing build to compare values
            var existingBuild = GetById(build.Id);

            // if submission state has beeen set to submitted then set submission date aswell
            if (existingBuild.SubmissionState == SubmissionState.NotSubmitted && build.SubmissionState == SubmissionState.Submitted)
            {
                build.SubmissionDate = DateTime.Now;
            }

            return repo.Update(build);
        }

        public override void Validate(Build build)
        {
            // if build is null, cant carry out any other validation
            if (build == null)
                throw new ValidationException("Object", "Build cannot be null");
        }

        public IDictionary<int, IList<int>> GetBuildStructure(int appId)
        {
            var builds = GetByApp(appId);
            var productionBuilds = new Dictionary<int, IList<int>>();
            
            foreach (var build in builds)
            { 
                if (build.Type == BuildType.Production)
                {
                    productionBuilds.Add(build.Id, new List<int>());
                }
                else if (productionBuilds.Keys.Count > 0)
                {
                    // not a production build, so bump into the nested list of builds for the last production build in the dictionary
                    var key = productionBuilds.Keys.Last();
                    productionBuilds[key] = GetNestedBuilds(appId, build.Id);
                }
            }

            return productionBuilds;
        }

        private IList<int> GetNestedBuilds(int appId, int buildId)
        {
            // get all builds (created after this build)
            var builds = GetByApp(appId);

            var nestedBuilds = new List<int>();

            bool buildFound = false;
            foreach (var build in builds)
            {
                if (build.Type == BuildType.Production)
                {
                    if (buildFound)
                    {
                        break;
                    }
                    else
                    {
                        buildFound = true;
                    }
                }
                else 
                {
                    // this ad-hoc build is part of the production builds
                    nestedBuilds.Add(build.Id);
                }
            }
            return nestedBuilds;
        }
    
    }
}