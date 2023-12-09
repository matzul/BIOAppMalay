using DataDynamics.ActiveReports;

/// <summary>
/// Summary description for NewActiveReport1.
/// </summary>
public class ReportGen : DataDynamics.ActiveReports.ActiveReport
{
	private DataDynamics.ActiveReports.PageHeader pageHeader;
	private DataDynamics.ActiveReports.Detail detail;
    private TextBox txtDONumber;
    private TextBox txtItemNumber;
    private TextBox txtItemDesc;
	private DataDynamics.ActiveReports.PageFooter pageFooter;

    public ReportGen()
	{
		//
		// Required for Windows Form Designer support
		//
		InitializeComponent();
	}

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	protected override void Dispose( bool disposing )
	{
		if( disposing )
		{
		}
		base.Dispose( disposing );
	}

	#region ActiveReport Designer generated code
	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
        this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
        this.detail = new DataDynamics.ActiveReports.Detail();
        this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
        this.txtDONumber = new DataDynamics.ActiveReports.TextBox();
        this.txtItemNumber = new DataDynamics.ActiveReports.TextBox();
        this.txtItemDesc = new DataDynamics.ActiveReports.TextBox();
        ((System.ComponentModel.ISupportInitialize)(this.txtDONumber)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtItemNumber)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtItemDesc)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // pageHeader
        // 
        this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtDONumber});
        this.pageHeader.Height = 2.989F;
        this.pageHeader.Name = "pageHeader";
        // 
        // detail
        // 
        this.detail.ColumnSpacing = 0F;
        this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtItemNumber,
            this.txtItemDesc});
        this.detail.Height = 0.208F;
        this.detail.Name = "detail";
        // 
        // pageFooter
        // 
        this.pageFooter.Height = 3.573F;
        this.pageFooter.Name = "pageFooter";
        // 
        // txtDONumber
        // 
        this.txtDONumber.Border.BottomColor = System.Drawing.Color.Black;
        this.txtDONumber.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
        this.txtDONumber.Border.LeftColor = System.Drawing.Color.Black;
        this.txtDONumber.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
        this.txtDONumber.Border.RightColor = System.Drawing.Color.Black;
        this.txtDONumber.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
        this.txtDONumber.Border.TopColor = System.Drawing.Color.Black;
        this.txtDONumber.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
        this.txtDONumber.Height = 0.188F;
        this.txtDONumber.Left = 1.5F;
        this.txtDONumber.Name = "txtDONumber";
        this.txtDONumber.DataField = "dtDONumber";
        this.txtDONumber.Style = "ddo-char-set: 0; font-size: 9pt; ";
        this.txtDONumber.Text = "DO_NO";
        this.txtDONumber.Top = 0F;
        this.txtDONumber.Width = 0.875F;
        // 
        // txtItemNumber
        // 
        this.txtItemNumber.Border.BottomColor = System.Drawing.Color.Black;
        this.txtItemNumber.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
        this.txtItemNumber.Border.LeftColor = System.Drawing.Color.Black;
        this.txtItemNumber.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
        this.txtItemNumber.Border.RightColor = System.Drawing.Color.Black;
        this.txtItemNumber.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
        this.txtItemNumber.Border.TopColor = System.Drawing.Color.Black;
        this.txtItemNumber.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
        this.txtItemNumber.Height = 0.2F;
        this.txtItemNumber.Left = 0.25F;
        this.txtItemNumber.Name = "txtItemNumber";
        this.txtItemNumber.DataField = "dtItemNumber";
        this.txtItemNumber.Style = "ddo-char-set: 0; font-size: 8.25pt; ";
        this.txtItemNumber.Text = "ITEM_NO";
        this.txtItemNumber.Top = 0F;
        this.txtItemNumber.Width = 1.125F;
        // 
        // txtItemDesc
        // 
        this.txtItemDesc.Border.BottomColor = System.Drawing.Color.Black;
        this.txtItemDesc.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
        this.txtItemDesc.Border.LeftColor = System.Drawing.Color.Black;
        this.txtItemDesc.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
        this.txtItemDesc.Border.RightColor = System.Drawing.Color.Black;
        this.txtItemDesc.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
        this.txtItemDesc.Border.TopColor = System.Drawing.Color.Black;
        this.txtItemDesc.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None;
        this.txtItemDesc.Height = 0.2F;
        this.txtItemDesc.Left = 1.438F;
        this.txtItemDesc.Name = "txtItemDesc";
        this.txtItemDesc.DataField = "dtItemDesc";
        this.txtItemDesc.Style = "ddo-char-set: 0; font-size: 8.25pt; ";
        this.txtItemDesc.Text = "ITEM_DESC";
        this.txtItemDesc.Top = 0F;
        this.txtItemDesc.Width = 2.313F;
        // 
        // SpartDOInvoice
        // 
        this.MasterReport = false;
        this.PageSettings.PaperHeight = 11.69F;
        this.PageSettings.PaperWidth = 8.27F;
        this.PrintWidth = 7.25F;
        this.Sections.Add(this.pageHeader);
        this.Sections.Add(this.detail);
        this.Sections.Add(this.pageFooter);
        this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                    "l; font-size: 10pt; color: Black; ", "Normal"));
        this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; ", "Heading1", "Normal"));
        this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                    "lic; ", "Heading2", "Normal"));
        this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ", "Heading3", "Normal"));
        ((System.ComponentModel.ISupportInitialize)(this.txtDONumber)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtItemNumber)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtItemDesc)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}
	#endregion
}
