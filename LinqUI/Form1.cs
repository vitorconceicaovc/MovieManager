using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using LinqBD;
using LinqBDFormCRUD;

namespace LinqUI
{
    public partial class Form1 : Form
    {

        DataClasses1DataContext dc = new DataClasses1DataContext();

        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //------------------Listview---------------//
            listView1.Columns.Add("ID");
            listView1.Columns.Add("Título");
            listView1.Columns.Add("Categoria");

            //Carregar os filmes
            var lista = from Filme in dc.Filmes select Filme;

            // Adicionar cada filme como um ListViewItem à ListView
            foreach (Filme filme in lista)
            {
                ListViewItem item;
                item = listView1.Items.Add(filme.ID.ToString());
                item.SubItems.Add(filme.Titulo);
                item.SubItems.Add(filme.Categoria);
            }

            // Redimensionar colunas para ajustar o tamanho do cabeçalho
            for (int i = 0; i <= 2; i++)
            {
                listView1.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }

            //chamar metodo de clicar no titulo da coluna
            listView1.ColumnClick += listViewColumn_Click;





            //------------------TreeView---------------//
            var outraLista = from Categoria in dc.Categorias select Categoria;

            foreach (Categoria categoria in outraLista)
            {
                treeView1.Nodes.Add(categoria.Sigla);
            }

            //segundo nivel da árvore (Filmes)

            var listaFilmes = from Filme in dc.Filmes
                              orderby Filme.Titulo
                              select Filme;

            string catFilme;

            foreach (Filme filme in listaFilmes)
            {
                catFilme = filme.Categoria;

                foreach (TreeNode node in treeView1.Nodes)
                {
                    if(node.Text == catFilme)
                    {
                        node.Nodes.Add($"ID:{filme.ID.ToString()} Título: {filme.Titulo}");
                    }
                }
            }


            //------------------GridView---------------//

            dataGridView1.Columns.Add("colId", "ID");
            dataGridView1.Columns.Add("colTítulo", "Título");
            dataGridView1.Columns.Add("colCategoria", "Categoria");

            var outraListaDeFilmes = from Filme in dc.Filmes select Filme;

            int linha = 0;

            DataGridViewCellStyle estilo = new DataGridViewCellStyle();
            estilo.ForeColor = Color.Red;

            //criar estilo para fundo amarelo
            DataGridViewCellStyle estiloFundoAmarelo = new DataGridViewCellStyle();
            estiloFundoAmarelo.BackColor = Color.Yellow;

            foreach (Filme filme in outraListaDeFilmes)
            {
                DataGridViewRow registo = new DataGridViewRow();
                dataGridView1.Rows.Add(registo);

                dataGridView1.Rows[linha].Cells[0].Value = filme.ID;
                dataGridView1.Rows[linha].Cells[1].Value = filme.Titulo;
                dataGridView1.Rows[linha].Cells[2].Value = filme.Categoria;

                if ((string)dataGridView1.Rows[linha].Cells[2].Value == "FA")
                {
                    dataGridView1.Rows[linha].DefaultCellStyle = estilo;
                }

                //aplicar o fundo se for da categoria FD
                if ((string)dataGridView1.Rows[linha].Cells[2].Value == "FD")
                {
                    dataGridView1.Rows[linha].DefaultCellStyle = estiloFundoAmarelo;
                }

                linha++;
            }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;   

        }

        //metodo de clicar no titulo da coluna
        private void listViewColumn_Click(object sender, ColumnClickEventArgs e)
        {

            

            // Determinar qual propriedade deve ser usada para ordenação com base no índice da coluna clicada
            Func<Filme, object> propriedade;
            switch (e.Column)
            {
                case 0: // ID
                    propriedade = f => f.ID;

                    //limpar listview
                    listView1.Items.Clear();

                    //ordenar
                    var listaPorID = from filme in dc.Filmes
                                orderby filme.ID ascending
                                select filme;

                    //listar
                    foreach (Filme filme in listaPorID)
                    {
                        ListViewItem item;
                        item = listView1.Items.Add(filme.ID.ToString());
                        item.SubItems.Add(filme.Titulo);
                        item.SubItems.Add(filme.Categoria);
                    }

                    break;


                case 1: // Título
                    propriedade = f => f.Titulo;

                    //limpar listview
                    listView1.Items.Clear();

                    //ordenar
                    var listaPorTitulo = from filme in dc.Filmes
                                orderby filme.Titulo ascending
                                select filme;

                    //listar
                    foreach (Filme filme in listaPorTitulo)
                    {
                        ListViewItem item;
                        item = listView1.Items.Add(filme.ID.ToString());
                        item.SubItems.Add(filme.Titulo);
                        item.SubItems.Add(filme.Categoria);
                    }

                    break;


                case 2: // Categoria
                    propriedade = f => f.Categoria;

                    //limpar listview
                    listView1.Items.Clear();

                    //ordenar
                    var listaPorCategoria = from filme in dc.Filmes
                                orderby filme.Categoria ascending
                                select filme;

                    //listar
                    foreach (Filme filme in listaPorCategoria)
                    {
                        ListViewItem item;
                        item = listView1.Items.Add(filme.ID.ToString());
                        item.SubItems.Add(filme.Titulo);
                        item.SubItems.Add(filme.Categoria);
                    }

                    break;

                default:
                    return;
            }

            
        }

        private void btnGerirFilmes_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void btnAtualisar_Click(object sender, EventArgs e)
        {
            
        }
    }
}
