using System;
using System.Threading.Tasks;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace JackCraftLauncher.CrossPlatform.Views.MyControls
{
    public partial class MyDialog : UserControl
    {
        public enum Result
        {
            Yes,
            No,
            //Cancel
        }

        private Result? result;

        public MyDialog()
        {
            InitializeComponent();
        }

        private void ButtonClick_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "YesButton":
                    result = Result.Yes;
                    break;
                case "NoButton":
                    result = Result.No;
                    break;
/*                case "CancelButton":
                    result = Result.Cancel;
                    break;*/
            }
        }

        private void TitleBar_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (e.ClickCount <= 1)
                MainWindow.Instance.BeginMoveDrag(e);
            else if (e.ClickCount == 2)
                if (MainWindow.Instance.WindowState == WindowState.Maximized)
                    MainWindow.Instance.WindowState = WindowState.Normal;
                else if (MainWindow.Instance.WindowState == WindowState.Normal)
                    MainWindow.Instance.WindowState = WindowState.Maximized;
        }

        public static async Task<Result> ShowDialog(Grid grid, string TitleString, string ContentString)
        {
            MyDialog myDialog = new MyDialog();
            myDialog.Title.Text = TitleString;
            myDialog.Content.Text = ContentString;
            grid.Children.Add(myDialog);

            while (myDialog.result == null)
            {
                await Task.Delay(1);
            }
            grid.Children.Remove(myDialog);
            return (Result)myDialog.result;
        }
    }
}