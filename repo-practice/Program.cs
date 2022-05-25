using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace repo_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            Repositories.EmployeeRepository emp_obj = new Repositories.EmployeeRepository();
            Repositories.ItemRepository item_r = new Repositories.ItemRepository();
            
            emp_obj.MakeNewConnection();
            item_r.MakeNewConnection();

         

            Models.Employee model_e = new Models.Employee();
            Models.Item model_i = new Models.Item();

            //Insert Data
            /*Random rnd = new Random();
            model_e.id = rnd.Next();
            model_e.employee_name = "'Sinta'";
            model_e.employee_address = "'Jakarta'";

            model_i.Id = rnd.Next();
            model_i.item_name = "'Bottle'";
            model_i.price = 15000;
            model_i.quantity = 20;

            emp_obj.Create(model_e);
            item_r.Create(model_i);*/

            //Update data

            /* model_e.id = 251660114;
             model_e.employee_name = "'Meila'";
             model_e.employee_address = "'Jakarta'";

             model_i.Id = 607222773;
             model_i.item_name = "'Bottle'";
             model_i.quantity = 10;
             model_i.price = 10000;

             emp_obj.Update(model_e);
             item_r.Update(model_i);*/

            //Retrieve one data
            /*
                        List<string> result_i = item_r.GetById(model_i, 1);
                        foreach (var item in result_i)
                        {
                            Console.WriteLine(item);
                        }

                        Console.WriteLine("-------------------------");
                        List<string> result_e = emp_obj.GetById(model_e, 251660114);
                        foreach (var item in result_e)
                        {
                            Console.WriteLine(item);
                        }*/

            //Retrieve All data

            Dictionary<int, List<string>> result_i = item_r.GetAll(model_i);
            foreach (var item in result_i)
            {
                foreach (var i in item.Value)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine("--------------");
            }

            Console.WriteLine("--------------------------------------");
            Dictionary<int, List<string>> result_e = emp_obj.GetAll(model_e);
            foreach (var item in result_e)
            {
                foreach (var i in item.Value)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine("--------------");
            }

/*    Random rnd = new Random();
    model_e.id = rnd.Next();
    model_e.employee_name = "'Sinta'";
    model_e.employee_address = "'Jakarta'";

    emp_obj.Create(model_e);*/
/*
            Dictionary<int, List<string>> result = item_r.GetAll(model_i);
            foreach (var item in result)
            {
                foreach (var i in item.Value)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine("--------------");
            }*/
/* Repositories.ItemRepository item_r = new Repositories.ItemRepository();
 item_r.MakeNewConnection();
 Models.Item model_i = new Models.Item();
 List<string> result = item_r.GetById(model_i, 1);
 foreach (var item in result)
 {
     Console.WriteLine(item);
 }
*/
/*            Random rnd = new Random();
            Models.Item model_i = new Models.Item();
            model_i.Id = rnd.Next();
            model_i.item_name = "'Bottle'";
            model_i.price = 50000;
            model_i.quantity = 30;

            item_r.Create(model_i);*/

/* Models.Item model_i = new Models.Item();
 model_i.Id = 1;
 model_i.item_name = "'Book'";
 model_i.price = 5000;
 model_i.quantity = 20;
*/
/*  Models.Item model_i = new Models.Item();
  item_r.Delete(1463969350, model_i);*/
            emp_obj.CloseConnection();
            item_r.CloseConnection();
        }
    }
}
