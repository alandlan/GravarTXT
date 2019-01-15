using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroVeiculoTXT
{
    public partial class lab07form : Form
    {
        public lab07form()
        {
            InitializeComponent();
        }

        private void lab07form_Load(object sender, EventArgs e)
        {
            //Carregar combustivelComboBox
            combustivelComboBox.DataSource =
            Enum.GetNames(typeof(Combustiveis));
            //Desmarcar a combo
            combustivelComboBox.SelectedIndex = -1;

            //Carregar cambioComboBox
            cambioComboBox.DataSource = Enum.GetNames(typeof(Cambios));
            //Desmarcar a combo
            cambioComboBox.SelectedIndex = -1;

            //Carregar marcaComboBox
            //Limpar dados anteriores
            marcaComboBox.Items.Clear();
            //Utilizar o endereço onde o arquivo Marcas.txt foi gravado
            System.IO.StreamReader leitor =
            new System.IO.StreamReader(
            @"C:\Cursos\Curso_ASPNet\CadastroVeiculoTXT\CadastroVeiculoTXT\src\Marcas.txt");
            while (!leitor.EndOfStream)
            {
                marcaComboBox.Items.Add(leitor.ReadLine());
            }
            leitor.Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            try
            {
                Veiculo carro = new Veiculo(
                @"C:\Cursos\Curso_ASPNet\CadastroVeiculoTXT\CadastroVeiculoTXT\Dados\Cadastro de Veículos.txt");
                //Atribuir valor às propriedades do veículo
                //com as devidas validações
                carro.Placa = placaTextBox.ValidarVazio().ValidarPlaca();
                carro.Modelo = modeloTextBox.ValidarVazio();
                carro.Cor = corTextBox.ValidarVazio();
                carro.Ano = anoTextBox.ValidarInt16().ValidarAno();
                carro.Marca = marcaComboBox.ValidarCombo();
                carro.Observacao = observacaoTextBox.Text;
                //Fazendo CAST
                carro.Combustivel =
                (Combustiveis)combustivelComboBox
                .ValidarComboEnum();
                //Fazendo CAST
                carro.Cambio =
                (Cambios)cambioComboBox
                .ValidarComboEnum();
                //Chamar o método gravar do veículo
                carro.Gravar();

                //Avisar o usuário
                MessageBox.Show("Veículo gravado com sucesso!", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Redefinir a tela
                this.LimparTela();
                placaTextBox.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Alerta de Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void observacaoTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                toquesLabel.Text =
                "Restante: " +
                (300 - observacaoTextBox.TextLength).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta de Erro",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
