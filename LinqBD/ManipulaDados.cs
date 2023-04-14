using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqBD
{
    class ManipulaDados
    {
        static void Main(string[] args)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();

            //Inserir novo registo
            Filme f = new Filme { 
                ID = dc.Filmes.Count() +100,
                Titulo = "O Exterminador 3",
                Categoria = "FA"
            };

            dc.Filmes.InsertOnSubmit(f);

            try
            {
                dc.SubmitChanges();
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }

            var lista = from Filme in dc.Filmes select Filme;

            foreach (Filme filme in lista)
            {
                Console.WriteLine($"ID : {filme.ID}");
                Console.WriteLine($"Titulo : {filme.Titulo}");
                Console.WriteLine($"Categoria : {filme.Categoria}");

                Console.WriteLine();
            }

            Console.WriteLine("Existem {0} filmes.", lista.Count());

            Console.WriteLine("------------------------Alterar Registo----------------------");
            //Alterar registo
            
            int idAAlterar = 4;

            var pesquisa = from Filme in dc.Filmes
                           where Filme.ID == idAAlterar
                           select Filme;

            f = pesquisa.Single();  
            f.Titulo = "O Título foi alterado";

            try
            {
                dc.SubmitChanges();
            }
            catch (Exception e )
            {
                Console.WriteLine(e.Message);
            }

            var lista2 = from Filme in dc.Filmes select Filme;

            foreach (Filme filme in lista2)
            {
                Console.WriteLine($"ID : {filme.ID}");
                Console.WriteLine($"Titulo : {filme.Titulo}");
                Console.WriteLine($"Categoria : {filme.Categoria}");

                Console.WriteLine();
            }

            Console.WriteLine("Existem {0} filmes.", lista.Count());

            Console.WriteLine("------------------------Eliminar Registo----------------------");
            //Eliminar registo

            Filme f2 = new Filme();

            var outraPesquisa = from Filme in dc.Filmes
                                where Filme.ID == 5
                                select Filme;

            if(outraPesquisa.Count() == 0)
            {
                Console.WriteLine("O filme a apagar já foi apagado");
                Console.ReadKey();  
                return;
            }

            f2 = outraPesquisa.Single();

            dc.Filmes.DeleteOnSubmit(f2);

            try
            {
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var listaApagada = from Filme in dc.Filmes select Filme;

            foreach (Filme filme in listaApagada)
            {
                Console.WriteLine($"ID : {filme.ID}");
                Console.WriteLine($"Titulo : {filme.Titulo}");
                Console.WriteLine($"Categoria : {filme.Categoria}");

                Console.WriteLine();
            }

            Console.WriteLine("Existem {0} filmes.", lista.Count());

            Console.ReadKey();
        }
    }
}
