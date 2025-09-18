using Lab3.Entity;
using Lab3.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Services
{
    public class ProductService
    {

        ProductRepo _repo = new();

        public async Task AddAsync(product pr)
        {
            if (pr.Quantity > 0) {
                pr.CreatedAt = DateTime.Now;
            await _repo.AddAsync(pr);
            }
        }

        public async Task<List<product>> GetAllAsync()
        {
            List<product> ls = await _repo.GetAllAsync();
           for(int i =0; i < ls.Count; i++)
            {
                if (ls[i].Quantity == 0)
                    ls.RemoveAt(i);
            }
            return ls;
        }

    }
}
