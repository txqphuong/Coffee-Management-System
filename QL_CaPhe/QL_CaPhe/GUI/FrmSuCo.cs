﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_CaPhe.GUI
{
    public partial class FrmSuCo : Form
    {
        public FrmSuCo()
        {
            InitializeComponent();
        }

        private void FrmSuCo_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qL_CaPhe.SUCO' table. You can move, or remove it, as needed.
            this.sUCOTableAdapter.Fill(this.qL_CaPhe.SUCO);
            thongke();
        }
        private void btn_Xemchitiet_Click(object sender, EventArgs e)
        {
            xemchitiet(i);
        }

        private void btn_themSuCo_Click(object sender, EventArgs e)
        {
            frmPhieuNhapSuCo new_form = new frmPhieuNhapSuCo();
            new_form.Owner = this;
            this.Hide();
            new_form.Show();
        }

        private void rdo_Top_Least_Incident_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo_Top_Least_Incident.Checked)
                this.sUCOTableAdapter.FillBy_TopItChiPhi(qL_CaPhe.SUCO);
            if (rdo_Top_Most_Incident.Checked)
                this.sUCOTableAdapter.TopNhieuChiPhi_SuCo(qL_CaPhe.SUCO);
        }

        private void Ve_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmAdmin new_form = new frmAdmin();
            // this.ower = 
        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            this.sUCOTableAdapter.SuCo_NgayLap(qL_CaPhe.SUCO,NgayBatDau.Value, NgayKetThuc.Value);
        }
        void toExcel(DataTable a, string excelFilePath = null)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;  
            // Start Excel and get Application object.  
            excel = new Microsoft.Office.Interop.Excel.Application();
            // for making Excel visible  
            excel.Visible = false;
            excel.DisplayAlerts = false;
            // Creation a new Workbook  
            excelworkBook = excel.Workbooks.Add(Type.Missing);
            // Workk sheet  
            excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
            excelSheet.Name = "Test work sheet";
            excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[a.Rows.Count+1, a.Columns.Count]];
            excelCellrange.EntireColumn.AutoFit();
            Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
            border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border.Weight = 2d;
            int ColumnsCount = a.Columns.Count;
            object[] Header = new object[ColumnsCount];

            // column headings               
            for (int i = 0; i < ColumnsCount; i++)
                Header[i] = a.Columns[i].ColumnName;

            Microsoft.Office.Interop.Excel.Range HeaderRange = excelSheet.get_Range((Microsoft.Office.Interop.Excel.Range)(excelSheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)(excelSheet.Cells[1, ColumnsCount]));
            HeaderRange.Value = Header;
            HeaderRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            HeaderRange.Font.Bold = true;
            // rows
            for (var i = 0; i < a.Rows.Count; i++)
            {
                // to do: format datetime values before printing
                for (var j = 0; j < a.Columns.Count; j++)
                {
                    excelSheet.Cells[i + 2, j + 1] = a.Rows[i][j];
                }
            }
            if (!string.IsNullOrEmpty(excelFilePath))
            {
                try
                {
                    excelSheet.SaveAs(excelFilePath);
                    excel.Quit();
                    MessageBox.Show("Excel file saved!");
                }
                catch (Exception ex)
                {
                    throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                        + ex.Message);
                }
            }
            else
            { // no file path is given
                excel.Visible = true;
            }
        }
     

        private void xuấtDanhSáchSựCốToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toExcel(this.sUCOTableAdapter.GetData());
        }

        private void xuấtDanhSáchChiPhíÍtNhấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toExcel(this.sUCOTableAdapter.GetDataBy5());
        }

        private void xuấtDanhSáchChiPhíNhiềuNhấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toExcel(this.sUCOTableAdapter.GetDataBy_TopNhieuChiPhi_SuCo());
        }

        private void xuấtDanhSáchSựCốTheoThờiGianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toExcel(this.sUCOTableAdapter.GetDataBy_SuCo_NgayLap(NgayBatDau.Value, NgayKetThuc.Value));
        }

        private void Tai_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sUCOTableAdapter.Fill(qL_CaPhe.SUCO);
            thongke();
        }
        void thongke()
        {
            lbl_TongCP.Text = String.Format("{0: #,##}", int.Parse(this.sUCOTableAdapter.tongChiPhi().ToString()));
            lbl_TongSC.Text = dgv_SuCo.RowCount.ToString();
        }


        void xemchitiet(int i)
        {
            frmChiTietSuCo new_form = new frmChiTietSuCo(dgv_SuCo[0,i].Value.ToString());
            new_form.Owner = this;
            this.Hide();
            new_form.Show();
        }
        int i = 0;
        private void dgv_SuCo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            thongke();
            i = e.RowIndex;

        }

        private void TimKiem_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sUCOTableAdapter.SuCo_TimKiem(qL_CaPhe.SUCO, Text_toolStripTextBox.Text);
        }

        private void FrmSuCo_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn chắc chắn thoát chứ!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Validate())
                {
                    this.sUCOBindingSource.EndEdit();
                    sUCOTableAdapter.Update(qL_CaPhe.SUCO);
                    MessageBox.Show("Lưu thành công", "Thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        
        private void In_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

       
    }
}
