using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL2_BookStoreManagement.View
{
    public partial class fCus_Overview: Form
    {
        public fCus_Overview()
        {
            InitializeComponent();
            if (this.Parent != null)
            {
                this.Width = this.Parent.Width;
                this.Height = this.Parent.Height;
            }
        }
    }
}
