using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhasContas
{
    public class ContaDatabase
    {
        private readonly SQLiteAsyncConnection database;

        // Método de conexão com o BD e criação da tabela Conta caso não exista
        public ContaDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Conta>().Wait();
        }


        // Método para listagem de todos os registros da tabela Contas
        public Task<List<Conta>> GetContas()
        {
            return database.Table<Conta>().ToListAsync();
        }


        public Task<List<Conta>> GetContasNaoPagas()
        {
            return database.Table<Conta>().Where(i => i.Paga == false).ToListAsync();
        }


        public Task<List<Conta>> GetContasPagas()
        {
            return database.Table<Conta>().Where(i => i.Paga == true).ToListAsync();
        }


        // Método que retornar um registro da tabela Contas pelo Id
        public Task<Conta> GetContaById(uint id)
        {
            return database.Table<Conta>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }


        // Método para salvar ou atualizar um registro da tabela Conta
        public Task<int> SaveConta(Conta conta)
        {
            if(conta.Id == 0)
            {
                database.InsertAsync(conta);
            }

            return database.UpdateAsync(conta);
        }


        // Método para apagar um registro da tabela Conta
        public Task<int> DeleteConta(Conta conta)
        {
            return database.DeleteAsync(conta);
        }
    }
}
