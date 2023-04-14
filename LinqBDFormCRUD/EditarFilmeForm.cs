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

namespace LinqBDFormCRUD
{
    public partial class EditarFilmeForm : Form
    {

        public Filme FilmeAEditar { get; set; }

        private DataClasses1DataContext dc = new DataClasses1DataContext();


        public EditarFilmeForm( string categoriaSelecionada)
        {
            InitializeComponent();
            initComboBoxCategoria(categoriaSelecionada);
        }

        private void initComboBoxCategoria( string categoriaSelecionada)
        {
            var categorias = from c in dc.Categorias orderby c.Categoria1 select c;

            comboBoxCategoria.DisplayMember = "Categoria1";
            comboBoxCategoria.DataSource = categorias.ToList();

            comboBoxCategoria.SelectedItem = categorias.FirstOrDefault(c => c.Categoria1 == categoriaSelecionada);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void EditarFilmeForm_Load(object sender, EventArgs e) //Carregar ao iniciar a form
        {
            // Verifica se há um filme para ser editado
            if (FilmeAEditar != null)
            {
                // Define o valor do txtID com o ID do filme
                txtID.Text = FilmeAEditar.ID.ToString();

                // Define o valor do comboboxCategorias com a categoria do filme                        
                initComboBoxCategoria(FilmeAEditar.Categoria1.ToString());                

                // Define o valor do txtTitulo com o título do filme
                txtTitulo.Text = FilmeAEditar.Titulo;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Verifica se há um filme para ser editado
            if (FilmeAEditar != null)
            {
                // Atualiza os valores do filme com os valores dos campos da interface do usuário
                FilmeAEditar.ID = Convert.ToInt32(txtID.Text);
                FilmeAEditar.Titulo = txtTitulo.Text;

                if (comboBoxCategoria.SelectedItem != null)
                {
                    FilmeAEditar.Categoria1 = comboBoxCategoria.SelectedItem as Categoria;
                }
                else
                {
                    FilmeAEditar.Categoria1 = null;
                }

                // Atualiza o registro do filme no banco de dados
                dc.SubmitChanges();

                // Fecha a janela de edição
                this.Close();
            }
        }
    }
}
