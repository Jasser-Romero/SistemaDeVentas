using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SistemaDeVentas.Api.Contratos
{
    public interface IController<T>
    {
        [HttpGet]
        List<T> GetAll();
        [HttpPut]
        void Update(T t);
        [HttpDelete]
        void Delete(T t);
        [HttpPost]
        void Create(T t);
    }
}
