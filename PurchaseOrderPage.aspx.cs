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

public partial class PurchaseOrderPage : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sOrderNo = "";
    public MainModel modCompInfo = new MainModel();
    public MainModel oModOrder = new MainModel();
    public ArrayList lsOrderLineItem = new ArrayList();

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
        if (Request.QueryString["orderno"] != null)
        {
            sOrderNo = Request.QueryString["orderno"].ToString();
        }
    }
    private void processValues()
    {
        modCompInfo = oMainCon.getCompInfoDetails(sCurrComp);
        oModOrder = oMainCon.getPurchaseOrderHeaderDetails(sCurrComp, sOrderNo);
        lsOrderLineItem = oMainCon.getPurchaseOrderDetailsList(sCurrComp, sOrderNo, 0, "");
        generatePDFFile();
    }
    private void generatePDFFile()
    {
        // Create a MigraDoc document
        Document doc = new Document();
        doc.Info.Title = "PESANAN BELIAN";
        doc.Info.Subject = "No. Pesanan:" + oModOrder.GetSetorderno;
        doc.Info.Author = "B.I.O.App System";

        //set page orientation
        doc.DefaultPageSetup.Orientation = MigraDoc.DocumentObjectModel.Orientation.Portrait;
        doc.DefaultPageSetup.TopMargin = "7.5cm"; //120
        doc.DefaultPageSetup.BottomMargin = "8.5cm"; //150
        doc.DefaultPageSetup.LeftMargin = 40;
        doc.DefaultPageSetup.RightMargin = 40;
        doc.DefaultPageSetup.HeaderDistance = "1.5cm"; //20
        doc.DefaultPageSetup.FooterDistance = "1cm"; //20

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

        // Put 1st logo in the header
        string logo_lima = Server.MapPath("~/images/"+modCompInfo.GetSetcomp_logo1);
        MigraDoc.DocumentObjectModel.Shapes.Image image = section.Headers.Primary.AddImage(logo_lima);
        image.Height = "1cm";
        image.LockAspectRatio = true;
        image.RelativeVertical = RelativeVertical.Line;
        image.RelativeHorizontal = RelativeHorizontal.Margin;
        image.Top = ShapePosition.Top;
        image.Left = ShapePosition.Right;
        image.WrapFormat.Style = WrapStyle.Through;

        // Put 2nd logo in the header
        string logo_mod = Server.MapPath("~/images/"+modCompInfo.GetSetcomp_logo2);
        MigraDoc.DocumentObjectModel.Shapes.Image image2 = section.Headers.Primary.AddImage(logo_mod);
        image2.Height = "1cm";
        image2.LockAspectRatio = true;
        image2.RelativeVertical = RelativeVertical.Line;
        image2.RelativeHorizontal = RelativeHorizontal.Margin;
        image2.Top = ShapePosition.Top;
        image2.Left = ShapePosition.Left;
        image2.WrapFormat.Style = WrapStyle.Through;

        // Create Header
        Paragraph header = section.Headers.Primary.AddParagraph();
        header.AddText("PESANAN BELIAN");
        header.Format.Font.Size = 12;
        header.Format.Font.Bold = true;
        header.Format.Alignment = ParagraphAlignment.Center;

        // Create main section for Purchase Order 
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
        Column columnTop = tableTop.AddColumn("8cm");
        columnTop.Format.Alignment = ParagraphAlignment.Left;
        columnTop = tableTop.AddColumn("3cm");
        columnTop.Format.Alignment = ParagraphAlignment.Left;
        columnTop = tableTop.AddColumn("7cm");
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
        //rowTop.Borders.Bottom.Visible = false;
        rowTop.Cells[0].AddParagraph();
        rowTop.Cells[0].Borders.Bottom.Visible = false;
        rowTop.Cells[1].AddParagraph();
        rowTop.Cells[2].AddParagraph();


        rowTop = tableTop.AddRow();
        rowTop.Cells[0].AddParagraph("Daripada:");
        rowTop.Cells[0].AddParagraph(modCompInfo.GetSetcomp_name);
        rowTop.Cells[0].AddParagraph(modCompInfo.GetSetcomp_address);
        rowTop.Cells[0].AddParagraph(modCompInfo.GetSetcomp_website);
        rowTop.Cells[0].Borders.Left.Visible = false;
        rowTop.Cells[0].Borders.Right.Visible = false;
        rowTop.Cells[0].Borders.Top.Visible = false;
        rowTop.Cells[0].Borders.Bottom.Visible = false;
        rowTop.Cells[0].MergeDown = 5;
        rowTop.Cells[1].AddParagraph("No. Pesanan");
        rowTop.Cells[1].Borders.Right.Visible = false;
        rowTop.Cells[2].AddParagraph(": " + oModOrder.GetSetorderno);
        rowTop.Cells[2].Borders.Left.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Cells[1].AddParagraph("Tarikh");
        rowTop.Cells[1].Borders.Right.Visible = false;
        rowTop.Cells[2].AddParagraph(": " + oModOrder.GetSetorderdate);
        rowTop.Cells[2].Borders.Left.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Cells[1].AddParagraph("Jenis");
        rowTop.Cells[1].Borders.Right.Visible = false;
        rowTop.Cells[2].AddParagraph(": " + oModOrder.GetSetordertype);
        rowTop.Cells[2].Borders.Left.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Cells[1].AddParagraph("Pejabat Perolehan");
        rowTop.Cells[1].Borders.Right.Visible = false;
        rowTop.Cells[2].AddParagraph(": " + modCompInfo.GetSetcomp_name);
        rowTop.Cells[2].Borders.Left.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Cells[1].AddParagraph("Pegawai Perolehan");
        rowTop.Cells[1].Borders.Right.Visible = false;
        rowTop.Cells[2].AddParagraph(": " + oModOrder.GetSetusername);
        rowTop.Cells[2].Borders.Left.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Cells[0].Borders.Bottom.Visible = false; 
        rowTop.Cells[1].AddParagraph("No. Dihubungi");
        rowTop.Cells[1].Borders.Right.Visible = false;
        rowTop.Cells[2].AddParagraph(": " + modCompInfo.GetSetcomp_contactno);
        rowTop.Cells[2].Borders.Left.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Cells[0].AddParagraph("");
        rowTop.Cells[0].MergeRight = 2;
        rowTop.Cells[0].Borders.Left.Visible = false;
        rowTop.Cells[0].Borders.Right.Visible = false;
        rowTop.Cells[0].Borders.Top.Visible = false;
        rowTop.Cells[0].Borders.Bottom.Visible = false;
        rowTop.Cells[2].Borders.Right.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Cells[0].AddParagraph("Kepada: ");
        rowTop.Cells[0].AddParagraph(oModOrder.GetSetbpdesc);
        rowTop.Cells[0].AddParagraph(oModOrder.GetSetbpaddress);
        rowTop.Cells[0].AddParagraph("Hubungi: " + oModOrder.GetSetbpcontact);
        rowTop.Cells[0].Borders.Left.Visible = false;
        rowTop.Cells[0].Borders.Right.Visible = false;
        rowTop.Cells[0].Borders.Bottom.Visible = false;
        rowTop.Cells[1].AddParagraph("Support: ");
        rowTop.Cells[1].AddParagraph(modCompInfo.GetSetcomp_contact);
        rowTop.Cells[1].AddParagraph("ADMIN & SUPPORT");
        rowTop.Cells[1].AddParagraph("Hubungi: " + modCompInfo.GetSetcomp_contactno);
        rowTop.Cells[1].Borders.Left.Visible = false;
        rowTop.Cells[1].Borders.Right.Visible = false;
        rowTop.Cells[1].Borders.Bottom.Visible = false;
        rowTop.Cells[1].MergeRight = 1;
        rowTop.Cells[2].Borders.Right.Visible = false;

        // Create the item table
        MigraDoc.DocumentObjectModel.Tables.Table table = section.AddTable();
        table.Style = "Table";
        table.Borders.Color = MigraDoc.DocumentObjectModel.Colors.Blue;
        table.Borders.Width = 0.25;
        table.Borders.Left.Width = 0.5;
        table.Borders.Right.Width = 0.5;
        table.Rows.LeftIndent = 0;

        // Before you can add a row, you must define the columns
        Column column = table.AddColumn("0.5cm");
        column.Format.Alignment = ParagraphAlignment.Center;

        column = table.AddColumn("3cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("4cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("2cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("2cm");
        column.Format.Alignment = ParagraphAlignment.Center;

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
        row.Shading.Color = MigraDoc.DocumentObjectModel.Colors.LightGray;
        row.Cells[0].AddParagraph("#");
        row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[1].AddParagraph("NO. ITEM");
        row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[1].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[2].AddParagraph("KETERANGAN");
        row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[2].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[3].AddParagraph("HARGA UNIT");
        row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[3].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[4].AddParagraph("DISKAUN");
        row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[4].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[5].AddParagraph("QTY");
        row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[6].AddParagraph("HARGA");
        row.Cells[6].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[6].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[7].AddParagraph("TAX");
        row.Cells[7].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[7].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[8].AddParagraph("JUMLAH");
        row.Cells[8].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[8].VerticalAlignment = VerticalAlignment.Bottom;

        for (int i = 0; i < lsOrderLineItem.Count; i++)
        {
            MainModel modOrdDet = (MainModel)lsOrderLineItem[i];

            // Each item fills two rows
            Row row1 = table.AddRow();
            row1.Height = "2cm";
            row1.TopPadding = 1.5;
            //row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row1.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row1.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row1.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row1.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row1.Cells[5].Format.Alignment = ParagraphAlignment.Center;
            row1.Cells[6].Format.Alignment = ParagraphAlignment.Right;
            row1.Cells[7].Format.Alignment = ParagraphAlignment.Right;
            row1.Cells[8].Format.Alignment = ParagraphAlignment.Right;

            row1.Cells[0].AddParagraph((i+1).ToString());
            row1.Cells[1].AddParagraph(modOrdDet.GetSetitemno);
            row1.Cells[2].AddParagraph(modOrdDet.GetSetitemdesc);
            row1.Cells[3].AddParagraph(modOrdDet.GetSetunitprice.ToString("#,##0.00"));
            row1.Cells[4].AddParagraph(modOrdDet.GetSetdiscamount.ToString("#,##0.00"));
            row1.Cells[5].AddParagraph(modOrdDet.GetSetquantity.ToString());
            row1.Cells[6].AddParagraph(modOrdDet.GetSetorderprice.ToString("#,##0.00"));
            row1.Cells[7].AddParagraph(modOrdDet.GetSettaxamount.ToString("#,##0.00"));
            row1.Cells[8].AddParagraph(modOrdDet.GetSettotalprice.ToString("#,##0.00"));

            if (i>0 && ((i+1)%6)==0)
            {
                row1.Cells[0].Borders.Bottom.Visible = true;
                row1.Cells[1].Borders.Bottom.Visible = true;
                row1.Cells[2].Borders.Bottom.Visible = true;
                row1.Cells[3].Borders.Bottom.Visible = true;
                row1.Cells[4].Borders.Bottom.Visible = true;
                row1.Cells[5].Borders.Bottom.Visible = true;
                row1.Cells[6].Borders.Bottom.Visible = true;
                row1.Cells[7].Borders.Bottom.Visible = true;
                row1.Cells[8].Borders.Bottom.Visible = true;
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
                row1.Cells[7].Borders.Bottom.Visible = false;
                row1.Cells[8].Borders.Bottom.Visible = false;
            }
        }
        if ((lsOrderLineItem.Count % 6) > 0)
        {
            int totalremainingrow = 6 - (lsOrderLineItem.Count % 6);
            for (int j = 0; j < totalremainingrow; j++)
            {
                Row rowRemain = table.AddRow();
                rowRemain.Height = "2cm";
                rowRemain.Cells[0].AddParagraph();
                rowRemain.Cells[1].AddParagraph();
                rowRemain.Cells[2].AddParagraph();
                rowRemain.Cells[3].AddParagraph();
                rowRemain.Cells[4].AddParagraph();
                rowRemain.Cells[5].AddParagraph();
                rowRemain.Cells[6].AddParagraph();
                rowRemain.Cells[7].AddParagraph();
                rowRemain.Cells[8].AddParagraph();

                if (j == (totalremainingrow-1))
                {
                    rowRemain.Cells[0].Borders.Bottom.Visible = true;
                    rowRemain.Cells[1].Borders.Bottom.Visible = true;
                    rowRemain.Cells[2].Borders.Bottom.Visible = true;
                    rowRemain.Cells[3].Borders.Bottom.Visible = true;
                    rowRemain.Cells[4].Borders.Bottom.Visible = true;
                    rowRemain.Cells[5].Borders.Bottom.Visible = true;
                    rowRemain.Cells[6].Borders.Bottom.Visible = true;
                    rowRemain.Cells[7].Borders.Bottom.Visible = true;
                    rowRemain.Cells[8].Borders.Bottom.Visible = true;
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
                    rowRemain.Cells[7].Borders.Bottom.Visible = true;
                    rowRemain.Cells[8].Borders.Bottom.Visible = true;
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
                    rowRemain.Cells[7].Borders.Bottom.Visible = false;
                    rowRemain.Cells[8].Borders.Bottom.Visible = false;
                }
            }
        }

        /*
        Row rowTax = table.AddRow();
        //rowTax.Height = "1cm";
        rowTax.Cells[0].AddParagraph();
        rowTax.Cells[0].Borders.Left.Visible = false;
        rowTax.Cells[0].Borders.Right.Visible = false;
        rowTax.Cells[0].Borders.Bottom.Visible = false;
        rowTax.Cells[0].MergeRight = 6;
        rowTax.Cells[7].AddParagraph("TAX");
        rowTax.Cells[7].Format.Alignment = ParagraphAlignment.Left;
        rowTax.Cells[8].AddParagraph(oModOrder.GetSettaxamount.ToString("#,##0.00"));
        rowTax.Cells[8].Format.Alignment = ParagraphAlignment.Right;
        */

        Row rowTot = table.AddRow();
        rowTot.Height = "1cm";
        rowTot.Format.Font.Bold = true;
        rowTot.Cells[0].AddParagraph();
        rowTot.Cells[0].Borders.Left.Visible = false;
        rowTot.Cells[0].Borders.Right.Visible = false;
        rowTot.Cells[0].Borders.Bottom.Visible = false;
        rowTot.Cells[0].MergeRight = 6;
        /*
        rowTot.Cells[1].AddParagraph();
        rowTot.Cells[1].Borders.Left.Visible = false;
        rowTot.Cells[2].AddParagraph();
        rowTot.Cells[2].Borders.Left.Visible = false;
        rowTot.Cells[3].AddParagraph();
        rowTot.Cells[3].Borders.Left.Visible = false;
        rowTot.Cells[4].AddParagraph();
        rowTot.Cells[4].Borders.Left.Visible = false;
        rowTot.Cells[5].AddParagraph();
        rowTot.Cells[5].Borders.Left.Visible = false;
        rowTot.Cells[6].AddParagraph();
        rowTot.Cells[6].Borders.Left.Visible = false;
        */
        rowTot.Cells[6].Borders.Right.Visible = false;

        rowTot.Cells[7].AddParagraph("JUMLAH BESAR");
        rowTot.Cells[7].Format.Alignment = ParagraphAlignment.Left;
        rowTot.Cells[7].VerticalAlignment = VerticalAlignment.Center;
        rowTot.Cells[7].Borders.Left.Visible = false;
        //rowTot.Cells[7].Borders.Right.Visible = false;
        rowTot.Cells[7].Borders.Bottom.Visible = false;

        rowTot.Cells[8].AddParagraph(oModOrder.GetSettotalamount.ToString("#,##0.00"));
        rowTot.Cells[8].Format.Alignment = ParagraphAlignment.Right;
        rowTot.Cells[8].VerticalAlignment = VerticalAlignment.Center;

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
        rowTblBtm.Cells[0].AddParagraph("Catatan:");
        rowTblBtm.Cells[0].MergeRight = 2; 
        
        rowTblBtm = tblBtm.AddRow();
        //rowTblBtm.Borders.Left.Visible = false;
        //rowTblBtm.Borders.Right.Visible = false;
        //rowTblBtm.Borders.Top.Visible = false;
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph(oModOrder.GetSetorderremarks);
        rowTblBtm.Cells[0].MergeRight = 2;

        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Height = "2cm";
        //rowTblBtm.Borders.Left.Visible = false;
        //rowTblBtm.Borders.Right.Visible = false;
        rowTblBtm.Borders.Top.Visible = false;
        //rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph();
        rowTblBtm.Cells[0].MergeRight = 2;

        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Left.Visible = false;
        rowTblBtm.Borders.Right.Visible = false;
        rowTblBtm.Borders.Top.Visible = false;
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph("1. Pesanan Belian ini sah sehingga tiga(3) bulan dari tarikh ia dikeluarkan.");
        rowTblBtm.Cells[0].MergeRight = 2;

        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Left.Visible = false;
        rowTblBtm.Borders.Right.Visible = false;
        rowTblBtm.Borders.Top.Visible = false;
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph("2. Sila pastikan Pesanan Belian ini ditandatangani sebelum salinan dikembalikan ke Pejabat Perolehan.");
        rowTblBtm.Cells[0].MergeRight = 2;

        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Left.Visible = false;
        rowTblBtm.Borders.Right.Visible = false;
        rowTblBtm.Borders.Top.Visible = false;
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph("3. Jabatan Kewangan hanya akan memproses bayaran ke atas Pesanan Belian ini jika Invois disertakan bersama.");
        rowTblBtm.Cells[0].MergeRight = 2; 
        
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Left.Visible = false;
        rowTblBtm.Borders.Right.Visible = false;
        rowTblBtm.Borders.Top.Visible = false;
        //rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph();
        rowTblBtm.Cells[0].MergeRight = 1;
        rowTblBtm.Cells[2].AddParagraph();
        rowTblBtm.Cells[2].Borders.Bottom.Visible = false;
        
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Format.Font.Bold = true;
        rowTblBtm.Cells[0].AddParagraph("Disediakan Oleh:");
        //rowTblBtm.Cells[0].Borders.Top.Visible = true;
        rowTblBtm.Cells[1].AddParagraph("Disahkan Oleh:");
        rowTblBtm.Cells[2].AddParagraph();
        rowTblBtm.Cells[2].Borders.Left.Visible = false;
        rowTblBtm.Cells[2].Borders.Right.Visible = false;
        rowTblBtm.Cells[2].Borders.Top.Visible = false;
        rowTblBtm.Cells[2].Borders.Bottom.Visible = false;
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph();
        rowTblBtm.Cells[1].AddParagraph();
        rowTblBtm.Cells[2].AddParagraph();
        rowTblBtm.Cells[2].Borders.Left.Visible = false;
        rowTblBtm.Cells[2].Borders.Right.Visible = false;
        rowTblBtm.Cells[2].Borders.Top.Visible = false;
        rowTblBtm.Cells[2].Borders.Bottom.Visible = false;
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[2].Borders.Left.Visible = false;
        rowTblBtm.Cells[2].Borders.Right.Visible = false;
        rowTblBtm.Cells[2].Borders.Top.Visible = false;
        rowTblBtm.Cells[2].Borders.Bottom.Visible = false;
        rowTblBtm.Height = "2cm";
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph("Nama:");
        rowTblBtm.Cells[1].AddParagraph("Nama:");
        rowTblBtm.Cells[2].AddParagraph();
        rowTblBtm.Cells[2].Borders.Left.Visible = false;
        rowTblBtm.Cells[2].Borders.Right.Visible = false;
        rowTblBtm.Cells[2].Borders.Top.Visible = false;
        rowTblBtm.Cells[2].Borders.Bottom.Visible = false;
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph("Jawatan:");
        rowTblBtm.Cells[1].AddParagraph("Jawatan:");
        rowTblBtm.Cells[2].AddParagraph();
        rowTblBtm.Cells[2].Borders.Left.Visible = false;
        rowTblBtm.Cells[2].Borders.Right.Visible = false;
        rowTblBtm.Cells[2].Borders.Top.Visible = false;
        rowTblBtm.Cells[2].Borders.Bottom.Visible = false;
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Cells[0].AddParagraph("Tarikh:");
        rowTblBtm.Cells[1].AddParagraph("Tarikh:");
        rowTblBtm.Cells[2].AddParagraph();
        rowTblBtm.Cells[2].Borders.Left.Visible = false;
        rowTblBtm.Cells[2].Borders.Right.Visible = false;
        rowTblBtm.Cells[2].Borders.Top.Visible = false;
        rowTblBtm.Cells[2].Borders.Bottom.Visible = false;

        // Create a renderer for PDF that uses Unicode font encoding
        PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

        // Set the MigraDoc document
        pdfRenderer.Document = doc;

        // Create the PDF document
        pdfRenderer.RenderDocument();

        // Save the document...
        string pdfFilename = sOrderNo + ".pdf";
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