using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ByteBank.Agencias
{
    public delegate bool ValidacaoEventHandler(string texto);

    public class ValidacaoTextBox : TextBox
    {
        public ValidacaoTextBox()
        {
            TextChanged += ValidacaoTextBox_TextChanged;
        }

        private void ValidacaoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Background = Validacao(Text)
                ? new SolidColorBrush(Colors.White)
                : new SolidColorBrush(Colors.OrangeRed);

        }

        public event ValidacaoEventHandler Validacao;

    }
}
