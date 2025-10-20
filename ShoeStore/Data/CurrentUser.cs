using ShoeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Data
{
    public static class CurrentUser
    {
        public static User _CurrentUser = new User();
    }
}
