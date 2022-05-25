using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;

namespace repo_practice.Repositories
{
    class GeneralRepository<T> : IModel<T> where T : class
    {
        public SqlConnection conn;
        //public T model;
        private string connection_string = "Data Source=DESKTOP-EAS3LU7;User ID=meila;Password=06051999;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void MakeNewConnection()
        {
            conn = new SqlConnection(connection_string);
            try
            {
                conn.Open();
                Console.WriteLine("Connection Established");
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void CloseConnection()
        {
            conn.Close();
        }
        public List<string> GetModelProperties(T model)
        {
            List<string> listField = new List<string>();
            Type t = typeof(T);
            foreach (var field in t.GetFields())
            {
                listField.Add(field.Name);
            }

            return listField;
        }

        public List<string> GetModelValue(T model)
        {
            List<string> listField = GetModelProperties(model);
            List<string> listValue = new List<string>();
            foreach (var item in listField)
            {
                var fieldValue = model.GetType().GetField(item).GetValue(model).ToString();
                listValue.Add(fieldValue);
            }

            return listValue;
        }
        public void Create(T model)
        {
            
            List<string> listValue = GetModelValue(model);
            string combined_list = string.Join(",", listValue);

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"INSERT INTO {model.GetType().Name} VALUES ");
            strBuilder.Append($"({combined_list})");

            string insert_query = strBuilder.ToString();
            SqlCommand command = new SqlCommand(insert_query, conn);

            command.ExecuteNonQuery();

            Console.WriteLine("Data successfully inserted!");
        }

        public void Update(T model)
        {
            List<string> listField = GetModelProperties(model);
            List<string> listValue = GetModelValue(model);
            List<string> fieldValue = new List<string>();

            for(int i = 0; i < listField.Count; i++)
            {
                fieldValue.Add($"{listField[i]} = {listValue[i]}");
            }

            string combined_list = string.Join(",", fieldValue);

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"UPDATE {model.GetType().Name} SET ");
            strBuilder.Append($"{combined_list}");
            strBuilder.Append($"WHERE {listField[0]} = {listValue[0]}");
            
            string update_query = strBuilder.ToString();

            SqlCommand command = new SqlCommand(update_query, conn);

            command.ExecuteNonQuery();

            Console.WriteLine("Data successfully updated!");
        }

        public void Delete(int id, T model)
        {
            List<string> listField = GetModelProperties(model);

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"DELETE FROM {model.GetType().Name} ");
            strBuilder.Append($"WHERE {listField[0]} = {id}");
            
            string delete_query = strBuilder.ToString();
            
            SqlCommand command = new SqlCommand(delete_query, conn);

            command.ExecuteNonQuery();

            Console.WriteLine("Data successfully deleted!");
        }

     

        public List<string> GetById(T model, int id)
        {
            List<string> listField = GetModelProperties(model);
            List<string> result = new List<string>();

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"SELECT * FROM {model.GetType().Name} ");
            strBuilder.Append($"WHERE {listField[0]} = {id}");
            
            string retrieve_one = strBuilder.ToString();
            SqlCommand command = new SqlCommand(retrieve_one, conn);
            SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                foreach(string item in listField)
                {
                    result.Add(reader[item].ToString());
                }
            }
            return result;
        }


        public Dictionary<int, List<string>> GetAll(T model)
        {
            StringBuilder strBuilder = new StringBuilder();
            Dictionary<int, List<string>> result = new Dictionary<int, List<string>>();

            strBuilder.Append($"SELECT * FROM {model.GetType().Name}");

            string retrieve_all = strBuilder.ToString();
            SqlCommand command = new SqlCommand(retrieve_all, conn);
            SqlDataReader reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                result.Add(i, new List<string>());
                for(int j = 0; j < reader.FieldCount; j++)
                {
                    result[i].Add(reader[j].ToString());
                }
                i++;
            }

            return result;
        }
    }
}
