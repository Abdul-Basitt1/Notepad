using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;

namespace Notepad
{
    public partial class Formnotepad : Form
    {

        public Formnotepad()
        {
            InitializeComponent();
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var option = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.OKCancel);
            if(option == DialogResult.OK)
            {
                saveToolStripMenuItem_Click(sender, e);
                notepad.Clear(); 
            }
            else 
            {
                notepad.Clear();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (notepad.Text != string.Empty)
            {
                var option = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.OKCancel);
                if (option == DialogResult.OK)
                {
                    saveToolStripMenuItem_Click(sender, e);
                    notepad.Clear();
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|*.*";
                    if(openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.StreamReader OpenFile = new System.IO.StreamReader(openFileDialog.FileName);
                        notepad.Text = OpenFile.ReadToEnd();
                        OpenFile.Close();
                    }
                   
                }
                else
                {
                    notepad.Clear();
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|*.*";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.StreamReader OpenFile = new System.IO.StreamReader(openFileDialog.FileName);
                        notepad.Text = OpenFile.ReadToEnd();
                        OpenFile.Close();
                    }
                }
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamReader OpenFile = new System.IO.StreamReader(openFileDialog.FileName);
                    notepad.Text = OpenFile.ReadToEnd();
                    OpenFile.Close();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(saveFileDialog.FileName);
                SaveFile.WriteLine(notepad.Text);
                SaveFile.Close();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
            
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog print = new PrintPreviewDialog();
            PrintDocument printDocument = new PrintDocument();
            PrintDialog printDialog = new PrintDialog();
            print.Document = printDocument;
            if(printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void prntDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(notepad.Text, new Font("Times New Roman", 14, FontStyle.Bold),Brushes.Black,new PointF(100,100 ));
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Drawing.Printing.PrintDocument prntDoc = new System.Drawing.Printing.PrintDocument();
            PrintPreviewDialog preview = new PrintPreviewDialog();
            prntDoc.PrintPage += new
            System.Drawing.Printing.PrintPageEventHandler(prntDoc_PrintPage);
            preview.Document = prntDoc;
            if (preview.ShowDialog(this) == DialogResult.OK)
            {
                prntDoc.Print();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.SelectAll();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                notepad.Font = fontDialog.Font;
            }
        }

        private void formatToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                notepad.ForeColor = colorDialog.Color;
            }
        }

        private void upperCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Text = notepad.Text.ToUpper();
        }

        private void lowerCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Text = notepad.Text.ToLower();
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CaretPosition = notepad.SelectionStart;
            string TextBefore = notepad.Text.Substring(0, CaretPosition);
            string textAfter = notepad.Text.Substring(CaretPosition);
            string currentDate = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"); 
            notepad.Text = TextBefore + currentDate + textAfter;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.Clear();
        }

        private void notepad_TextChanged(object sender, EventArgs e)
        {
            if(notepad.Text.Length > 0)
            {
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
            }
            else
            {
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
            }
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(wordWrapToolStripMenuItem.Checked == true)
            {
                wordWrapToolStripMenuItem.Checked = false;
                notepad.WordWrap = false;
            }
            else
            {
                wordWrapToolStripMenuItem.Checked = true;
                notepad.WordWrap = true;
            }
        }

        private void highlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.SelectionBackColor = Color.Yellow;
        }


        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float currentsize;
            currentsize = notepad.Font.Size;
            currentsize = currentsize + 2.0f;
            notepad.Font = new Font(notepad.Font.Name, currentsize);
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float currentsize;
            currentsize = notepad.Font.Size;
            currentsize = currentsize - 2.0f;
            notepad.Font = new Font(notepad.Font.Name, currentsize);
        }

        private void restoreZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float currentsize = 8.0f;
            notepad.Font = new Font(notepad.Font.Name, currentsize);


        }

        private void mousehovercount(object sender, EventArgs e)
        {
            MatchCollection wordColl = Regex.Matches(notepad.Text, @"[\W]+");          
            toolStripStatusLabel1.Text = "Total words : " + wordColl.Count;
            toolStripStatusLabel2.Text = "";
        }

        

        private void notepad_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "Mouse away from notepad";
        }

        private void notepad_DoubleClick(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void notepad_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", e.LinkText);
        }
    }
}
