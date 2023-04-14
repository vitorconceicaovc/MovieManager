using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinqBD;
using static System.Windows.Forms.DataFormats;

namespace LinqBDFormCRUD
{
    public partial class Form2 : Form
    {

        private DataClasses1DataContext dc = new DataClasses1DataContext();



        public Form2()
        {
            InitializeComponent();
            InitListaFilmes();
            initComboBoxCategoria();
        }

        private void initComboBoxCategoria()
        {
            var categorias = from c in dc.Categorias orderby c.Categoria1 select c;

            comboBoxCategoria.DisplayMember = "Categoria1";
            comboBoxCategoria.DataSource = categorias.ToList();
        }

        private void InitListaFilmes()
        {
            var filmes = from f in dc.Filmes orderby f.ID select f;

            listBoxFilmes.DataSource = filmes.ToList();
            listBoxFilmes.DisplayMember = "Titulo";

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            // verifica se um item está selecionado na listBoxFilmes
            if (listBoxFilmes.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um filme para apagar.");
                return;
            }


            // obtém o filme selecionado
            Filme filmeSelecionado = listBoxFilmes.SelectedItem as Filme;

            if (filmeSelecionado != null)
            {
                // exibe uma mensagem de confirmação
                DialogResult resposta = MessageBox.Show("Tem certeza que deseja apagar o filme " + filmeSelecionado.Titulo + "?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // se a resposta for "Sim", apaga o filme
                if (resposta == DialogResult.Yes)
                {
                    dc.Filmes.DeleteOnSubmit(filmeSelecionado);
                    dc.SubmitChanges();

                    // atualiza a listBoxFilmes
                    InitListaFilmes();
                }

            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            // verifica se uma categoria foi selecionada
            Categoria categoriaSelecionada = comboBoxCategoria.SelectedItem as Categoria;
            if (categoriaSelecionada == null)
            {
                MessageBox.Show("Selecione uma categoria.");
                return;
            }

            // obtém o último ID utilizado na tabela Filmes
            int ultimoID = dc.Filmes.OrderByDescending(f => f.ID).FirstOrDefault()?.ID ?? 0;

            // cria um novo objeto Filme com os dados do formulário
            Filme novoFilme = new Filme
            {
                ID = ultimoID + 1,
                Titulo = txtTitulo.Text,
                Categoria = categoriaSelecionada.Sigla
            };

            // adiciona o novo filme ao contexto de dados
            dc.Filmes.InsertOnSubmit(novoFilme);

            // salva as mudanças na base de dados
            dc.SubmitChanges();

            // atualiza a listBoxFilmes
            InitListaFilmes();
        }

        private void button1_Click(object sender, EventArgs e) //btnGerirFilmes
        {
            // Verifica se algum filme foi selecionado
            if (listBoxFilmes.SelectedItem != null)
            {
                // Obtém o filme selecionado na ListBoxFilmes
                Filme filmeSelecionado = (Filme)listBoxFilmes.SelectedItem;

                // Cria uma nova instância da EditarFilmeForm
                EditarFilmeForm editarFilmeForm = new EditarFilmeForm(filmeSelecionado.Categoria);

                // Define o valor da propriedade FilmeAEditar na EditarFilmeForm
                editarFilmeForm.FilmeAEditar = filmeSelecionado;

                // Abre a EditarFilmeForm
                editarFilmeForm.Show();
            }
        }
    }
}
