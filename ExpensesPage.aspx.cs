using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExpensesPage : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sExpensesNo = "";
    public MainModel modCompInfo = new MainModel();
    public MainModel oModExpenses = new MainModel();
    public ArrayList lsExpensesLineItem = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            initialValues();
            processValues();
        }
    }
    private void initialValues()
    {
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Session["comp"] != null)
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Request.QueryString["expensesno"] != null)
        {
            sExpensesNo = Request.QueryString["expensesno"].ToString();
        }
    }
    private void processValues()
    {
        modCompInfo = oMainCon.getCompInfoDetails(sCurrComp);
        oModExpenses = oMainCon.getExpensesHeaderDetails(sCurrComp, sExpensesNo);
        lsExpensesLineItem = oMainCon.getExpensesDetailsList(sCurrComp, sExpensesNo, 0, "");
        generatePDFFile();
    }
    private void generatePDFFile()
    {
        // Create a MigraDoc document
        Document doc = new Document();
        doc.Info.Title = "Expenses";
        doc.Info.Subject = "Expenses No:" + oModExpenses.GetSetexpensesno;
        doc.Info.Author = "B.I.O.App System";

        //set page orientation
        doc.DefaultPageSetup.Orientation = MigraDoc.DocumentObjectModel.Orientation.Portrait;
        doc.DefaultPageSetup.TopMargin = "7.5cm"; //120
        doc.DefaultPageSetup.BottomMargin = "8.5cm"; //150
        doc.DefaultPageSetup.LeftMargin = 40;
        doc.DefaultPageSetup.RightMargin = 40;
        doc.DefaultPageSetup.HeaderDistance = "1.5cm"; //20
        doc.DefaultPageSetup.FooterDistance = "5.5cm"; //20

        MigraDoc.DocumentObjectModel.Style style = doc.Styles["Normal"];
        // Because all styles are derived from Normal, the next line changes the 
        // font of the whole document. Or, more exactly, it changes the font of
        // all styles and paragraphs that do not redefine the font.
        style.Font.Name = "Verdana";

        // Create a new style called Table based on style Normal
        style = doc.Styles.AddStyle("Table", "Normal");
        style.Font.Name = "Verdana";
        style.Font.Name = "Arial";
        style.Font.Size = 8;

        // Each MigraDoc document needs at least one section.
        Section section = doc.AddSection();

        // Put Capital Digital logo in the header
        /*
        string logo_lima = Server.MapPath("~/images/logo_CD_bgBLUE_BLACK_310x98.png");
        MigraDoc.DocumentObjectModel.Shapes.Image image = section.Headers.Primary.AddImage(logo_lima);
        image.Height = "1cm";
        image.LockAspectRatio = true;
        image.RelativeVertical = RelativeVertical.Line;
        image.RelativeHorizontal = RelativeHorizontal.Margin;
        image.Top = ShapePosition.Top;
        image.Left = ShapePosition.Right;
        image.WrapFormat.Style = WrapStyle.Through;
        */
        // Put CD logo in the header
        /*
        string logo_mod = Server.MapPath("~/images/logo_versi_4.png");
        MigraDoc.DocumentObjectModel.Shapes.Image image2 = section.Headers.Primary.AddImage(logo_mod);
        image2.Height = "1.5cm";
        image2.LockAspectRatio = true;
        image2.RelativeVertical = RelativeVertical.Line;
        image2.RelativeHorizontal = RelativeHorizontal.Margin;
        image2.Top = ShapePosition.Top;
        image2.Left = ShapePosition.Left;
        image2.WrapFormat.Style = WrapStyle.Through;
        */

        // Create Header

        Paragraph header2 = section.Headers.Primary.AddParagraph();
        header2.AddText(modCompInfo.GetSetcomp_name);
        header2.Format.Font.Size = 8;
        header2.Format.Font.Bold = true;
        header2.Format.Alignment = ParagraphAlignment.Center;

        Paragraph header3 = section.Headers.Primary.AddParagraph();
        header3.AddText(modCompInfo.GetSetcomp_address);
        header3.Format.Font.Size = 7;
        header3.Format.Font.Bold = false;
        header3.Format.Alignment = ParagraphAlignment.Center;

        header3 = section.Headers.Primary.AddParagraph();
        header3.AddText("website: " + modCompInfo.GetSetcomp_website);
        header3.Format.Font.Size = 7;
        header3.Format.Font.Bold = false;
        header3.Format.Alignment = ParagraphAlignment.Center;

        header3 = section.Headers.Primary.AddParagraph();
        header3.AddText("Tel: " + modCompInfo.GetSetcomp_contactno);
        header3.Format.Font.Size = 7;
        header3.Format.Font.Bold = false;
        header3.Format.Alignment = ParagraphAlignment.Center;

        Paragraph header = section.Headers.Primary.AddParagraph();
        header.AddText("");
        header.Format.Font.Size = 12;
        header.Format.Font.Bold = true;
        header.Format.Alignment = ParagraphAlignment.Center;

        header = section.Headers.Primary.AddParagraph();
        header.AddText("VOUCHER BAYARAN");
        header.Format.Font.Size = 12;
        header.Format.Font.Bold = true;
        header.Format.Alignment = ParagraphAlignment.Center;


        // Create main section for Sales Order 
        //Paragraph main = section.AddParagraph();
        // main = section.AddParagraph();
        //main.Format.SpaceBefore = 1;

        // Create the item table for header
        //MigraDoc.DocumentObjectModel.Tables.Table tableTop = section.AddTable();
        MigraDoc.DocumentObjectModel.Tables.Table tableTop = section.Headers.Primary.AddTable();
        tableTop.Style = "Table";
        tableTop.Borders.Color = MigraDoc.DocumentObjectModel.Colors.Blue;
        tableTop.Borders.Width = 0.25;
        tableTop.Borders.Left.Width = 0.5;
        tableTop.Borders.Right.Width = 0.5;
        tableTop.Rows.LeftIndent = 0;

        // Before you can add a row, you must define the columns
        Column columnTop = tableTop.AddColumn("2cm");
        columnTop.Format.Alignment = ParagraphAlignment.Left;
        columnTop = tableTop.AddColumn("7cm");
        columnTop.Format.Alignment = ParagraphAlignment.Left;
        columnTop = tableTop.AddColumn("2cm");
        columnTop.Format.Alignment = ParagraphAlignment.Left;
        columnTop = tableTop.AddColumn("2cm");
        columnTop.Format.Alignment = ParagraphAlignment.Left;
        columnTop = tableTop.AddColumn("5cm");
        columnTop.Format.Alignment = ParagraphAlignment.Left;

        Row rowTop = tableTop.AddRow();
        rowTop.Borders.Left.Visible = false;
        rowTop.Borders.Right.Visible = false;
        rowTop.Borders.Top.Visible = false;
        rowTop.Borders.Bottom.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Borders.Left.Visible = false;
        rowTop.Borders.Right.Visible = false;
        rowTop.Borders.Top.Visible = false;
        rowTop.Borders.Bottom.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Borders.Left.Visible = false;
        rowTop.Borders.Right.Visible = false;
        rowTop.Borders.Top.Visible = false;
        rowTop.Borders.Bottom.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Borders.Left.Visible = false;
        rowTop.Borders.Right.Visible = false;
        rowTop.Borders.Top.Visible = false;
        rowTop.Borders.Bottom.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Cells[0].AddParagraph("Bayar Kepada: ");
        rowTop.Cells[0].Borders.Left.Visible = false;
        rowTop.Cells[0].Borders.Right.Visible = false;
        rowTop.Cells[0].Borders.Bottom.Visible = false;
        rowTop.Cells[1].AddParagraph(oModExpenses.GetSetbpdesc);
        rowTop.Cells[1].Borders.Left.Visible = false;
        rowTop.Cells[1].Borders.Right.Visible = false;
        rowTop.Cells[1].Borders.Bottom.Visible = false;
        rowTop.Cells[2].AddParagraph();
        rowTop.Cells[2].Borders.Left.Visible = false;
        rowTop.Cells[2].Borders.Right.Visible = false;
        rowTop.Cells[2].Borders.Bottom.Visible = false;
        rowTop.Cells[3].AddParagraph("No. Bil & Belanja: ");
        rowTop.Cells[3].Borders.Left.Visible = false;
        rowTop.Cells[3].Borders.Right.Visible = false;
        rowTop.Cells[3].Borders.Bottom.Visible = false;
        rowTop.Cells[4].AddParagraph(oModExpenses.GetSetexpensesno);
        rowTop.Cells[4].Borders.Left.Visible = false;
        rowTop.Cells[4].Borders.Right.Visible = false;
        rowTop.Cells[4].Borders.Bottom.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Cells[0].AddParagraph();
        rowTop.Cells[0].Borders.Left.Visible = false;
        rowTop.Cells[0].Borders.Right.Visible = false;
        rowTop.Cells[0].Borders.Bottom.Visible = false;
        rowTop.Cells[0].MergeDown = 2;
        rowTop.Cells[1].AddParagraph(oModExpenses.GetSetbpaddress);
        rowTop.Cells[1].AddParagraph(oModExpenses.GetSetbpcontact);
        rowTop.Cells[1].Borders.Left.Visible = false;
        rowTop.Cells[1].Borders.Right.Visible = false;
        rowTop.Cells[1].Borders.Bottom.Visible = false;
        rowTop.Cells[1].MergeDown = 2;
        rowTop.Cells[2].AddParagraph();
        rowTop.Cells[2].Borders.Left.Visible = false;
        rowTop.Cells[2].Borders.Right.Visible = false;
        rowTop.Cells[2].Borders.Bottom.Visible = false;
        rowTop.Cells[2].MergeDown = 2;
        rowTop.Cells[3].AddParagraph("Tarikh: ");
        rowTop.Cells[3].Borders.Left.Visible = false;
        rowTop.Cells[3].Borders.Right.Visible = false;
        rowTop.Cells[3].Borders.Bottom.Visible = false;
        rowTop.Cells[4].AddParagraph(oModExpenses.GetSetexpensesdate);
        rowTop.Cells[4].Borders.Left.Visible = false;
        rowTop.Cells[4].Borders.Right.Visible = false;
        rowTop.Cells[4].Borders.Bottom.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Cells[3].AddParagraph("Status: ");
        rowTop.Cells[3].Borders.Left.Visible = false;
        rowTop.Cells[3].Borders.Right.Visible = false;
        rowTop.Cells[3].Borders.Bottom.Visible = false;
        rowTop.Cells[4].AddParagraph(oModExpenses.GetSetstatus);
        rowTop.Cells[4].Borders.Left.Visible = false;
        rowTop.Cells[4].Borders.Right.Visible = false;
        rowTop.Cells[4].Borders.Bottom.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Cells[0].AddParagraph("Rujukan: ");
        rowTop.Cells[0].Borders.Left.Visible = false;
        rowTop.Cells[0].Borders.Right.Visible = false;
        rowTop.Cells[0].Borders.Bottom.Visible = false;
        rowTop.Cells[1].AddParagraph(oModExpenses.GetSetremarks);
        rowTop.Cells[1].Borders.Left.Visible = false;
        rowTop.Cells[1].Borders.Right.Visible = false;
        rowTop.Cells[1].Borders.Bottom.Visible = false;
        rowTop.Cells[2].AddParagraph();
        rowTop.Cells[2].Borders.Left.Visible = false;
        rowTop.Cells[2].Borders.Right.Visible = false;
        rowTop.Cells[2].Borders.Bottom.Visible = false;
        rowTop.Cells[3].AddParagraph();
        rowTop.Cells[3].Borders.Left.Visible = false;
        rowTop.Cells[3].Borders.Right.Visible = false;
        rowTop.Cells[3].Borders.Bottom.Visible = false;
        rowTop.Cells[4].AddParagraph();
        rowTop.Cells[4].Borders.Left.Visible = false;
        rowTop.Cells[4].Borders.Right.Visible = false;
        rowTop.Cells[4].Borders.Bottom.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Cells[0].AddParagraph("Jenis Bayaran: ");
        rowTop.Cells[0].Borders.Left.Visible = false;
        rowTop.Cells[0].Borders.Right.Visible = false;
        rowTop.Cells[0].Borders.Bottom.Visible = false;
        rowTop.Cells[1].AddParagraph(oModExpenses.GetSetexpensestype);
        rowTop.Cells[1].Borders.Left.Visible = false;
        rowTop.Cells[1].Borders.Right.Visible = false;
        rowTop.Cells[1].Borders.Bottom.Visible = false;
        rowTop.Cells[2].AddParagraph();
        rowTop.Cells[2].Borders.Left.Visible = false;
        rowTop.Cells[2].Borders.Right.Visible = false;
        rowTop.Cells[2].Borders.Bottom.Visible = false;
        rowTop.Cells[3].AddParagraph("Rujukan:");
        rowTop.Cells[3].Borders.Left.Visible = false;
        rowTop.Cells[3].Borders.Right.Visible = false;
        rowTop.Cells[3].Borders.Bottom.Visible = false;
        rowTop.Cells[4].AddParagraph(oModExpenses.GetSetremarks);
        rowTop.Cells[4].Borders.Left.Visible = false;
        rowTop.Cells[4].Borders.Right.Visible = false;
        rowTop.Cells[4].Borders.Bottom.Visible = false;

        // Create the item table
        MigraDoc.DocumentObjectModel.Tables.Table table = section.AddTable();
        table.Style = "Table";
        table.Borders.Color = MigraDoc.DocumentObjectModel.Colors.Blue;
        table.Borders.Width = 0.25;
        table.Borders.Left.Width = 0.5;
        table.Borders.Right.Width = 0.5;
        table.Rows.LeftIndent = 0;

        // Before you can add a row, you must define the columns
        Column column = table.AddColumn("1cm");
        column.Format.Alignment = ParagraphAlignment.Center;

        column = table.AddColumn("4.5cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("6cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("1cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("2cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("1.5cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("2cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        // Create the header of the table
        Row row = table.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Center;
        row.Format.Font.Bold = true;
        row.Height = "1cm";
        row.Shading.Color = MigraDoc.DocumentObjectModel.Colors.LightGray;
        row.Cells[0].AddParagraph("KETERANGAN ITEM");
        row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[0].MergeRight = 2;
        row.Cells[3].AddParagraph("QTY");
        row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[4].AddParagraph("HARGA");
        row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[5].AddParagraph("TAX");
        row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[6].AddParagraph("JUMLAH");
        row.Cells[6].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[6].VerticalAlignment = VerticalAlignment.Center;

        for (int i = 0; i < lsExpensesLineItem.Count; i++)
        {
            MainModel modExpDet = (MainModel)lsExpensesLineItem[i];

            // Each item fills two rows
            Row row1 = table.AddRow();
            row1.Height = "1cm";
            row1.TopPadding = 1.5;
            //row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row1.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row1.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row1.Cells[3].Format.Alignment = ParagraphAlignment.Center;
            row1.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row1.Cells[5].Format.Alignment = ParagraphAlignment.Right;
            row1.Cells[6].Format.Alignment = ParagraphAlignment.Right;

            row1.Cells[0].AddParagraph((i+1).ToString());
            if (oModExpenses.GetSetexpensescat.Equals("PAYMENT_VOUCHER"))
            {
                row1.Cells[1].AddParagraph(oMainCon.getParamDetails("000", modExpDet.GetSetitemno, oModExpenses.GetSetexpensestype, "").GetSetparamdesc);
            }
            else
            {
                row1.Cells[1].AddParagraph(modExpDet.GetSetitemno);
            }
            row1.Cells[2].AddParagraph(modExpDet.GetSetitemdesc);
            row1.Cells[3].AddParagraph(modExpDet.GetSetquantity.ToString());
            row1.Cells[4].AddParagraph(modExpDet.GetSetexpensesprice.ToString("#,##0.00"));
            row1.Cells[5].AddParagraph(modExpDet.GetSettaxamount.ToString("#,##0.00"));
            row1.Cells[6].AddParagraph(modExpDet.GetSettotalexpenses.ToString("#,##0.00"));

            if (i>0 && ((i+1)%10)==0)
            {
                row1.Cells[0].Borders.Bottom.Visible = true;
                row1.Cells[1].Borders.Bottom.Visible = true;
                row1.Cells[2].Borders.Bottom.Visible = true;
                row1.Cells[3].Borders.Bottom.Visible = true;
                row1.Cells[4].Borders.Bottom.Visible = true;
                row1.Cells[5].Borders.Bottom.Visible = true;
                row1.Cells[6].Borders.Bottom.Visible = true;
            }
            else
            {
                row1.Cells[0].Borders.Bottom.Visible = false;
                row1.Cells[1].Borders.Bottom.Visible = false;
                row1.Cells[2].Borders.Bottom.Visible = false;
                row1.Cells[3].Borders.Bottom.Visible = false;
                row1.Cells[4].Borders.Bottom.Visible = false;
                row1.Cells[5].Borders.Bottom.Visible = false;
                row1.Cells[6].Borders.Bottom.Visible = false;
            }
        }
        if ((lsExpensesLineItem.Count % 10) > 0)
        {
            int totalremainingrow = 10 - (lsExpensesLineItem.Count % 10);
            for (int j = 0; j < totalremainingrow; j++)
            {
                Row rowRemain = table.AddRow();
                rowRemain.Height = "1cm";
                rowRemain.Cells[0].AddParagraph();
                rowRemain.Cells[1].AddParagraph();
                rowRemain.Cells[2].AddParagraph();
                rowRemain.Cells[3].AddParagraph();
                rowRemain.Cells[4].AddParagraph();
                rowRemain.Cells[5].AddParagraph();
                rowRemain.Cells[6].AddParagraph();

                if (j == (totalremainingrow-1))
                {
                    rowRemain.Cells[0].Borders.Bottom.Visible = true;
                    rowRemain.Cells[1].Borders.Bottom.Visible = true;
                    rowRemain.Cells[2].Borders.Bottom.Visible = true;
                    rowRemain.Cells[3].Borders.Bottom.Visible = true;
                    rowRemain.Cells[4].Borders.Bottom.Visible = true;
                    rowRemain.Cells[5].Borders.Bottom.Visible = true;
                    rowRemain.Cells[6].Borders.Bottom.Visible = true;
                }
                else if (j > 0 && (j % (totalremainingrow-1)) == 0)
                {
                    rowRemain.Cells[0].Borders.Bottom.Visible = true;
                    rowRemain.Cells[1].Borders.Bottom.Visible = true;
                    rowRemain.Cells[2].Borders.Bottom.Visible = true;
                    rowRemain.Cells[3].Borders.Bottom.Visible = true;
                    rowRemain.Cells[4].Borders.Bottom.Visible = true;
                    rowRemain.Cells[5].Borders.Bottom.Visible = true;
                    rowRemain.Cells[6].Borders.Bottom.Visible = true;
                }
                else
                {
                    rowRemain.Cells[0].Borders.Bottom.Visible = false;
                    rowRemain.Cells[1].Borders.Bottom.Visible = false;
                    rowRemain.Cells[2].Borders.Bottom.Visible = false;
                    rowRemain.Cells[3].Borders.Bottom.Visible = false;
                    rowRemain.Cells[4].Borders.Bottom.Visible = false;
                    rowRemain.Cells[5].Borders.Bottom.Visible = false;
                    rowRemain.Cells[6].Borders.Bottom.Visible = false;
                }
            }
        }

        Row rowTot = table.AddRow();
        rowTot.Height = "1cm";
        rowTot.Format.Font.Bold = true;
        rowTot.Cells[0].AddParagraph("Catatan:");
        rowTot.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        rowTot.Cells[0].VerticalAlignment = VerticalAlignment.Top;
        rowTot.Cells[0].MergeRight = 2;

        rowTot.Cells[3].AddParagraph("JUMLAH BESAR");
        rowTot.Cells[3].Format.Alignment = ParagraphAlignment.Right;
        rowTot.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        rowTot.Cells[3].MergeRight = 2;

        rowTot.Cells[6].AddParagraph(oModExpenses.GetSettotalamount.ToString("#,##0.00"));
        rowTot.Cells[6].Format.Alignment = ParagraphAlignment.Right;
        rowTot.Cells[6].VerticalAlignment = VerticalAlignment.Center;

        //footer.AddText("Footer");
        //footer.Format.Font.Size = 9;
        //footer.Format.Alignment = ParagraphAlignment.Center;

        // Create the item table for footer
        //MigraDoc.DocumentObjectModel.Tables.Table tblBtm = section.Headers.Primary.AddImage(logo_lima);
        MigraDoc.DocumentObjectModel.Tables.Table tblBtm = section.Footers.Primary.AddTable();
        //MigraDoc.DocumentObjectModel.Tables.Table tblBtm = section.AddTable();
        tblBtm.Style = "Table";
        tblBtm.Borders.Color = MigraDoc.DocumentObjectModel.Colors.Blue;
        tblBtm.Borders.Width = 0.25;
        tblBtm.Borders.Left.Width = 0.5;
        tblBtm.Borders.Right.Width = 0.5;
        tblBtm.Rows.LeftIndent = 0;

        // Before you can add a row, you must define the columns
        Column colTblBtm = tblBtm.AddColumn("6cm");
        colTblBtm.Format.Alignment = ParagraphAlignment.Left;
        colTblBtm = tblBtm.AddColumn("6cm");
        colTblBtm.Format.Alignment = ParagraphAlignment.Left;
        colTblBtm = tblBtm.AddColumn("6cm");
        colTblBtm.Format.Alignment = ParagraphAlignment.Left;

        Row rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Left.Visible = false;
        rowTblBtm.Borders.Right.Visible = false;
        rowTblBtm.Borders.Top.Visible = false;
        //rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph();
        rowTblBtm.Cells[1].AddParagraph();
        rowTblBtm.Cells[2].AddParagraph();

        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Format.Font.Bold = true;
        rowTblBtm.Height = "1cm";
        rowTblBtm.Shading.Color = MigraDoc.DocumentObjectModel.Colors.Gray;
        rowTblBtm.Cells[0].AddParagraph("Disediakan Oleh:");
        rowTblBtm.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        rowTblBtm.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        rowTblBtm.Cells[1].AddParagraph("Disemak Oleh:");
        rowTblBtm.Cells[1].Format.Alignment = ParagraphAlignment.Left;
        rowTblBtm.Cells[1].VerticalAlignment = VerticalAlignment.Center;
        rowTblBtm.Cells[2].AddParagraph("Diluluskan Oleh:");
        rowTblBtm.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        rowTblBtm.Cells[2].VerticalAlignment = VerticalAlignment.Center;
        //rowTblBtm.Cells[2].Borders.Left.Visible = false;
        //rowTblBtm.Cells[2].Borders.Right.Visible = false;
        //rowTblBtm.Cells[2].Borders.Top.Visible = false;
        //rowTblBtm.Cells[2].Borders.Bottom.Visible = false;
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph();
        rowTblBtm.Cells[1].AddParagraph();
        rowTblBtm.Cells[2].AddParagraph();
        //rowTblBtm.Cells[2].Borders.Left.Visible = false;
        //rowTblBtm.Cells[2].Borders.Right.Visible = false;
        //rowTblBtm.Cells[2].Borders.Top.Visible = false;
        //rowTblBtm.Cells[2].Borders.Bottom.Visible = false;
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Bottom.Visible = false;
        //rowTblBtm.Cells[2].Borders.Left.Visible = false;
        //rowTblBtm.Cells[2].Borders.Right.Visible = false;
        //rowTblBtm.Cells[2].Borders.Top.Visible = false;
        //rowTblBtm.Cells[2].Borders.Bottom.Visible = false;
        rowTblBtm.Height = "2cm";
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph("Nama:");
        rowTblBtm.Cells[1].AddParagraph("Nama:");
        rowTblBtm.Cells[2].AddParagraph("Nama:");
        //rowTblBtm.Cells[2].Borders.Left.Visible = false;
        //rowTblBtm.Cells[2].Borders.Right.Visible = false;
        //rowTblBtm.Cells[2].Borders.Top.Visible = false;
        //rowTblBtm.Cells[2].Borders.Bottom.Visible = false;
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph("Jawatan:");
        rowTblBtm.Cells[1].AddParagraph("Jawatan:");
        rowTblBtm.Cells[2].AddParagraph("Jawatan:");
        //rowTblBtm.Cells[2].Borders.Left.Visible = false;
        //rowTblBtm.Cells[2].Borders.Right.Visible = false;
        //rowTblBtm.Cells[2].Borders.Top.Visible = false;
        //rowTblBtm.Cells[2].Borders.Bottom.Visible = false;
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Cells[0].AddParagraph("Tarikh:");
        rowTblBtm.Cells[1].AddParagraph("Tarikh:");
        rowTblBtm.Cells[2].AddParagraph("Tarikh:");
        //rowTblBtm.Cells[2].Borders.Left.Visible = false;
        //rowTblBtm.Cells[2].Borders.Right.Visible = false;
        //rowTblBtm.Cells[2].Borders.Top.Visible = false;
        //rowTblBtm.Cells[2].Borders.Bottom.Visible = false;

        // Create a renderer for PDF that uses Unicode font encoding
        PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

        // Set the MigraDoc document
        pdfRenderer.Document = doc;

        // Create the PDF document
        pdfRenderer.RenderDocument();

        // Save the document...
        string pdfFilename = sExpensesNo + ".pdf";
        string file = Server.MapPath("~/App_Data/" + pdfFilename);
        //string file = HttpContext.Current.Server.MapPath("~/pdf/" + pdfFilename);
        //string file = "C:/TEMP/" + pdfFilename;

        // ...and start a viewer //
        //pdfRenderer.Save(file);
        //Process.Start(file);

        // Send PDF to browser //
        MemoryStream stream = new MemoryStream();
        pdfRenderer.Save(stream, false);
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-length", stream.Length.ToString());
        Response.BinaryWrite(stream.ToArray());
        Response.Flush();
        stream.Close();
        Response.End();

        //download file //
        //Response.ContentType = "Application/pdf";
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + pdfFilename);
        //Response.TransmitFile(file);
        //Response.End();

    }

}