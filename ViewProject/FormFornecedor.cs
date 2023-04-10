using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControllerProject;
using ModelProject;

namespace ViewProject
{
    public partial class FormFornecedor : Form
    {
        private FornecedorCotroller controller =
            new FornecedorCotroller();

        public FormFornecedor()
        {
            InitializeComponent();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            var fornecedor = this.controller.Insert(
                new Fornecedor()
                {
                    Id = Guid.NewGuid(),
                    Nome = txtNome.Text,
                    CNPJ = txtCnpj.Text
                });

            txtId.Text = fornecedor.Id.ToString();
            dgvFornecedor.DataSource = null;
            dgvFornecedor.DataSource = this.controller.GetAll();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtId.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtCnpj.Text = string.Empty;
        }

        private void dgvFornecedor_SelectionChanged(object sender, EventArgs e)
        {
            txtId.Text = dgvFornecedor.CurrentRow.Cells[0].
                Value.ToString();
            txtNome.Text = dgvFornecedor.CurrentRow.Cells[1].
                Value.ToString();
            txtCnpj.Text = dgvFornecedor.CurrentRow.Cells[2].
                Value.ToString();
        }

        private void ClearControls()
        {
            dgvFornecedor.ClearSelection();
            txtId.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtCnpj.Text = string.Empty;
            txtNome.Focus();
        }
    }
}
