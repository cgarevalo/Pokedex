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
            //ESto es un evento para cambiar de imagenes
            if (dgvPokemons.CurrentRow != null)
            {
                Pokemon selecionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
                CargarImagen(selecionado.UrlImagen);

            }
        }

        private void Cargar()
        {
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                listaPokemon = negocio.listar();
                dgvPokemons.DataSource = listaPokemon;
                OcultarColumnas();
                CargarImagen(listaPokemon[0].UrlImagen);//listaPokemon[0] porque muestra el primer pokemon de la lista listaPokemon
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OcultarColumnas()
        {
            dgvPokemons.Columns["UrlImagen"].Visible = false;
            dgvPokemons.Columns["Id"].Visible = false;
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado;
            seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            frmAltaPokemon modificar = new frmAltaPokemon(seleccionado);
            modificar.ShowDialog();
            Cargar();
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            Eliminar(); //No tiene argumento
                        //Pero podría tener false como argumento y sería lo mismo
        }

        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            Eliminar(true); //Tiene true como argumento (un booleano)
        }

        private void Eliminar(bool logico = false)
        {
            //A logico se lo pone en false para que sea opcional agregar un bool como argumento
            //Por eso, cuando los botones llaman al método, uno esta sin argumento y el otro tiene true como argumento
            PokemonNegocio negocio = new PokemonNegocio();
            Pokemon seleccionado;
            try
            {                                             //pregunta              //titulo
                DialogResult resultado = MessageBox.Show("¿Quiere eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); //El MessageBoxIcon.Warning es opcional
                                                                                                                                                //Solo muestra un icono
                if (resultado == DialogResult.Yes)
                {
                    seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;

                    if (logico)
                    {
                    negocio.EliminarLogico(seleccionado.Id);
                    }
                    else
                    {
                        negocio.EliminarFisico(seleccionado.Id);
                    }

                    Cargar();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Evento KeyPress de txtFiltro
            
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            //Evento TextChanged de txtFiltro
            List<Pokemon> listaFiltrada;
            string filtro = txtFiltro.Text;

            if (filtro != "")
            {
                //Busca al pokemon escrito en el txtFiltro
                listaFiltrada = listaPokemon.FindAll(p => p.Nombre.ToUpper().Contains(filtro.ToUpper()) || p.Tipo.Descripcion.ToUpper().Contains(filtro.ToUpper()));//Esto lo pasa a mayúsculas y busca coincidencias
            }
            else
            {
                listaFiltrada = listaPokemon;
            }

            //Actualiza el datagridview con la nueva lista
            dgvPokemons.DataSource = null;
            dgvPokemons.DataSource = listaFiltrada;
            OcultarColumnas();
        }
    }
}
