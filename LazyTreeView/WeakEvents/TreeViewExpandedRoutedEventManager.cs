using System.Windows;
using System.Windows.Controls;

namespace LazyTreeView.WeakEvents
{
  internal class TreeViewExpandedRoutedEventManager : WeakEventManagerBase<TreeViewExpandedRoutedEventManager, TreeView>
  {
    protected override void StartListeningTo(TreeView source)
    {
      source.AddHandler(TreeViewItem.ExpandedEvent, new RoutedEventHandler(DeliverEvent));
    }

    protected override void StopListeningTo(TreeView source)
    {
      source.RemoveHandler(TreeViewItem.ExpandedEvent, new RoutedEventHandler(DeliverEvent));
    }
  }
}
