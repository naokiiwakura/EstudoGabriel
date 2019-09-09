using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoAplication.Tools
{
    public static class CalcularIdade
    {
        public static int Age(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age -= 1;

            return age;
        }
    }
}
