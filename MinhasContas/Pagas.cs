using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace MinhasContas
{
    class Pagas: ObservableCollection<Conta>
    {
        public string LongName { get; set; }
    }
}
