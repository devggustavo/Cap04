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
    public partial class FrmRegistroNotaEntrada : Form
    {
        private NotaEntradaController controller;
        private FornecedorCotroller fornecedorController;
        private ProdutoController produtoController;

        private NotaEntrada notaAtual;

        public FrmRegistroNotaEntrada
            (NotaEntradaController controller,
             FornecedorCotroller fornecedorController,
             ProdutoController produtoController)
        {

            InitializeComponent();
            this.controller = controller;
            this.fornecedorController =
            fornecedorController;
            this.produtoController = produtoController;
            InicializaComboBoxs();
        }

        private void InicializaComboBoxs()
        {
            cbxFornecedor.Items.Clear();
            cbxProduto.Items.Clear();
            foreach (Fornecedor fornecedor in
            this.fornecedorController.GetAll())
            {
                cbxFornecedor.Items.Add(fornecedor);
            }
            foreach (Produto produto in
            this.produtoController.GetAll())
            {
                cbxProduto.Items.Add(produto);
            }
        }

        private void ClearcontrolsNota()
        {
            dgvNotasEntrada.ClearSelection();
            dgvProdutos.ClearSelection();
            txtIdNotaEntrada.Text = string.Empty;
            cbxFornecedor.SelectedIndex = -1;
            txtNumero.Text = string.Empty;
            dtpEmissao.Value = DateTime.Now;
            dtpEntrada.Value = DateTime.Now;
            cbxFornecedor.Focus();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            ClearcontrolsNota();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            var notaEntrada = new NotaEntrada()
            {
                Id = (txtIdNotaEntrada.Text == string.Empty ?
              Guid.NewGuid() : new Guid(txtIdNotaEntrada.Text)),
                DataEmissao = dtpEmissao.Value,
                DataEntrada = dtpEntrada.Value,
                FornecedorNota = (Fornecedor)cbxFornecedor.SelectedItem,
                Numero = txtNumero.Text
            };

            notaEntrada = (txtIdNotaEntrada.Text == string.Empty ?
                this.controller.Insert(notaEntrada) :
                this.controller.Update(notaEntrada));
            dgvNotasEntrada.DataSource = null;
            dgvNotasEntrada.DataSource = this.controller.GetAll();
            ClearcontrolsNota();



        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearcontrolsNota();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (txtIdNotaEntrada.Text == string.Empty)
            {
                MessageBox.Show(
                "Selecione a NOTA a ser removida no GRID");
            }
            else
            {
                this.controller.Remove(
                new NotaEntrada()
                {
                    Id = new Guid(txtIdNotaEntrada.Text)
                });
                dgvNotasEntrada.DataSource = null;
                dgvNotasEntrada.DataSource =
                this.controller.GetAll();
                ClearcontrolsNota();
            }
        }
 
        private void dgvNotasEntrada_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                this.notaAtual = this.controller.
                GetNotaEntradaById((Guid)dgvNotasEntrada.
                CurrentRow.Cells[0].Value);
                txtIdNotaEntrada.Text = notaAtual.Id.
                ToString();
                txtNumero.Text = notaAtual.Numero;
                cbxFornecedor.SelectedItem = notaAtual.
                FornecedorNota;
                dtpEmissao.Value = notaAtual.DataEmissao;
                dtpEntrada.Value = notaAtual.DataEntrada;
                UpdateProdutosGrid();
            }
            catch (Exception)
            {
                this.notaAtual = new NotaEntrada();
            }

            
        }

        private void UpdateProdutosGrid()
        {
            dgvProdutos.DataSource = null;
            if (this.notaAtual.Produtos.Count > 0)
            {
                dgvProdutos.DataSource = this.notaAtual.
                Produtos;
            }
        }



    }
}
