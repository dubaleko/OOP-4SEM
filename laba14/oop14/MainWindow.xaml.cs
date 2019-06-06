using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace oop14
{
    #region Command
    public abstract class Command
    {
        public abstract string Execute();
    }
       

    public class OpenWord : Command
    {
        public override string Execute()
        {            
            return "Word open";
        }
    }

    public class OpenOpera : Command
    {
        public override string Execute()
        {
            return "Opera open";
        }
    }

    public class Invoker
    {
        Command command;

        public void SetCommand(Command c)
        {
            command = c;
        }

        public string  Run()
        {
           return command.Execute();
        }
    }
    #endregion

    #region State
    enum ComputerState
    {
        ON,
        OFF
    }
    class Computer
    {
        public ComputerState State { get; set; }
        public int countPrograms = 0;
        public string[] openPrograms = new string[50];

        public Computer(ComputerState cs)
        {
            State = cs;
        }

        public string ComputerOn()
        {
            if (State == ComputerState.OFF)
            {
                State = ComputerState.ON;
            }
            return "Включение";
        }
        public string ComputerOff()
        {
            if (State == ComputerState.ON)
            {
                State = ComputerState.OFF;
            }
            return "Выключение";
        }

        // сохранение состояния
        public ComputerMemento SaveState()
        {            
            return new ComputerMemento( openPrograms);
        }

        // восстановление состояния
        public void RestoreState(ComputerMemento memento)
        {
            this.openPrograms = memento.openPrograms;            
        }
    }
    #endregion

    #region Memento
    // Memento
    class ComputerMemento
    {

        public string[] openPrograms = new string[50];

        public ComputerMemento( string[] prog)
        {
            this.openPrograms = prog;
        }
    }

    // Caretaker
    class CHistory
    {
        public Stack<ComputerMemento> History { get; private set; }
        public CHistory()
        {
            History = new Stack<ComputerMemento>();
        }    
    }
#endregion

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int countPrograms = 0;
        Invoker invoker = new Invoker();
        ComputerState computerState = ComputerState.OFF;
        Computer computer = new Computer(ComputerState.OFF);
       
        CHistory cHistory = new CHistory();

        private void On_Click(object sender, RoutedEventArgs e)
        {
            Screen.Items.Add(computer.ComputerOn());
            computerState = ComputerState.ON;
            Monitor.Fill = Brushes.Yellow;
            countPrograms = 0;
        }

        private void Of_Click(object sender, RoutedEventArgs e)
        {
            Screen.Items.Add(computer.ComputerOff());
            computerState = ComputerState.OFF;
            Monitor.Fill = Brushes.Black;
        }

        private void OpenOpera_Click(object sender, RoutedEventArgs e)
        {
            if(computerState == ComputerState.ON)
            {
                OpenOpera command = new OpenOpera();
                invoker.SetCommand(command);
                Screen.Items.Add(invoker.Run());
                computer.openPrograms[countPrograms++] = invoker.Run();
            }
            else
            {
                Screen.Items.Add("Включите компьютер!");
            }
        }

        private void OpenWord_Click(object sender, RoutedEventArgs e)
        {
            if (computerState == ComputerState.ON)
            {
                OpenWord command = new OpenWord();
                invoker.SetCommand(command);
                Screen.Items.Add(invoker.Run());
                computer.openPrograms[countPrograms++] = invoker.Run();
            }
            else
            {
                Screen.Items.Add("Включите компьютер!");
            }
        }

        private void SaveStation_Click(object sender, RoutedEventArgs e)
        {
            cHistory.History.Push(computer.SaveState());             
        }

        private void SuspendStation_Click(object sender, RoutedEventArgs e)
        {
            ComputerMemento computerMemento = cHistory.History.Pop();
            computer.RestoreState(computerMemento);

            Screen.Items.Add("Последнее сохранённое состояние:");
            for(int i=0;i<countPrograms; i++)
            {
                Screen.Items.Add(computerMemento.openPrograms[i]);
            }
           
        }
    }
}
