using System;
using System.IO;
using System.Linq;

namespace LazyTreeView.Directory
{
  public class DirFactory
  {
    public Dir CreateDirectory(string path)
    {
      bool hasChildren;
      try
      {
        hasChildren = System.IO.Directory.GetDirectories(path).Any();
      }
      catch (UnauthorizedAccessException)
      {
        hasChildren = false;
      }
      catch (IOException)
      {
        hasChildren = false;
      }

      if (hasChildren)
      {
        return new DirWithChildren(this, path);
      }
      else
      {

        return new DirWithNoChildren(this, path);
      }
    }
  }
}
