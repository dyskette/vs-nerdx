using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using VsNerdX.Util;

namespace VsNerdX.Core
{
    public class HelpViewControl
    {

        private readonly HierarchyControl _hierarchyControl;
        private StackPanel _helpStackPanel;
        private ScrollViewer _scrollView;
        private Grid NavigatorFrame { get; set; }

        public HelpViewControl(IHierarchyControl hierarchyControl)
        {
            this._hierarchyControl = (HierarchyControl)hierarchyControl;
        }

        public void ToggleHelp()
        {
            if (_helpStackPanel == null)
            {
                this.ShowHelp();
                NavigatorFrame = (Grid)this._hierarchyControl.SolutionHierarchy.GetValue("Frame").GetValue("FrameView").GetValue("Content");
                NavigatorFrame.SizeChanged += HierarchyControl_SizeChanged;
                HierarchyControl_SizeChanged(null, null);
                this._scrollView.Focus();
            }
            else
            {
                this.HideHelp();
                this._hierarchyControl.GetHierarchyListBox().Focus();
                var navigatiorFrame = (Grid)this._hierarchyControl.SolutionHierarchy.GetValue("Frame").GetValue("FrameView").GetValue("Content");
                navigatiorFrame.SizeChanged -= HierarchyControl_SizeChanged;
            }
        }

        private void HierarchyControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var navigationContent = (ContentPresenter)NavigatorFrame.GetValue("Content");
            _scrollView.MaxHeight = navigationContent.ActualHeight;
            _scrollView.MaxWidth = navigationContent.ActualWidth;
        }

        private void HideHelp()
        {
            _hierarchyControl.ContentGrid.Children.Remove(_helpStackPanel);
            _helpStackPanel = null;
        }


        private void ShowHelp()
        {
            this._scrollView = new ScrollViewer();
            var textBlock = new TextBlock();

            this._scrollView.FocusVisualStyle = new Style();

            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.Background = new SolidColorBrush(Color.FromRgb(201, 201, 201));
            textBlock.FontSize = 15;
            textBlock.FontFamily = new FontFamily("Consolas");

            textBlock.Inlines.AddRange(new Inline[] {
                new Bold(new Run(" VsNerdX " + VsixManifest.GetManifest().Version + " Quick Help")), new LineBreak(),
                new Bold(new Run("==========================")), new LineBreak(), new LineBreak(),
            });

            // The same groups and keys as the README and CommandProcessor.InitializeCommands.
            Section(textBlock, "Navigation",
                ("j / k", "down / up"), ("J / K", "last / first child"), ("h", "collapse the containing folder"),
                ("P", "go to parent"), ("gg / G", "top / bottom"));
            Section(textBlock, "Opening",
                ("l", "open"), ("o / O", "toggle node / open recursively"), ("go", "preview"),
                ("v / s", "open in vertical / horizontal split"), ("C", "close node recursively"),
                ("W / M", "collapse everything"));
            Section(textBlock, "Files",
                ("a / A", "new file / new folder"), ("r", "rename"), ("d", "delete"), ("m", "cut"), ("c", "copy"),
                ("p", "paste"), ("y", "copy relative path"), ("gy", "copy full path"), ("ge", "copy visible text"),
                ("H / I", "toggle show all files"), ("e", "open in File Explorer"), ("q", "close Solution Explorer"));
            Section(textBlock, "Find and help",
                ("/", "find"), ("Esc", "leave find mode, or clear a pending prefix"), ("?", "toggle help"));

            _scrollView.Content = textBlock;
            _scrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

            _helpStackPanel = new StackPanel();
            var separator = new Separator();
            _helpStackPanel.Children.Add(_scrollView);
            _helpStackPanel.Children.Add(separator);

            Grid.SetColumn(_helpStackPanel, 0);
            Grid.SetRow(_helpStackPanel, 0);
            _hierarchyControl.ContentGrid.Children.Add(_helpStackPanel);
        }

        /// <summary>
        ///     Appends a titled group of "key: action" lines.
        /// </summary>
        private static void Section(TextBlock textBlock, string title, params (string Key, string Action)[] lines)
        {
            textBlock.Inlines.Add(new Bold(new Run(" " + title)));
            textBlock.Inlines.Add(new LineBreak());
            textBlock.Inlines.Add(new Run("--------------------------"));
            textBlock.Inlines.Add(new LineBreak());
            foreach (var (key, action) in lines)
            {
                textBlock.Inlines.Add(new Bold(new Run(" " + key)));
                textBlock.Inlines.Add(new Run(": " + action));
                textBlock.Inlines.Add(new LineBreak());
            }
            textBlock.Inlines.Add(new LineBreak());
        }

        public bool IsVisible()
        {
            return this._helpStackPanel != null;
        }

        public void LineDown()
        {
            _scrollView.LineDown();
        }

        public void LineUp()
        {
            _scrollView.LineUp();
        }

        public void GoToBottom()
        {
            _scrollView.ScrollToBottom();
        }

        public void GoToTop()
        {
            _scrollView.ScrollToTop();
        }
    }
}