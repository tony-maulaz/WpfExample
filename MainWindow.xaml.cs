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
using System.Collections.ObjectModel;

namespace WpfExample
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel vm = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.SetControl(1);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            vm.SetControl(2);
        }
    }

    public class ViewModel : NotifyBaseClass
    {
        public ObservableCollection<Person> Persons { get; private set; }

        public Test test { get; set; }

        public ViewModel()
        {
            test = new Test();
            test.Value = 0;
            Persons = new ObservableCollection<Person>();

            Persons.Add(new Person(12, "Bob"));
            Persons.Add(new Person(1, "Alice"));
            Persons.Add(new Person(5, "Jean"));

            selectedPerson = Persons[1];
        }

        public void SetControl(int id)
        {
            if(id == 1)
            {
                CurrentControl = new UserControl1();
            }
            else
            {
                CurrentControl = new UserControl2();
            }
            CurrentControl.DataContext = SelectedPerson;
        }

        private ICommand clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                //return clickCommand ?? (clickCommand = new CommandHandler(() => MyAction(), () => CanExecute()));
                // lambda
                return clickCommand ?? (clickCommand = new CommandHandler(() => MyAction(), () => { return ButtonEnabled; }));
            }
        }
        /* possible de mettre une méthode
        public bool CanExecute
        {
            get
            {
                return ButtonEnabled;
            }
        }
        */

        public void MyAction()
        {
            
            test.Name = "Test 1";
            test.Value++;
        }

        private Control currentControl;
        public Control CurrentControl
        {
            get { return currentControl; }
            set
            {
                if( currentControl != value)
                {
                    currentControl = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool buttonEnabled;
        public bool ButtonEnabled
        {
            get { return buttonEnabled; }
            set
            {
                if (value != buttonEnabled)
                {
                    buttonEnabled = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Person selectedPerson;
        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                if(selectedPerson != value)
                {
                    selectedPerson = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }

    public class Test : NotifyBaseClass
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if( value != this.name)
                {
                    this.name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double value;
        public double Value
        {
            get { return value; }
            set
            {
                this.value = value;
                NotifyPropertyChanged();
            }
        }
    }

    public class Person : NotifyBaseClass
    {
        public Person(int id, string name)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; set; }
        public int Id { get; set; }
    }
}
