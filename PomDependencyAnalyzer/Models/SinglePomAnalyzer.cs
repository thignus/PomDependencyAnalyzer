using PomDependencyAnalyzer.Models.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomDependencyAnalyzer.Models
{
    public class SinglePomAnalyzer
    {
        private readonly ObservableCollection<Dependency> dependencyCollection = new ObservableCollection<Dependency>();

        public ObservableCollection<Dependency> DependencyCollection
        {
            get
            {
                return dependencyCollection;
            }
        }

        public void loadPomDependencies(string filePath)
        {
            Console.WriteLine("Loading pom file");
            Dependency testDependency = new Dependency("testName", "1.3.54");
            dependencyCollection.Add(testDependency);

            if (File.Exists(filePath))
            {
                
            }
        }
    }
}
