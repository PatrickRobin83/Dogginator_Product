using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DogginatorLibrary.Messages
{
    public static class SuccessMessages
    {
        public static void ChangesSavedSuccess()
        {
            MessageBox.Show("Änderungen wurden gespeichert", "Hinweis", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }
    }
}
