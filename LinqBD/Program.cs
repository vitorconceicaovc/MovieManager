using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqBD
{
    internal class Program2
    {
        static void Main2(string[] args)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();

            //Obter a listagem completa de filmes
            //var lista = from Filme in dc.Filmes orderby Filme.Titulo select Filme;

            //Obter a listagem completa de filmes mas descendente, e com id
            var lista = from Filme in dc.Filmes orderby Filme.Titulo descending, Filme.ID select Filme; 

            foreach ( Filme filme in lista ) 
            {
                Console.WriteLine($"ID : {filme.ID}" );
                Console.WriteLine($"Titulo : {filme.Titulo}");
                Console.WriteLine($"Categoria : {filme.Categoria}");

                Console.WriteLine();
            }

            Console.WriteLine("Existem {0} filmes.", lista.Count());

            Console.ReadKey();  
        }
    }
}
