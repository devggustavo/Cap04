using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModelProject;
using ControllerProject;
using PersistenceProject;
using ViewProject;

namespace ViewProject
{
    public partial class frmJanelaPrincipal : Form
    {

        private FornecedorCotroller fornecedorController =
        new FornecedorCotroller();
        private ProdutoController produtoController =
        new ProdutoController();
        private NotaEntradaController notaEntradaController =
        new NotaEntradaController();
        public frmJanelaPrincipal()
        {
            InitializeComponent();
        }

        private void formJanelaprincipal_Load(object sender, EventArgs e)
        {

        }

        private void fornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormFornecedor(fornecedorController).ShowDialog();
        }

        private void compraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmRegistroNotaEntrada(notaEntradaController,
            fornecedorController, produtoController).
                  ShowDialog();
        }
    }
    
}
