using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEF
{
    class Program
    {
        static void Main(string[] args)
        {
            // Database accessor. This opens the database automatically
            var school = new SchoolEntities();

            // This accesses the ClassMaster table
            foreach (var classMaster in school.ClassMasters)
            {
                Console.WriteLine("ClassId: {0}\nClassName: {1}\nClassDescription: {2}\nClassPrice: {3}\n",
                    classMaster.ClassId, classMaster.ClassName, classMaster.ClassDescription, classMaster.ClassPrice);
            }

            Console.Write("Done.");
            Console.ReadLine();

            foreach (var user in school.Users)
            {
                Console.WriteLine("{0}", user.UserName);

                foreach (var classMaster in user.ClassMasters)
                {
                    Console.WriteLine("\tClassId: {0} ClassName: {1}", classMaster.ClassId, classMaster.ClassName);
                }
                Console.WriteLine();
            }

            Console.Write("Done.");
            Console.ReadLine();
        }
    }
}
