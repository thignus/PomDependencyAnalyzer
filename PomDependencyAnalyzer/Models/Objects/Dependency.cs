using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomDependencyAnalyzer.Models.Objects
{
    public class Dependency
    {
        private string dependencyGroupId;
        private string dependencyArtifactId;
        private string dependencyVersion;

        public string DependencyName
        {
            get
            {
                return dependencyGroupId;
            }

            set
            {
                dependencyGroupId = value;
            }
        }

        public string DependencyArtifactId
        {
            get
            {
                return dependencyArtifactId;
            }

            set
            {
                dependencyArtifactId = value;
            }
        }

        public string DependencyVersion
        {
            get
            {
                return dependencyVersion;
            }

            set
            {
                dependencyVersion = value;
            }
        }

        

        public Dependency(string dependencyGroupId, string dependencyArtifactId, string dependencyVersion)
        {
            this.dependencyGroupId = dependencyGroupId;
            this.dependencyArtifactId = dependencyArtifactId;
            this.dependencyVersion = dependencyVersion;
        }
    }
}
