using PomDependencyAnalyzer.Models.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PomDependencyAnalyzer.Models
{
    public class SinglePomAnalyzer
    {
        private const string DEPENDENCY_MANAGEMENT_NODE_NAME = "dependencyManagement";
        private const string DEPENDENCY_LIST_NODE_NAME = "dependencies";
        private const string DEPENDENCY_NODE_NAME = "dependency";

        private const string DEPENDENCY_GROUP_ID_NODE_NAME = "groupId";
        private const string DEPENDENCY_ARTIFACT_ID_NODE_NAME = "artifactId";
        private const string DEPENDENCY_VERSION_NODE_NAME = "version";



        private ObservableCollection<Dependency> dependencyCollection = new ObservableCollection<Dependency>();
        private ObservableCollection<Dependency> comparePomDependencyCollection = new ObservableCollection<Dependency>();
        private ObservableCollection<Dependency> dependencyDiffCollection = new ObservableCollection<Dependency>();
        private ObservableCollection<Dependency> compareDependencyDiffCollection = new ObservableCollection<Dependency>();

        public ObservableCollection<Dependency> DependencyCollection
        {
            get
            {
                return dependencyCollection;
            }
        }

        public ObservableCollection<Dependency> CompareDependencyCollection
        {
            get
            {
                return comparePomDependencyCollection;
            }
        }

        public ObservableCollection<Dependency> DependencyDiffCollection
        {
            get
            {
                return dependencyDiffCollection;
            }

            set
            {
                dependencyDiffCollection = value;
            }
        }

        public ObservableCollection<Dependency> CompareDependencyDiffCollection
        {
            get
            {
                return compareDependencyDiffCollection;
            }

            set
            {
                compareDependencyDiffCollection = value;
            }
        }

        public void loadPomDependencies(string filePath)
        {
            Console.WriteLine("Loading pom file");
            //Dependency testDependency = new Dependency("testGroup", "testArtifact", "1.3.54");
            //dependencyCollection.Add(testDependency);

            if (File.Exists(filePath))
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(filePath);
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Config file not found" + ex.StackTrace);
                    return;
                    // write default config file here
                }
                catch (XmlException ex)
                {
                    Console.WriteLine("Malformed XML" + ex.StackTrace);
                    return;
                    // delete and re-write default xml file?
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General exception: " + ex.StackTrace);
                    return;
                }

                dependencyCollection = parseForDependencies(doc);
                compareDependencyLists();
            }
        }

        public void loadComparePomDependencies(string filePath)
        {
            Console.WriteLine("Loading compare pom file");
            //Dependency testDependency = new Dependency("testGroup", "testArtifact", "1.3.54");
            //dependencyCollection.Add(testDependency);

            if (File.Exists(filePath))
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(filePath);
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Pom file not found" + ex.StackTrace);
                    return;
                }
                catch (XmlException ex)
                {
                    Console.WriteLine("Malformed XML" + ex.StackTrace);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General exception: " + ex.StackTrace);
                    return;
                }

                comparePomDependencyCollection = parseForDependencies(doc);
                compareDependencyLists();
            }
        }

        public ObservableCollection<Dependency> parseForDependencies(XmlDocument doc)
        {
            ObservableCollection<Dependency> dependencyList = new ObservableCollection<Dependency>();

            if (doc.DocumentElement != null)
            {
                //XmlElement root = doc.DocumentElement;
                XmlNamespaceManager manager = new XmlNamespaceManager(doc.NameTable);
                manager.AddNamespace("mvn", "http://maven.apache.org/POM/4.0.0");
                XmlNodeList dependencies = doc.SelectNodes("//mvn:dependency | //mvn:plugin", manager);
                Console.WriteLine("Dependencies: " + dependencies.Count);
                foreach (XmlNode dependencyNode in dependencies)
                {
                    //Console.WriteLine("Node: " + dependencyNode.InnerText);
                    string groupId, artifactId, version;
                    groupId = artifactId = version = "";
                    foreach (XmlNode dependency in dependencyNode.ChildNodes)
                    {
                        switch (dependency.Name)
                        {
                            case DEPENDENCY_GROUP_ID_NODE_NAME:
                                groupId = dependency.InnerText;
                                break;
                            case DEPENDENCY_ARTIFACT_ID_NODE_NAME:
                                artifactId = dependency.InnerText;
                                break;
                            case DEPENDENCY_VERSION_NODE_NAME:
                                version = dependency.InnerText;
                                break;
                        }
                    }
                    dependencyList.Add(new Dependency(groupId, artifactId, version));
                }
            }

            return dependencyList;
        }

        private void compareDependencyLists()
        {
            if (dependencyCollection.Count > 0 && comparePomDependencyCollection.Count > 0)
            {
                DependencyDiffCollection.Clear();
                CompareDependencyDiffCollection.Clear();
                foreach(Dependency d in dependencyCollection)
                {
                    foreach(Dependency c in comparePomDependencyCollection)
                    {
                        if (!String.IsNullOrWhiteSpace(d.DependencyVersion) && !String.IsNullOrWhiteSpace(c.DependencyVersion))
                        {
                            if (d.DependencyArtifactId.Equals(c.DependencyArtifactId) && d.DependencyName.Equals(c.DependencyName) && !d.DependencyVersion.Equals(c.DependencyVersion))
                            {
                                Console.WriteLine("Caught a difference");
                                DependencyDiffCollection.Add(d);
                                CompareDependencyDiffCollection.Add(c);
                            }
                        }
                    }
                }
            }
        }
    }
}
