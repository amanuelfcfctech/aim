using System;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Office.Drawing;
using DevExpress.XtraRichEdit.Model;

namespace CFCAIM
{					   
	public partial class TestGrid : Form
	{		
		public TestGrid()
		{
			InitializeComponent();
			Random rnd = new Random();
			DataTable table = new DataTable();
			table.Columns.Add("Value1");
			table.Columns.Add("Value2");
			for (int i = 0; i < rnd.Next(100); i++)
			{
				table.Rows.Add(new object[] { "Custom" + rnd.Next(15), "Address" + rnd.Next(20) });
			}
			gridControl1.DataSource = table;
		}

		/*	
		GridHitInfo CalcColumnPanelHitInfo(Point pt, GridHitInfo hi)
		{
			GridColumnInfoArgs ci = CalcColumnPanelHitInfo(pt);
			bool bottomEdge;
			bottomEdge = IntInRangeBottom(pt.Y, ci.Bounds.Bottom, ci.Bounds.Bottom - ControlUtils.ColumnResizeEdgeSize);
			if (bottomEdge)
			{
				hi.HitTest = GridHitTest.RowEdge;
			}
			return hi;
		}

		protected bool IntInRangeBottom(int y, int top, int bottom)
		{
			if (bottom < top)
			{
				int temp = top;
				top = bottom;
				bottom = temp;
			}
			return (y >= top && y < bottom);
		}

		protected internal bool CheckHitTest(Rectangle bounds, Point point, GridHitTest hitTest)
		{
			if (GridDrawing.PtInRect(bounds, point))
			{
				return true;
			}
			return false;
		}						

		protected override void EndRowSizing()
		{
			int rowHandle = (int)(Painter.ReSizingObject);
			if (rowHandle > 0)
			{
				GridRowInfo ri = ViewInfo.GetGridRowInfo(rowHandle);
				if (ri == null) return;
				int height = (Painter.CurrentSizerPos - ri.DataBounds.Top) / ViewInfo.RowLineCount;
				if (height > 0)
					RowHeight = height;
			}
			else
			{
				GridColumnsInfo ci = ViewInfo.ColumnsInfo;
				if (ci == null) return;
				int height = (Painter.CurrentSizerPos - ci.FirstColumnInfo.Bounds.Top) / ViewInfo.RowLineCount;
				if (height > 0)
					ColumnPanelRowHeight = height;
			}
		}

		*/   
    }
}
