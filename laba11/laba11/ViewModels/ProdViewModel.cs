using laba11.Commands;
using laba11.Models;
using System.Windows.Input;

namespace laba11.ViewModels
{
    class ProdViewModel : ViewModelBase
    {
        public Prod Prod;

        public ProdViewModel(Prod prod)
        {
            this.Prod = prod;
        }

        public string Name
        {
            get { return Prod.Name; }
            set
            {
                Prod.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public int Count
        {
            get { return Prod.Count; }
            set
            {
                Prod.Count = value;
                OnPropertyChanged("Count");
            }
        }
        public int Price
        {
            get { return Prod.Price; }
            set
            {
                Prod.Price = value;
                OnPropertyChanged("Price");
            }
        }

        #region Commands

        #region Забрать

        private DelegateCommand getItemCommand;

        public ICommand GetItemCommand
        {
            get
            {
                if (getItemCommand == null)
                {
                    getItemCommand = new DelegateCommand(GetItem, CanGetItem);
                }
                return getItemCommand;
            }
        }

        private void GetItem()
        {
            Count++;
        }

        private bool CanGetItem()
        {
            return Count < 22;
        }


        #endregion

        #region Выдать

        private DelegateCommand giveItemCommand;

        public ICommand GiveItemCommand
        {
            get
            {
                if (giveItemCommand == null)
                {
                    giveItemCommand = new DelegateCommand(GiveItem, CanGiveItem);
                }
                return giveItemCommand;
            }
        }

        private void GiveItem()
        {
            Count--;
        }

        private bool CanGiveItem()
        {
            return Count > 0;
        }

        #endregion

        #endregion
    }
}
