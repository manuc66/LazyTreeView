using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;

namespace LazyTreeView.Directory
{
  class DirWithChildren : Dir
  {
    protected readonly BackgroundWorker _backgroundWorker;
    protected static readonly Dir DummyItem;
    static DirWithChildren()
    {
      DummyItem = new DirWithNoChildren(null, "Loading, please wait ....");
    }

    public DirWithChildren(DirFactory dirFactory, string path)
      : base(dirFactory, path)
    {
      _subItems.Add(DummyItem);
      _backgroundWorker = new BackgroundWorker { WorkerSupportsCancellation = false, WorkerReportsProgress = false };
      _backgroundWorker.DoWork += bw_DoWork;
      _backgroundWorker.RunWorkerCompleted += bw_RunWorkerCompleted;
    }

    public override void IgnoreSubItems()
    {
      _subItems.Clear();
      _subItems.Add(DummyItem);
    }
    public override void ExpandSubItems()
    {
      if (!_backgroundWorker.IsBusy)
      {
        _backgroundWorker.RunWorkerAsync();
      }
    }
    private void bw_DoWork(object sender, DoWorkEventArgs e)
    {
      // simulate hard work or database tiemout!
      Thread.Sleep(500);
      try
      {
        e.Result = System.IO.Directory.GetDirectories(Path).Select(DirFactory.CreateDirectory);
      }
      catch (IOException)
      {
        e.Result = new List<Dir>(0);
      }
    }

    void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      FillSubItems(e.Result as IEnumerable<Dir>);
    }

    private void FillSubItems(IEnumerable<Dir> content)
    {
      if (content == null)
      {
        return;
      }

      //code here to retrieve subitems
      _subItems.Clear();
      foreach (var item in content)
        _subItems.Add(item);
    }
  }
}