using Microsoft.Win32;
using PomDependencyAnalyzer.Models;
using PomDependencyAnalyzer.Models.Objects;
using System;
using System.Collections.Generic;
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
            pomSelectDialog.Filter = "xml files (.xml)|*.xml|All files (*.*)|*.*";
            pomSelectDialog.RestoreDirectory = true;

            if(pomSelectDialog.ShowDialog() == true)
            {
                try
                {
                    singlePomAnalyzerModel.loadPomDependencies(pomSelectDialog.FileName);
                    RaisePropertyChangedEvent("Dependencies");
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
            pomSelectDialog.Filter = "xml files (.xml)|*.xml|All files (*.*)|*.*";
            pomSelectDialog.RestoreDirectory = true;

            if (pomSelectDialog.ShowDialog() == true)
            {
                try
                {
                    singlePomAnalyzerModel.loadComparePomDependencies(pomSelectDialog.FileName);
                    RaisePropertyChangedEvent("CompareDependencies");
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not read pom file from disk.");
                }
            }
        }
    }
}
