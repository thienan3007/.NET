using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectGroup.data;
using ProjectGroup.entities;

namespace ProjectGroup.view
{
    public partial class authorlistview : DevExpress.XtraEditors.XtraForm
    {
        public authorlistview()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            AuthorData authorData = new AuthorData();
            List<Author> authors = authorData.GetAuthors();
            bsAuthor.DataSource = authors;
            gridControl1.DataSource = bsAuthor;
            gridView1.PopulateColumns();
            
            gridView1.Columns.Remove(gridView1.Columns["Email"]);
        }
    }
}