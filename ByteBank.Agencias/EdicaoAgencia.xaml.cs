using ByteBank.Agencias.DAL;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ByteBank.Agencias
{
    /// <summary>
    /// Interaction logic for EdicaoAgencia.xaml
    /// </summary>
    public partial class EdicaoAgencia : Window
    {

        private readonly Agencia _agencia;
        public EdicaoAgencia(Agencia agencia)
        {
            InitializeComponent();

            _agencia = agencia ?? throw new ArgumentNullException(nameof(agencia));
            AtualizarCamposDeTexto();
            AtualizarControles();
        }

        private void AtualizarCamposDeTexto()
        {
            txtNumero.Text = _agencia.Numero;
            txtNome.Text = _agencia.Nome;
            txtDescricao.Text = _agencia.Descricao;
            txtEndereco.Text = _agencia.Endereco;
            txtTelefone.Text = _agencia.Telefone;
        }

        private void AtualizarControles()
        {
            RoutedEventHandler DialogResultTrue = (o, e) => DialogResult = true;

            RoutedEventHandler DialogResultFalse = (o, e) => DialogResult = false;

            var okEventHandler = DialogResultTrue + Fechar;
            var cancelarEventHandler =
                (RoutedEventHandler)Delegate.Combine(
                    DialogResultFalse,
                    (RoutedEventHandler)Fechar);

            btnOk.Click += new RoutedEventHandler(okEventHandler);
            btnCancelar.Click += new RoutedEventHandler(cancelarEventHandler);

            btnOk.Click += new RoutedEventHandler(Fechar);
            btnCancelar.Click += new RoutedEventHandler(Fechar);

            txtNome.Validacao += ValidateInputText;
            txtEndereco.Validacao += ValidateInputText;
            txtDescricao.Validacao += ValidateInputText;
            txtNumero.Validacao += ValidateOnlyNumbers;
            txtTelefone.Validacao += ValidateInputText;
        }

        private bool ValidateOnlyNumbers(string texto)
            => ValidateInputText(texto)
                && texto.All(char.IsDigit);

        private bool ValidateInputText(string texto)
            => !string.IsNullOrWhiteSpace(texto);

        private void Fechar(object sender, EventArgs e)
            => Close();
    }
}
