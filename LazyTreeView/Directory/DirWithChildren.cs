using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LazyTreeView.Directory
{
  class DirWithChildren : Dir
  {
    protected static readonly Dir DummyItem;

    static DirWithChildren()
    {
      DummyItem = new DirWithNoChildren(null, "Loading, please wait ....");
    }

    public DirWithChildren(DirFactory dirFactory, string path)
      : base(dirFactory, path)
    {
      _subItems.Add(DummyItem);
    }

    public override void IgnoreSubItems()
    {
      _subItems.Clear();
      _subItems.Add(DummyItem);
    }

    public async override void ExpandSubItems()
    {
      var dirs = await GetSubDirectories();
      FillSubItems(dirs);
    }

    private async Task<IEnumerable<Dir>> GetSubDirectories()
    {
      return await Task<IEnumerable<Dir>>.Factory.StartNew(() =>
      {
        Thread.Sleep(500);
        IEnumerable<Dir> subDirectories;
        try
        {
          subDirectories = System.IO.Directory.GetDirectories(Path).Select(DirFactory.CreateDirectory);
        }
        catch (IOException)
        {
          subDirectories = new List<Dir>(0);
        }
        return subDirectories;
      });
    }

    private void FillSubItems(IEnumerable<Dir> content)
    {
      //code here to retrieve subitems
      _subItems.Clear();
      foreach (var item in content)
        _subItems.Add(item);
    }
  }
}