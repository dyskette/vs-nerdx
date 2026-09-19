using System.Windows.Forms;
using VsNerdX.Core;
using VsNerdX.Util;
using static VsNerdX.VsNerdXPackage;

namespace VsNerdX.Command.Navigation
{
    public class CopyPath : ICommand
    {
        private readonly IHierarchyControl _hierarchyControl;

        public CopyPath(IHierarchyControl hierarchyControl)
        {
            this._hierarchyControl = hierarchyControl;
        }

        public ExecutionResult Execute(IExecutionContext executionContext, Keys key)
        {
            // A solution folder has no path, and copying its label is still the
            // useful answer here, so the fallback lives with the caller wanting it.
            var path = SelectionPath.Resolve(this._hierarchyControl)
                       ?? TreeHelper.GetText(this._hierarchyControl.GetSelectedItem());

            if (path != null)
            {
                Clipboard.SetText(path);
            }

            executionContext = executionContext.Clear().With(mode: InputMode.Normal);
            return new ExecutionResult(executionContext, CommandState.Handled);
        }
    }
}
