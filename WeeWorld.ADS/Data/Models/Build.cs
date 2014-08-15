using System;
using System.Collections.Generic;
using System.Linq;
using WeeWorld.ADS.Data.Enums;

namespace WeeWorld.ADS.Data.Models
{
    public class Build : IModel
    {
        public int Id { get; set; }
        public virtual Application Application { get; set; }
        public BuildType Type { get; set; }
        public string VersionNumber { get; set; }
        public string BuildNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public string PackageUrl { get; set; }
        public string ReleaseNotes { get; set; }
        public SubmissionState SubmissionState { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string SubmissionNotes { get; set; }
    }
}