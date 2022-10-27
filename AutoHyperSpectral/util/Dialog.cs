using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoHyperSpectral.util
{
    class Dialog
    {
        public string openDialog()
        {
            //openFile
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "aviファイルを選択";
            openFileDialog.AddExtension = true;
            openFileDialog.Filter = "aviファイル(*.avi)|*.avi";

            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return "";

            string filename = openFileDialog.FileName;
            return filename;
        }

        public string openJsonFile()
        {
            //openFile
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "jsonファイルを選択";
            openFileDialog.AddExtension = true;
            openFileDialog.Filter = "jsonファイル(*.json)|*.json";

            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return "";

            string filename = openFileDialog.FileName;
            return filename;
        }
    }

}
