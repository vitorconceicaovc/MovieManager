using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqBD
{
    internal class Program3
    {
        static void Main3(string[] args)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();

            //lista de filmes só dramaticos
            //var lista = from Filme in dc.Filmes where Filme.Categoria == "FD" select Filme;

            //Filmes começados por o
            //var lista = from Filme in dc.Filmes where Filme.Titulo.StartsWith("o") select Filme;    

            //Filmes com determinado conjunto de caracteres no titulo
            var lista = from Filme in dc.Filmes where Filme.Titulo.Contains("ext") select Filme;    

            foreach (Filme filme in lista)
            {
                Console.WriteLine($"ID : {filme.ID}");
                Console.WriteLine($"Titulo : {filme.Titulo}");
                Console.WriteLine($"Categoria : {filme.Categoria}");

                Console.WriteLine();
            }

            Console.WriteLine("Existem {0} filmes.", lista.Count());

            Console.WriteLine("--------------------------------------------------------------------");

            //Agrupar informação - Contar a contidade de filmes por categoria

            var novalista = from Filme in dc.Filmes
                            group Filme by Filme.Categoria
                            into c
                            select new
                            {
                                Categoria = c.Key,
                                Contagem = c.Count()
                            };
            foreach (var c in novalista)
            {
                Console.WriteLine(c.Categoria + " (" + c.Contagem + ")");
            }

            Console.WriteLine("------------------------Junção entre tabelas------------------------");

            var outralista = from Filme in dc.Filmes
                             join Categoria in dc.Categorias
                             on Filme.Categoria equals Categoria.Sigla
                             select new
                             {
                                 Filme.ID,
                                 Filme.Titulo,
                                 Categoria.Categoria1
                             };

            foreach (var c in outralista)
            {
                Console.WriteLine($"ID : {c.ID}");
                Console.WriteLine($"Titulo : {c.Titulo}");
                Console.WriteLine($"Categoria : {c.Categoria1}");

                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
