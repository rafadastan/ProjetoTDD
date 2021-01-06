using Projeto.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Contracts
{
    public interface IProdutoRepository
    {
        void Create(Produto produto);
        void Update(Produto produto);
        void Delete(Produto produto);

        List<Produto> GetAll();
        Produto GetById(Guid id);
    }
}
