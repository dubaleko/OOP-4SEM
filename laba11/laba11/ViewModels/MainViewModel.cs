using laba11.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba11.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public ObservableCollection<ProdViewModel> ProdsList { get; set; }

        #region Constructor

        public MainViewModel(List<Prod> prods)
        {
            ProdsList = new ObservableCollection<ProdViewModel>(prods.Select(b => new ProdViewModel(b)));
        }

        #endregion
    }
}
