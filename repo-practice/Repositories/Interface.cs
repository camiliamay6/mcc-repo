using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace repo_practice.Repositories
{
    interface IModel<T> where T : class
    {
        public void Create(T model);
        public void Update(T model);
        public void Delete(int id, T model);
        public List<string> GetById(T model, int id);
        public Dictionary<int, List<string>> GetAll(T model);
    }
}
