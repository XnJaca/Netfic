﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryCategoria
    {
        IEnumerable<Categoria> GetCategorias();
        Categoria GetCategoriaById(int id);
        Categoria Save(Categoria categoria);

        Boolean Delete(int id);
    }
}
