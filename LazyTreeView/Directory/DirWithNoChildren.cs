namespace LazyTreeView.Directory
{
  class DirWithNoChildren : Dir
  {
    public DirWithNoChildren(DirFactory dirFactory, string path)
      : base(dirFactory, path)
    {
    }

    public override void ExpandSubItems()
    {

    }

    public override void IgnoreSubItems()
    {
    }
  }
}