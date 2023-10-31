using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace frmPokemons
{
    public partial class frmPokemons : Form
    {
        private List<Pokemon> listaPokemon;

        public frmPokemons()
        {
            InitializeComponent();
        }

        private void frmPokemons_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon selecionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            CargarImagen(selecionado.UrlImagen);
        }

        private void Cargar()
        {
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                listaPokemon = negocio.listar();
                dgvPokemons.DataSource = listaPokemon;
                dgvPokemons.Columns["UrlImagen"].Visible = false;
                //listaPokemon[0] porque muestra el primer pokemon de la lista listaPokemon
                CargarImagen(listaPokemon[0].UrlImagen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CargarImagen(string imagen)
        {
            try
            {
                pbxPokemon.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxPokemon.Load("https://image.shutterstock.com/image-vector/ui-image-placeholder-wireframes-apps-260nw-1037719204.jpg");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaPokemon alta = new frmAltaPokemon();
            alta.ShowDialog();
            Cargar();
        }
    }
}
