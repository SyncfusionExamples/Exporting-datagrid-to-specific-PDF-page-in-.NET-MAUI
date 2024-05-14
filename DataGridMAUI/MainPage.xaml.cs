using Syncfusion.Maui.DataGrid.Exporting;
using Syncfusion.Pdf;

namespace DataGridMAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void OnExportToPDF(object sender, EventArgs e)
        {
            MemoryStream stream = new MemoryStream();
            DataGridPdfExportingController pdfExport = new DataGridPdfExportingController();

            var pdfDocument = new PdfDocument()
            {
                PageSettings =
                {
                    Orientation = PdfPageOrientation.Landscape
                }
            };
            pdfDocument.Pages.Add();            
            pdfDocument.Pages.Add();
            pdfDocument.Pages.Add();
            DataGridPdfExportingOption option = new DataGridPdfExportingOption() { StartPageIndex = 1, PdfDocument = pdfDocument};            
            var pdfDoc = pdfExport.ExportToPdf(this.dataGrid, option);
            pdfDoc.Save(stream);
            pdfDoc.Close(true);
            SaveService saveService = new();
            saveService.SaveAndView("Export Feature.pdf", "application/pdf", stream);
        }
    }
}
