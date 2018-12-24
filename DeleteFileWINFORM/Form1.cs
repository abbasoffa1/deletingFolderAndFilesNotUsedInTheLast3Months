using System;
using System.Windows.Forms;
using System.IO;
namespace DeleteFileWINFORM
{
    public partial class Form1 : Form
    {
        public DirectoryInfo openedDir { get; set; }
        public string PathDelete;

        public Form1()
        {
            InitializeComponent();
        }
        public void fillDgv()
        {
            dgvFiles.Rows.Clear();
            openedDir = new DirectoryInfo(folderDialog.SelectedPath);
            string[] files = Directory.GetFiles(openedDir.FullName);
            string[] directories = Directory.GetDirectories(openedDir.FullName);
            DateTime now = DateTime.Now;
            foreach (var item in files)
            {
                var file = Directory.GetLastAccessTime(item);
                var yeni = now.Subtract(file);

                if (yeni.Days > 91)
                {
                    dgvFiles.Rows.Add(item, yeni.Days);
                }
            }
            foreach (var item in directories)
            {
                var dir = Directory.GetLastAccessTime(item);
                var yeni = now.Subtract(dir);
                if (yeni.Days > 91)
                {
                    dgvFiles.Rows.Add(item, yeni.Days);
                }
            }
        }
        private void folderChsBtn_Click_1(object sender, EventArgs e)
        {
            folderDialog.ShowDialog();
            fillDgv();
        }

        private void dgvFiles_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            PathDelete=dgvFiles.Rows[e.RowIndex].Cells[0].Value.ToString();

            if (PathDelete.Contains("."))
            {
                DialogResult r = MessageBox.Show("File silmek isdediyinize eminsiniz?", "Delete", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    File.Delete(PathDelete);
                }
            }
            else
            {
                DialogResult r = MessageBox.Show("Folder silmek isdediyinize eminsiniz?", "Delete", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    Directory.Delete(PathDelete, true);
                }
            }
            fillDgv();
        }
    }
}
