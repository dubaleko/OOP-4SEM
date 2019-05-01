using System.Windows.Input;

namespace Lab7
{
    public class NewCustomCommand
    {
            private static RoutedUICommand pnvCommand;
            static NewCustomCommand()
            {
                InputGestureCollection inputs = new InputGestureCollection();
                inputs.Add(new KeyGesture(Key.P, ModifierKeys.Alt, "Alt+P"));
                pnvCommand = new RoutedUICommand("PNV", "PNV",typeof(NewCustomCommand), inputs);
            }
            public static RoutedUICommand PnvCommand
            {
                get { return pnvCommand; }
            }
    }
}
