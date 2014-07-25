using System.Collections.ObjectModel;

namespace LazyTreeView.Directory
{
  public abstract class Dir : ILazyCollection
  {
    protected readonly DirFactory DirFactory;

    protected readonly ObservableCollection<Dir> _subItems = new ObservableCollection<Dir>();
    public string Path { get; set; }

    protected Dir(DirFactory dirFactory, string path)
    {
      DirFactory = dirFactory;
      Path = path;
    }

    public ObservableCollection<Dir> SubItems
    {
      get { return _subItems; }
    }

    public string ShortPath
    {
      get
      {
        var ret = System.IO.Path.GetFileName(Path);
        if (string.IsNullOrEmpty(ret))
          ret = Path;
        return ret;
      }
    }

    public abstract void ExpandSubItems();

    public abstract void IgnoreSubItems();
  }
}
