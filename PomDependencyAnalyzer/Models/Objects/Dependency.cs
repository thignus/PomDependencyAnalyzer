using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomDependencyAnalyzer.Models.Objects
{
    public class Dependency
    {
        private string dependencyName;
        private string dependencyVersion;

        public string DependencyName
        {
            get
            {
                return dependencyName;
            }

            set
            {
                dependencyName = value;
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

        public Dependency(string dependencyName, string dependencyVersion)
        {
            this.dependencyName = dependencyName;
            this.dependencyVersion = dependencyVersion;
        }
    }
}
