using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCore.Data;
using MvcCore.Helpers;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region PROCEDIMIENTO insertarusuario
//create procedure insertarusuario
//(@username nvarchar(30), @pass varbinary(max)
//, @nombre nvarchar(30), @salt nvarchar(50))
//as
//    declare @maxid int
//    select @maxid = max(idusuario) +1 from USERHASH
//    insert into USERHASH values(@maxid, @nombre, @username, @pass, @salt)
//go
#endregion

namespace MvcCore.Repositories
{
    public class RepositorySQL : IRepository
    {
        private HospitalContext context;

        public RepositorySQL(HospitalContext context)
        {
            this.context = context;
        }

        public Usuario Login(String username, String password)
        {
            Usuario user = this.context.Usuarios.Where(z => z.UserName == username)
                .FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            else
            {
                String salt = user.Salt;
                byte[] passbbdd = user.Password;
                byte[] passform = CypherService.CifrarContenido(password, salt);
                bool resp = HelperToolKit.CompararArrayBytes(passbbdd, passform);
                if (resp == true)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public int GetMaxIdUsuario()
        {
            int id = (from datos in this.context.Usuarios
                      select datos.IdUsuario).Count();
            if (id == 0)
            {
                return 1;
            }
            return (from datos in this.context.Usuarios
                    select datos.IdUsuario).Max() + 1;
        }

        public void Insert(String nombre, String username, String password)
        {
            Usuario user = new Usuario();
            user.IdUsuario = this.GetMaxIdUsuario();
            user.Nombre = nombre;
            user.UserName = username;
            String salt = CypherService.GetSalt();
            user.Salt = salt;
            byte[] respuesta = CypherService.CifrarContenido(password, salt);
            user.Password = respuesta;
            this.context.Usuarios.Add(user);
            this.context.SaveChanges();
            //CON PROCEDIMIENTO
            //String sql = "insertarusuario @username, @pass, @nombre, @salt";
            //SqlParameter parusername = new SqlParameter("@username", username);
            //SqlParameter parnombre = new SqlParameter("@nombre", nombre);
            //String salt = CypherService.GetSalt();
            //SqlParameter parsalt = new SqlParameter("@salt", salt);
            //byte[] passcifrada = CypherService.CifrarContenido(password, salt);
            //SqlParameter parpass = new SqlParameter("@pass", passcifrada);
            //this.context.Database.ExecuteSqlRaw(sql, parusername, parpass, parnombre, parsalt);
        }

        public List<Departamento> GetDepartamentos()
        {
            return this.context.Departamentos.ToList();
        }

        public Departamento BuscarDepartamentoId(int id)
        {
            return this.context.Departamentos.Where(z => z.Numero == id).FirstOrDefault();
        }

        //public List<String> NombresUsuario()
        //{
        //    var consulta = from datos in this.context.Usuarios
        //                   select datos.Nombre;
        //    return consulta.ToList();
        //}
    }
}
