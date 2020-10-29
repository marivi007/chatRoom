using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnisantaChat.Models;
using System.Data.OleDb;

namespace UnisantaChat.Controllers
{
    public class CadastroController : Controller
    {

        //cria a conexão com o banco de dados
        OleDbConnection con = new OleDbConnection();

        //cria o objeto command and armazena a consulta SQL
        OleDbCommand com = new OleDbCommand();

        OleDbDataReader dados;



        // GET: Cadastro
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        void connectionString() 
        {
            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|/UnisantaChat.mdb";
        }


        [HttpPost]
        public ActionResult Login(Cadastro conta) 
        {
            connectionString();
            con.Open();
            com.Connection = con;

            if (conta.Tipo == 1)
            {
                com.CommandText = $"select * from Users where Email='{conta.Email}' and Senha='{conta.Senha}'";
                dados = com.ExecuteReader();
                if (dados.Read())
                {
                    
                    ViewBag.Nome = dados.GetString(1);
                    ViewBag.Email = dados.GetString(3);
                    con.Close();
                    return View("../ChatRoom/Chat");
                }
                else{
                    ViewBag.Message = "Dados Invalidos";
                    con.Close();
                    return View();

                }
                
                
            }
            else {
                com.CommandText = $"select * from Users where Nome='{conta.Nome}'";
                dados = com.ExecuteReader();
                if (dados.Read())
                {
                    ViewBag.Message = "Apelido já esta em uso";
                    con.Close();
                    return View();
                }
                con.Close();
                con.Open();
                com.CommandText = $"select * from Users where Email='{conta.Email}'";
                dados = com.ExecuteReader();
                if (dados.Read()) 
                {
                    ViewBag.Message = "Email já esta em uso";
                    con.Close();
                    return View();
                }
                
                con.Close();
                con.Open();
                com.CommandText = $"insert into Users(Nome,Senha,Email) values ('{conta.Nome}','{conta.Senha}','{conta.Email}')";
                com.ExecuteReader();
                ViewBag.Nome = conta.Nome;
                ViewBag.Email = conta.Email;
                con.Close();
                return View("../ChatRoom/Chat");
                
                
                    
                
                    
            }

            
        
        }

        
    }
}