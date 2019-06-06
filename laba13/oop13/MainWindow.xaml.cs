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

namespace oop13
{
    #region Adapter
    interface IPen
    {
        string Write();
    }

    class Pen : IPen
    {
        public string Write()
        {
            return "Pen writes";
        }
    }

    interface IChalk
    {
        string Draw();
    }

    class Chalk : IChalk
    {
        public string Draw()
        {
            return "Chalk draws";
        }
    }

    class Student
    {
        public string Stuf(IPen stuff)
        {
           return stuff.Write();
        }
    }

    
  
    class ChalkToPenAdapter : IPen
    {
        Chalk chalk;
        public ChalkToPenAdapter(Chalk c)
        {
            chalk = c;
        }

        public string Write()
        {
           return chalk.Draw();
        }
    }
    #endregion

    #region Decorator
    abstract class Cake
    {
        public Cake(string n)
        {
            this.Name = n;
        }

        public string Name { get; protected set; }
        public abstract int GetCost();
    }

    class AppleCake : Cake
    {
        public AppleCake() : base("Apple cake")
        { }
        public override int GetCost()
        {
            return 6;
        }
    }

    class ChocolateCake : Cake
    {
        public ChocolateCake()
            : base("Chocolate cake")
        { }
        public override int GetCost()
        {
            return 10;
        }
    }

    abstract class CakeDecorator : Cake
    {
        protected Cake Cake;
        public CakeDecorator(string n, Cake Cake) : base(n)
        {
            this.Cake = Cake;
        }
    }

    class DeliveryCake : CakeDecorator
    {
        public DeliveryCake(Cake p) : base(p.Name + ", with delivery", p)
        { }

        public override int GetCost()
        {
            return Cake.GetCost() + 5;
        }
    }

    class ThereCake : CakeDecorator
    {
        public ThereCake(Cake p) : base(p.Name + ", eat there", p)
        { }

        public override int GetCost()
        {
            return Cake.GetCost() - 1;
        }
    }
    #endregion

    #region Composite
    abstract class Component
    {
        protected string name;

        public Component(string name)
        {
            this.name = name;
        }

        public virtual void Add(Component component) { }

        public virtual void Remove(Component component) { }

        public virtual string Search(string itemName)
        {
            return "";
        }
        public virtual string GetName()
        {
            return "";
        }

        public virtual string Print()
        {
            return name;
        }
    }

    class Box : Component
    {
        private List<Component> components = new List<Component>();

        public Box(string name) : base(name)
        {
        }

        public override void Add(Component component)
        {
            components.Add(component);
        }

        public override void Remove(Component component)
        {
            components.Remove(component);
        }

        public override string GetName()
        {
            return name;
        }

        public override string Search(string itemName)
        {
            string result = "";

            try
            {
                for (int i = 0; i < components.Count; i++)
                {
                    foreach(Item item in (components[i] as Box).components)
                    {
                        if (item.GetName() == itemName)
                        {
                            result = "item found";
                        }
                    }                    
                    if (components[i].GetName() == itemName)
                    {
                        result = "item found";
                    }
                }           
            }
            catch
            {

            }
            if(result == "")
            {
                result = "item not found";
            }

            return result;
        }

        public override string Print()
        {
            string message = "";
            message += name.ToUpper() + "\n";
            for (int i = 0; i < components.Count; i++)
            {
               message+=  "- " +  components[i].Print() + "\n";
            }
            return message;
        }
    }

    class Item: Component
    {
        public Item(string name)  : base(name)
        { }

        public override string Print()
        {
            return name;
        }

        public override string GetName()
        {
            return name;
        }

    }
    #endregion

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Adapter_Button_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student();

            Pen pen = new Pen();
            AdapterTxtbl.Text += student.Stuf(pen) + '\n';
            AdapterTxtbl.Text += "Oops! Pen ended \n";

            Chalk chalk = new Chalk();
            IPen chalkStuff = new ChalkToPenAdapter(chalk);
            AdapterTxtbl.Text += student.Stuf(chalkStuff) + '\n';          
        }

        private void Decorator_Button_Click(object sender, RoutedEventArgs e)
        {
            Cake Cake1 = new AppleCake();
            Cake1 = new DeliveryCake(Cake1);
            DecoratorTxtbl.Text += String.Format("Название: {0}\n", Cake1.Name);
            DecoratorTxtbl.Text += String.Format("Цена: {0}\n", Cake1.GetCost());

            Cake Cake2 = new AppleCake();
            Cake2 = new ThereCake(Cake2);
            DecoratorTxtbl.Text += String.Format("Название: {0}\n", Cake2.Name);
            DecoratorTxtbl.Text += String.Format("Цена: {0}\n", Cake2.GetCost());

            Cake Cake3 = new ChocolateCake();
            Cake3 = new DeliveryCake(Cake3);
            DecoratorTxtbl.Text += String.Format("Название: {0}\n", Cake3.Name);
            DecoratorTxtbl.Text += String.Format("Цена: {0}\n", Cake3.GetCost());

            Cake Cake4 = new ChocolateCake();
            Cake4 = new ThereCake(Cake4);
            DecoratorTxtbl.Text += String.Format("Название: {0}\n", Cake4.Name);
            DecoratorTxtbl.Text += String.Format("Цена: {0}\n", Cake4.GetCost());
        }

        Component Box = new Box("big box:");

        private void Composite_Button_Click(object sender, RoutedEventArgs e)
        {  

            Component smallBox = new Box("small box");
            Component Item1 = new Item("book");
            Component Item2 = new Item("mobile");

            smallBox.Add(Item1);
            smallBox.Add(Item2);

            Box.Add(smallBox);
            CompositeTxtbl.Items.Add(Box.Print());


            smallBox.Remove(Item1);

            Component newBox = new Box("Gift box");

            Component Item3 = new Item("Candy");
            Component Item4 = new Item("Letter");
            newBox.Add(Item3);
            newBox.Add(Item4);
            Box.Add(newBox);

            CompositeTxtbl.Items.Add(Box.Print());
        }

        private void Find_Button_Click(object sender, RoutedEventArgs e)
        {         
            CompositeTxtbl.Items.Add(Box.Search("Letter"));
        }

        private void Find2_Button_Click(object sender, RoutedEventArgs e)
        {
            CompositeTxtbl.Items.Add(Box.Search("mobile"));
        }
    }
}
