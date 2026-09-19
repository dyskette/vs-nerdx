using VsNerdX.Core;
using static VsNerdX.VsNerdXPackage;

namespace VsNerdX.Util
{
    /// <summary>
    ///     Resolves the full path of whatever the hierarchy window has selected.
    /// </summary>
    public static class SelectionPath
    {
        /// <summary>
        ///     Resolves the selected node to a path on disk.
        /// </summary>
        /// <param name="hierarchyControl">Hierarchy window holding the selection.</param>
        /// <returns>
        ///     Full path, or the node's visible text when the node has no file behind
        ///     it. Null when nothing is selected.
        /// </returns>
        /// <remarks>
        ///     Open Folder workspaces expose the path on the visual node, while
        ///     solution-backed trees only answer through DTE, and the two disagree
        ///     about which one is populated. Both are tried, in that order.
        /// </remarks>
        public static string Resolve(IHierarchyControl hierarchyControl)
        {
            var selectedTreeNode = hierarchyControl.GetSelectedItem();
            if (selectedTreeNode == null)
            {
                return null;
            }

            var wsVisualNode = selectedTreeNode.GetValue("Item").GetValue("WorkspaceVisualNode");
            if (wsVisualNode != null)
            {
                return (string)wsVisualNode.GetValue("FullPath");
            }

            var selectedItem = Dte.SelectedItems.Item(1);
            var selectedProject = selectedItem.Project;
            if (selectedProject != null)
            {
                return selectedProject.FullName != "" ? selectedProject.FullName : selectedProject.Name;
            }

            if (selectedItem.ProjectItem != null)
            {
                return selectedItem.ProjectItem.FileNames[1];
            }

            if (selectedItem.Name == (string)Dte.Solution.Properties.Item("Name").Value)
            {
                return Dte.Solution.FullName;
            }

            return TreeHelper.GetText(selectedTreeNode);
        }
    }
}
