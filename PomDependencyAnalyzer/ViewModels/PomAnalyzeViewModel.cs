using Microsoft.Win32;
using PomDependencyAnalyzer.Models;
using PomDependencyAnalyzer.Models.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace PomDependencyAnalyzer.ViewModels
{
    public class PomAnalyzeViewModel : ObservableObject
    {
        private SinglePomAnalyzer singlePomAnalyzerModel = new SinglePomAnalyzer();
        private bool showCompareDiffs = false;
        

        public IEnumerable<Dependency> Dependencies
        {
            get
            {
                if (showCompareDiffs)
                {
                    return singlePomAnalyzerModel.DependencyDiffCollection;
                }
                else
                {
                    return singlePomAnalyzerModel.DependencyCollection;
                }
            }
        }

        public IEnumerable<Dependency> CompareDependencies
        {
            get
            {
                if (showCompareDiffs)
                {
                    return singlePomAnalyzerModel.CompareDependencyDiffCollection;
                }
                else
                {
                    return singlePomAnalyzerModel.CompareDependencyCollection;
                }
                
            }
        }

        public string MainPom
        {
            get
            {
                if (String.IsNullOrEmpty(singlePomAnalyzerModel.MainPom))
                {
                    return "Main Pom";
                }
                else
                {
                    return singlePomAnalyzerModel.MainPom;
                }
            }
        }

        public string ComparePom
        {
            get
            {
                if(String.IsNullOrEmpty(singlePomAnalyzerModel.ComparePom))
                {
                    return "Compare Pom";
                }else
                {
                    return singlePomAnalyzerModel.ComparePom;
                }
            }
        }

        public bool ShowDependencyCompare
        {
            get { return showCompareDiffs; }
            set
            {
                showCompareDiffs = value;
                RaisePropertyChangedEvent("Dependencies");
                RaisePropertyChangedEvent("CompareDependencies");
            }
        }

        public ICommand LoadPomFile
        {
            get { return new DelegateCommand(LoadPom); }
        }

        public ICommand LoadComparePomFile
        {
            get { return new DelegateCommand(LoadComparePom); }
        }

        private void LoadPom()
        {
            OpenFileDialog pomSelectDialog = new OpenFileDialog();
            pomSelectDialog.Filter = "xml files (.xml)|*.xml";
            pomSelectDialog.RestoreDirectory = true;

            if(pomSelectDialog.ShowDialog() == true)
            {
                try
                {
                    singlePomAnalyzerModel.loadPomDependencies(pomSelectDialog.FileName);
                    singlePomAnalyzerModel.MainPom = Directory.GetParent(pomSelectDialog.FileName).Name;
                    RaisePropertyChangedEvent("Dependencies");
                    RaisePropertyChangedEvent("MainPom");
                }
                catch(Exception)
                {
                    MessageBox.Show("Could not read pom file from disk.");
                } 
            }
        }

        private void LoadComparePom()
        {
            OpenFileDialog pomSelectDialog = new OpenFileDialog();
            pomSelectDialog.Filter = "xml files (.xml)|*.xml";
            pomSelectDialog.RestoreDirectory = true;

            if (pomSelectDialog.ShowDialog() == true)
            {
                try
                {
                    singlePomAnalyzerModel.loadComparePomDependencies(pomSelectDialog.FileName);
                    singlePomAnalyzerModel.ComparePom = Directory.GetParent(pomSelectDialog.FileName).Name;
                    RaisePropertyChangedEvent("CompareDependencies");
                    RaisePropertyChangedEvent("ComparePom");
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not read pom file from disk.");
                }
            }
        }
    }
}
