using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfoUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DBConnect db = new DBConnect();
                db.GetGo20UserInfo();

            }
            catch (Exception ex)
            {

            }

        }
    }
}
