using System;
using System.Windows.Forms;
using VsNerdX.Core;
using static VsNerdX.VsNerdXPackage;

namespace VsNerdX.Command.Navigation
{
    /// <summary>
    ///     Hides Solution Explorer, as q closes the explorer in Neovim and VS Code.
    /// </summary>
    public class CloseExplorer : ICommand
    {
        public ExecutionResult Execute(IExecutionContext executionContext, Keys key)
        {
            try
            {
                // Closing a tool window hides it; Window.CloseToolWindow would close whichever tool window is active.
                Dte.Windows.Item(EnvDTE.Constants.vsWindowKindSolutionExplorer).Close();
            }
            catch (Exception ex)
            {
                Logger?.Log($"CloseExplorer failed: {ex.Message}");
            }

            return new ExecutionResult(executionContext.Clear(), CommandState.Handled);
        }
    }
}
