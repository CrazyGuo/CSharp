using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IBatisQueryGenerator
{
    class MyException : System.Exception
    {
        private string message;

        public MyException(string message)
        {
            this.message = message;
        }

        public string getMessage()
        {
            return this.message;
        }

        public void showMessage()
        {
            MessageBox.Show(message);
        }
    }
}
