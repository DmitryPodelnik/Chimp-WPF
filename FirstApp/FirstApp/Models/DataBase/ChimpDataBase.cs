using FirstApp.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Models.DataBase
{
    public class ChimpDataBase
    {
        private ChimpDbContext _context;
        public ChimpDbContext Context
        {
            get => _context;
            set
            {
                _context = value;
            }
        }
    }
}
