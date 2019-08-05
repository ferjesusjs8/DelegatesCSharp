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

            txtNome.TextChanged += ValidateInputText;
            txtEndereco.TextChanged += ValidateInputText;
            txtDescricao.TextChanged += ValidateInputText;
            txtNumero.TextChanged += ValidateOnlyNumbers;
            txtTelefone.TextChanged += ValidateInputText;
        }

        private void ValidateOnlyNumbers(object sender, TextChangedEventArgs e)
            => (sender as TextBox).Background =
                string.IsNullOrEmpty((sender as TextBox).Text)
                    ? new SolidColorBrush(Colors.OrangeRed)
#pragma warning disable S3358 // Ternary nested !!!
                    : (sender as TextBox)
                        .Text
                        .All(char.IsDigit)
                            ? new SolidColorBrush(Colors.White)
                            : new SolidColorBrush(Colors.OrangeRed);
#pragma warning restore S3358 // Ternary nested !!!

        private void ValidateInputText(object o, EventArgs e)
            => (o as TextBox)
                .Background = string.IsNullOrEmpty((o as TextBox).Text)
                    ? new SolidColorBrush(Colors.OrangeRed)
                    : new SolidColorBrush(Colors.White);

        private void Fechar(object sender, EventArgs e)
            => Close();
    }
}
