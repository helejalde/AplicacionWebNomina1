using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionWebNomina1.Models;

namespace AplicacionWebNomina1.Controllers
{
    public class AccesoController : Controller
    {
        static string cadena = "Data Source = HERNANELEJALDEC; Initial Catalog=DepEmp; user ID=sa; Password=Admin123";
        // GET: Acceso

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        //Metodo Login
        [HttpPost]
        public ActionResult Login(Usuario oUsuario)
        {
            string mensaje;
            //using (SqlConnection cn= new SqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ConnectionString))
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("ValidarUser", cn);
                cmd.Parameters.AddWithValue("user", oUsuario.user);
                cmd.Parameters.AddWithValue("passw", oUsuario.passw);
                cmd.Parameters.Add("id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                //agregar un mensaje
                cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                cn.Open();
                cmd.ExecuteNonQuery();

                oUsuario.id = Convert.ToInt32(cmd.Parameters["id"].Value);
                mensaje = cmd.Parameters["mensaje"].Value.ToString();
                //oUsuario.Idusuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                cn.Close();
            }
            if (oUsuario.id == 1)
            {
                //Session["usuario"] = oUsuario.correo;
                ViewData["Mensaje"] = "Autenticación exitosa";
                return RedirectToAction("Index", "Home");
            }
            if (oUsuario.id == 0)
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }
        }
        //metodo registrar
        [HttpPost]
        public ActionResult Registrar(Usuario oUsuario)
        {
            bool registrado;
            string mensaje;
            //using (SqlConnection cn= new SqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ConnectionString))
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("Registro", cn);

                //parametros de entrada
                cmd.Parameters.AddWithValue("documento", oUsuario.documento);
                cmd.Parameters.AddWithValue("fechaNaci", oUsuario.fechaNaci);
                cmd.Parameters.AddWithValue("nombre", oUsuario.nombre);
                cmd.Parameters.AddWithValue("apellido", oUsuario.apellido);
                cmd.Parameters.AddWithValue("gender", oUsuario.gender);
                cmd.Parameters.AddWithValue("telefono", oUsuario.telefono);
                cmd.Parameters.AddWithValue("correo", oUsuario.correo);
                cmd.Parameters.AddWithValue("passw", oUsuario.passw);

                //parametros de salida
                cmd.Parameters.Add("registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["registrado"].Value);
                mensaje = cmd.Parameters["mensaje"].Value.ToString();
                
                cn.Close();

            }
            ViewData["mensaje"] = mensaje;
            
            if (registrado)
            {

                return RedirectToAction("Login", "Acceso");
            }
            
            else
            {
                return View();
            }
        }
    }
}