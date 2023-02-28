using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
using Technical_Test.Entities;

namespace Technical_Test.Models
{
    
    public class UsuarioModel
    {
       
        string connectionString = ConfigurationManager.AppSettings["ConecctionString"];


        public List<UsuarioObj> consultarInformacionLista()
        {
            List<UsuarioObj> listaUsuarios = new List<UsuarioObj>();
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);

                var query = @"SELECT * FROM usuario";

                listaUsuarios = databaseConnection.Query<UsuarioObj>(query, new { }).ToList();

                foreach (var item in listaUsuarios)
                {
                    var datos = "SELECT * FROM datos WHERE pos =" + item.pos;

                    var datosUsuario = databaseConnection.Query<UsuarioObj>(datos, new { Pos = item.pos }).FirstOrDefault();

                    item.telefono = datosUsuario.telefono;
                    item.direccion = datosUsuario.direccion;
                }
                


                return listaUsuarios;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public UsuarioObj mostrarInformacion(int? pos)
        {
            if (pos == null)
            {
                pos = 1;
            }
            UsuarioObj user = new UsuarioObj();

            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);

                var query = @"SELECT * FROM usuario WHERE pos =" +pos;

                user = databaseConnection.Query<UsuarioObj>(query, new { }).FirstOrDefault();

               
                var datos = "SELECT * FROM datos WHERE pos ="+ pos;

                var datosUsuario = databaseConnection.Query<UsuarioObj>(datos, new {  Pos =pos }).FirstOrDefault();
                   
                user.telefono = datosUsuario.telefono;
                user.direccion = datosUsuario.direccion;
                

                return user;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public UsuarioObj EliminarUsuario(int pos)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);

            var query = "DELETE from usuario WHERE pos ="+pos;

            var result = databaseConnection.Query(query);

            return new UsuarioObj { };

        }

        public bool ActualizarUsuario(UsuarioObj usuario)
        {

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);

            var query = "UPDATE datos SET  direccion = '"+usuario.direccion+"', telefono = " +usuario.telefono+" WHERE pos =" +usuario.pos;

            var result = databaseConnection.Execute(query, new { usuario.direccion, usuario.telefono, usuario.pos });
            
            return result>0;
        }

        public List<SelectListItem> ConsultarUsuariosCombo()
        {
            List<SelectListItem> resultado = new List<SelectListItem>();
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);

            var query = @"SELECT * FROM usuario";
            var usuarios = databaseConnection.Query<UsuarioObj>(query, new { }).ToList();

            try
            {

                foreach (var item in usuarios)
                {
                    resultado.Add(new SelectListItem
                    {
                        Value = item.pos.ToString(),
                        Text = item.nombre.ToString(),
                    });
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                databaseConnection.Dispose();
            }
            return resultado;
        }


    }

   
}