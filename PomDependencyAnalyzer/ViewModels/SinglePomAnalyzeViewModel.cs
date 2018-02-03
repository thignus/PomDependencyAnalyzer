using Microsoft.Win32;
using PomDependencyAnalyzer.Models;
using PomDependencyAnalyzer.Models.Objects;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace PomDependencyAnalyzer.ViewModels
{
    public class SinglePomAnalyzeViewModel : ObservableObject
    {
        private SinglePomAnalyzer singlePomAnalyzerModel = new SinglePomAnalyzer();
        

        public IEnumerable<Dependency> Dependencies
        {
            get { return singlePomAnalyzerModel.DependencyCollection; }
        }

        public ICommand LoadPomFile
        {
            get { return new DelegateCommand(LoadPom); }
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
                }
                catch(Exception)
                {
                    MessageBox.Show("Could not read pom file from disk.");
                } 
            }
        }
    }
}
