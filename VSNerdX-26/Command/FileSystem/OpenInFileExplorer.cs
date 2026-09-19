using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using VsNerdX.Core;
using VsNerdX.Util;
using static VsNerdX.VsNerdXPackage;

namespace VsNerdX.Command.Navigation
{
    public class OpenInFileExplorer : ICommand
    {
        private readonly IHierarchyControl _hierarchyControl;

        public OpenInFileExplorer(IHierarchyControl hierarchyControl)
        {
            this._hierarchyControl = hierarchyControl;
        }

        /// <remarks>
        ///     Dte.ExecuteCommand("File.OpeninFileExplorer") returns E_FAIL here: the
        ///     shell refuses to route that command for a hierarchy selection even
        ///     though the command id exists. Resolving the path and launching the
        ///     shell directly does not depend on command routing.
        /// </remarks>
        public ExecutionResult Execute(IExecutionContext executionContext, Keys key)
        {
            try
            {
                var path = SelectionPath.Resolve(this._hierarchyControl);

                if (string.IsNullOrEmpty(path))
                {
                    Logger?.Log("OpenInFileExplorer: no path for the current selection");
                }
                else if (System.IO.Directory.Exists(path))
                {
                    Process.Start("explorer.exe", $"\"{path}\"");
                }
                else if (System.IO.File.Exists(path))
                {
                    // /select opens the containing folder with the file highlighted.
                    Process.Start("explorer.exe", $"/select,\"{path}\"");
                }
                else
                {
                    Logger?.Log($"OpenInFileExplorer: not on disk: {path}");
                }
            }
            catch (Exception ex)
            {
                Logger?.Log($"OpenInFileExplorer failed: {ex.Message}");
            }

            executionContext = executionContext.Clear().With(mode: InputMode.Normal);
            return new ExecutionResult(executionContext, CommandState.Handled);
        }
    }
}
