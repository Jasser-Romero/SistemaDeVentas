using Newtonsoft.Json;
using SistemaDeVentas.Domain.Entities;
using SistemaDeVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVentas.Infrastructure.Repositories
{
    public class ApiClienteRepository : IClienteRepository
    {
        string url = "https://localhost:44323/api/Cliente";
        public int Create(Cliente t)
        {
            using(WebClient client = new WebClient())
            {
                string json = JsonConvert.SerializeObject(t);

                client.UploadString(url, json);
            }
            return 0;
        }

        public bool Delete(Cliente t)
        {
            throw new NotImplementedException();
        }

        public Cliente FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Cliente FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Cliente FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Cliente> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Update(Cliente t)
        {
            throw new NotImplementedException();
        }
    }
}
