using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeWorld.ADS.Services.Abstract
{
    public interface IFileService
    {
        void LookForApplications();

        /// <summary>look for any new builds on the CI server
        /// By default, it will start with the newest (ordered by build number) and work its way back.
        /// If fullScan is set to false (the default), it will stop processing builds and assume that all
        /// previous ones have been uploaded. If fullScan is true, it will continue to process builds until 
        /// all have been checked.
        /// </summary>
        void LookForBuilds(bool fullScan = false);
}
}
