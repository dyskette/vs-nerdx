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
            var path = SelectionPath.Resolve(this._hierarchyControl);

            if (path != null)
            {
                Clipboard.SetText(path);
            }

            executionContext = executionContext.Clear().With(mode: InputMode.Normal);
            return new ExecutionResult(executionContext, CommandState.Handled);
        }
    }
}
