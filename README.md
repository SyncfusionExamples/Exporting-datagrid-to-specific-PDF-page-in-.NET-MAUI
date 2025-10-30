# Exporting-datagrid-to-specific-PDF-page-in-.NET-MAUI
This article explains how to export a Syncfusion .NET MAUI DataGrid (SfDataGrid) to a specific page inside a PDF document. The included sample shows how to construct a multi-page PdfDocument, pick a target page by index, and merge the grid’s content into that page using the DataGridPdfExportingController. This approach is useful when you want a title or summary on page one and the data grid starting on a later page. For full guidance, refer to the official documentation: [Export to PDF (SfDataGrid)](https://help.syncfusion.com/maui/datagrid/export-to-pdf)

The project contains a working reference implementation:
- DataGridMAUI/MainPage.xaml defines the UI: an Export To PDF button and an SfDataGrid with explicit columns, grid lines, selection, and styles.
- DataGridMAUI/MainPage.xaml.cs implements the export: it creates the PdfDocument, adds pages, sets StartPageIndex, exports the grid, then saves and opens the file.

## xaml
```
<ContentPage.BindingContext>
    <local:OrderInfoRepository/>
</ContentPage.BindingContext>

<StackLayout>
    <Button Text="Export To PDF" WidthRequest="200" HeightRequest="50" 
                Clicked="OnExportToPDF" />
    <syncfusion:SfDataGrid x:Name="dataGrid"
                            Margin="20"
                            VerticalOptions="FillAndExpand"
                            ItemsSource="{Binding OrderInfoCollection}"
                            GridLinesVisibility="Both"
                            HeaderGridLinesVisibility="Both"
                            AutoGenerateColumnsMode="None"
                            SelectionMode="Multiple"
                            ColumnWidthMode="Auto">
        <syncfusion:SfDataGrid.DefaultStyle>
            <syncfusion:DataGridStyle RowBackground="LightBlue" HeaderRowBackground="LightGoldenrodYellow"/>
        </syncfusion:SfDataGrid.DefaultStyle>
        <syncfusion:SfDataGrid.Columns>
            <syncfusion:DataGridNumericColumn Format="D"
                                                HeaderText="Order ID"
                                                MappingName="OrderID">
            </syncfusion:DataGridNumericColumn>
            <syncfusion:DataGridTextColumn HeaderText="Customer ID"
                                            MappingName="CustomerID">
            </syncfusion:DataGridTextColumn>
            <syncfusion:DataGridTextColumn MappingName="Customer"
                                            HeaderText="Customer">
            </syncfusion:DataGridTextColumn>
            <syncfusion:DataGridTextColumn HeaderText="Ship City"
                                            MappingName="ShipCity">
            </syncfusion:DataGridTextColumn>
            <syncfusion:DataGridTextColumn HeaderText="Ship Country"
                                            MappingName="ShipCountry">
            </syncfusion:DataGridTextColumn>
        </syncfusion:SfDataGrid.Columns>
    </syncfusion:SfDataGrid>
</StackLayout>
```

## C#
This handler creates a PdfDocument, adds pages, and sets DataGridPdfExportingOption.StartPageIndex to tell the exporter exactly which page to place the grid on. You can continue to draw custom content on any page using Syncfusion PDF APIs.
```
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
```

## How it works
- Build or reuse a PdfDocument and add enough pages ahead of time.
- Choose the destination using StartPageIndex (0 is first page, 1 is second, etc.).
- Pass the PdfDocument via DataGridPdfExportingOption.PdfDocument so the exporter writes into your existing document.
- Configure PageSettings (e.g., Landscape) to better accommodate wide grids.

## Related documentation and resources
- DataGrid overview: https://help.syncfusion.com/maui/datagrid/overview
- Product page: https://www.syncfusion.com/maui-controls/maui-datagrid

##### Conclusion
 
I hope you enjoyed learning about how to export datagrid to specific pdf page in .NET MAUI DataGrid (SfDataGrid).
 
You can refer to our [.NET MAUI DataGrid’s feature tour](https://www.syncfusion.com/maui-controls/maui-datagrid) page to learn about its other groundbreaking feature representations. You can also explore our [.NET MAUI DataGrid Documentation](https://help.syncfusion.com/maui/datagrid/getting-started) to understand how to present and manipulate data. 
For current customers, you can check out our .NET MAUI components on the [License and Downloads](https://www.syncfusion.com/sales/teamlicense) page. If you are new to Syncfusion, you can try our 30-day [free trial](https://www.syncfusion.com/downloads/maui) to explore our .NET MAUI DataGrid and other .NET MAUI components.
 
If you have any queries or require clarifications, please let us know in the comments below. You can also contact us through our [support forums](https://www.syncfusion.com/forums), [Direct-Trac](https://support.syncfusion.com/create) or [feedback portal](https://www.syncfusion.com/feedback/maui?control=sfdatagrid), or the feedback portal. We are always happy to assist you!
