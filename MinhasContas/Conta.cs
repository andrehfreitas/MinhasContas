using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MinhasContas
{
    public class Conta
    {
        [PrimaryKey, AutoIncrement]
        public uint Id { get; set; }
        public String Nome { get; set; }
        public Double Valor { get; set; }
        public String DataVencimento { get; set; }
        public Boolean Paga { get; set; }
    }
}
