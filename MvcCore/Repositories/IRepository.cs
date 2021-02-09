using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public interface IRepository
    {
        Usuario Login(String username, String password);

        void Insert(String nombre, String username, String password);

        //List<String> NombresUsuario();

        int GetMaxIdUsuario();

        List<Departamento> GetDepartamentos();

        Departamento BuscarDepartamentoId(int id);
    }
}
