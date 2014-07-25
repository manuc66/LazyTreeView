using System;
using System.Linq;
using System.Windows;
using LazyTreeView.Directory;

namespace LazyTreeView
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    public MainWindow()
    {
      InitializeComponent();
      var directoryFactory = new DirFactory();
      fileSystemTreeView.ItemsSource = Environment.GetLogicalDrives().Select(directoryFactory.CreateDirectory);


    }


  }
}
