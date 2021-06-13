using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HkClothes
{
    public partial class LoadingForm : Form
    {
        public Action worker { get; set; }
        List<Task> tasks = new List<Task>();
        public LoadingForm(Action worker)
        {
            InitializeComponent();
            if (worker == null) throw new ArgumentNullException();
            this.worker = worker;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Task.Factory.StartNew(worker).ContinueWith(t => { 
                
                this.Close(); 
            }, TaskScheduler.FromCurrentSynchronizationContext());
            
        }
    }
}
