using System;
using System.IO;
using System.Windows.Forms;
using VsNerdX.Core;
using VsNerdX.Util;
using static VsNerdX.VsNerdXPackage;

namespace VsNerdX.Command.Navigation
{
    /// <summary>
    ///     Copies the selection's path: full, or relative to the solution's folder (VS Code's copyRelativeFilePath).
    /// </summary>
    public class CopyPath : ICommand
    {
        private readonly IHierarchyControl _hierarchyControl;
        private readonly bool _relative;

        public CopyPath(IHierarchyControl hierarchyControl, bool relative = false)
        {
            this._hierarchyControl = hierarchyControl;
            this._relative = relative;
        }

        public ExecutionResult Execute(IExecutionContext executionContext, Keys key)
        {
            var path = SelectionPath.Resolve(this._hierarchyControl);
            if (path != null && this._relative)
            {
                path = RelativeToSolution(path);
            }

            // A solution folder has no path, and copying its label is still the
            // useful answer here, so the fallback lives with the caller wanting it.
            path = path ?? TreeHelper.GetText(this._hierarchyControl.GetSelectedItem());

            if (path != null)
            {
                Clipboard.SetText(path);
            }

            executionContext = executionContext.Clear().With(mode: InputMode.Normal);
            return new ExecutionResult(executionContext, CommandState.Handled);
        }

        /// <summary>
        ///     The path relative to the solution's folder (or the open folder), or unchanged when it's outside it.
        /// </summary>
        private static string RelativeToSolution(string path)
        {
            var solution = Dte.Solution?.FullName;
            if (string.IsNullOrEmpty(solution))
            {
                return path;
            }

            // A solution's FullName is its .sln file; an Open Folder workspace's is the folder itself.
            var root = System.IO.Directory.Exists(solution) ? solution : Path.GetDirectoryName(solution);
            root = root.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
            return path.StartsWith(root, StringComparison.OrdinalIgnoreCase) ? path.Substring(root.Length) : path;
        }
    }
}
