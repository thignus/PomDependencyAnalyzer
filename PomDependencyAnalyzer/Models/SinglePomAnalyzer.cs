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
            Dependency testDependency = new Dependency("testGroup", "testArtifact", "1.3.54");
            dependencyCollection.Add(testDependency);

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

                if (doc.DocumentElement != null)
                {
                    //XmlElement root = doc.DocumentElement;
                    XmlNamespaceManager manager = new XmlNamespaceManager(doc.NameTable);
                    manager.AddNamespace("mvn", "http://maven.apache.org/POM/4.0.0");
                    XmlNodeList dependencies = doc.SelectNodes("//mvn:dependency | //mvn:plugin", manager);
                    Console.WriteLine("Dependencies: " + dependencies.Count);
                    foreach(XmlNode dependencyNode in dependencies)
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
                        dependencyCollection.Add(new Dependency(groupId, artifactId, version));
                    }

                    //foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    //{
                    //    if (node.Name.Equals(DEPENDENCY_MANAGEMENT_NODE_NAME))
                    //    {
                    //        foreach (XmlNode dependencyListNode in node.ChildNodes)
                    //        {
                    //            if (dependencyListNode.Name.Equals(DEPENDENCY_LIST_NODE_NAME))
                    //            {
                    //                foreach (XmlNode dependencyNode in dependencyListNode.ChildNodes)
                    //                {
                    //                    if (dependencyNode.Name.Equals(DEPENDENCY_NODE_NAME))
                    //                    {
                    //                        string groupId, artifactId, version;
                    //                        groupId = artifactId = version = "";
                    //                        foreach (XmlNode dependency in dependencyNode.ChildNodes)
                    //                        {

                    //                            switch(dependency.Name)
                    //                            {
                    //                                case DEPENDENCY_GROUP_ID_NODE_NAME:
                    //                                    groupId = dependency.InnerText;
                    //                                    break;
                    //                                case DEPENDENCY_ARTIFACT_ID_NODE_NAME:
                    //                                    artifactId = dependency.InnerText;
                    //                                    break;
                    //                                case DEPENDENCY_VERSION_NODE_NAME:
                    //                                    version = dependency.InnerText;
                    //                                    break;
                    //                            }
                    //                        }
                    //                        dependencyCollection.Add(new Dependency(groupId, artifactId, version));
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
        }
    }
}
