using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WeeWorld.ADS.Data.Enums;
using WeeWorld.ADS.Data.Models;
using WeeWorld.ADS.Data.Repositories.Abstract;
using WeeWorld.ADS.Models.Validation;
using WeeWorld.ADS.Services.Abstract;

namespace WeeWorld.ADS.Services.Concrete
{
    public class FileService : IFileService
    {
        private readonly IApplicationService applicationService;
        private readonly IBuildService BuildService;
        private readonly IList<string> applicationDirectoires;
        private readonly IGroupRelationshipService groupRelationshipService;
        private readonly IGroupService groupService;

        public FileService(IApplicationService applicationService, IBuildService buildService, IGroupService groupService, IGroupRelationshipService groupRelationshipService)
	    {
            this.applicationService = applicationService;
            this.BuildService = buildService;
            this.groupRelationshipService = groupRelationshipService;
            this.groupService = groupService;


            //var rootDir = HostingEnvironment.MapPath("http://jenkins.weeworld.local/");
            //var rootDir = HostingEnvironment.MapPath("\\builds\\");
            var rootDir = "";
            applicationDirectoires = Directory.GetDirectories(rootDir).Select(b => b.ToLower()).ToList();
	    }

        public void LookForApplications()
        {
            // interrogate the file server for new applications
            foreach (var directory in applicationDirectoires)
            {
                LookForApplication(directory);
            }
        }

        public void LookForApplication(string directory)
        {
            string appName = directory.Split('\\').Last();
            var app = applicationService.GetByName(appName);
            if (app == null)
            {
                app = new Application { Name = appName };
                applicationService.Create(app);

                var group = groupService.GetByName("administrators");
                groupRelationshipService.SaveApplicationGroups(app.Id, new List<int> { group.Id });
            }
        }

        /// <summary>look for any new builds on the CI server
        /// By default, it will start with the newest (ordered by build number) and work its way back.
        /// If fullScan is set to false (the default), it will stop processing builds and assume that all
        /// previous ones have been uploaded. If fullScan is true, it will continue to process builds until 
        /// all have been checked.
        /// </summary>
        public void LookForBuilds(bool fullScan = false)
        { 
            // interrogate the file server for new builds
            foreach (var appDirectory in applicationDirectoires)
            {
                // first ensure that the app exists in the db
                LookForApplication(appDirectory);

                // grab the application object
                string appName = appDirectory.Split('\\').Last();
                var application = applicationService.GetByName(appName);

                var builds = Directory.GetDirectories(appDirectory);

                // Iterate over each directory inside this application folder
                // order by name (ie build number)
                foreach (var buildDir in builds.OrderByDescending(b=> b)) // limit this by... date? versionNo?
                {
                    // grab the plist file from the ci package
                    var filepath = string.Concat(buildDir, "\\info.plist");
                    FileInfo fileInfo = new FileInfo(filepath);

                    if (fileInfo.Exists)
                    {
                        var plist = new PList(filepath);
                        var buildNumber = buildDir.Split('\\').Last();

                        var build = BuildService.GetByVersionNumber(application.Id, buildNumber);

                        if (build == null)
                        {
                            build = new Build
                            {
                                Application = application,
                                /* from package xml */
                                BuildNumber = buildNumber,
                                VersionNumber = plist["CFBundleShortVersionString"],
                                /* from ci output */
                                DateCreated = fileInfo.CreationTime,
                                Type = BuildType.Production,
                                ReleaseNotes = "pulled from svn",
                                PackageUrl = "http://www.url.com"
                            };

                            BuildService.Create(build);
                        }
                        else if (fullScan == false)
                        {
                            // stop processing builds for this app once the last uploaded one has been found
                            break;
                        }
                    }
                }
            }
        }
    }
}